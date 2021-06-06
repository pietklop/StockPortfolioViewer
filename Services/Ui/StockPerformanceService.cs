using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Entities;
using log4net;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;

namespace Services.Ui
{
    public class StockPerformanceService
    {
        private readonly ILog log;
        private readonly StockDbContext db;

        public StockPerformanceService(ILog log, StockDbContext db)
        {
            this.log = log;
            this.db = db;
        }

        public List<ValuePointDto> GetValues(string isin, PerformanceInterval interval, DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            // todo
            // performance based on owned stocks (this will only result in another final performance value)
            // sum of all stocks
            // support all intervals (ui)

            bool fromStart = dateFrom == null;
            dateFrom ??= DateTime.MinValue;
            dateTo ??= DateTime.Now.Date;

            var allPitValues = GetAllPitStockValues(dateFrom.Value, dateTo.Value, new []{isin});
            var pitValues = allPitValues.GroupBy(p => p.StockId).First().ToList();

            List<ValuePointDto> points = GetPoints(pitValues, interval, dateFrom.Value, dateTo.Value);

            return ScalePointsForGraph(points);
        }

        private List<ValuePointDto> ScalePointsForGraph(List<ValuePointDto> points)
        {   // make sure first value of range is 100
            var baseValue = points.First().RelativeValue;
            points.ForEach(p => p.RelativeValue *= 100 / baseValue);
            var maxDiv = points.Max(p => p.Dividend);
            if (maxDiv > 0)
                points.ForEach(p => p.Dividend *= 50 / maxDiv); // max div is scaled to 50% in graph

            return points;
        }

        private List<PitStockValue> GetAllPitStockValues(DateTime dateFrom, DateTime dateTo, string[] isins = null)
        {
            var allPitValues = db.PitStockValues
                .Include(p => p.Stock).ThenInclude(s => s.Dividends)
                .Include(p => p.Stock).ThenInclude(s => s.Transactions).ThenInclude(t => t.StockValue)
                .Where(p => isins == null || isins.Contains(p.Stock.Isin))
                .Where(p => p.TimeStamp > dateFrom.Date && p.TimeStamp.Date <= dateTo.Date)
                .OrderBy(p => p.TimeStamp).ToList();

            return allPitValues;
        }

        private List<ValuePointDto> GetPoints(List<PitStockValue> pitValues, PerformanceInterval interval, DateTime dateFrom, DateTime dateTo)
        {
            dateFrom = pitValues.First().TimeStamp.Date;
            var date = dateTo;
            var stock = pitValues.First().Stock;
            var transactions = stock.Transactions.ToList();
            var divsToAdd = new List<Dividend>();
            divsToAdd.AddRange(stock.Dividends.ToList());
            var dates = new List<DateTime>();
            var divAddedValue = new List<double>();
            while (date > dateFrom)
            {
                var nextDate = SubtractInterval(date);
                
                var divsToAddThisSpan = divsToAdd.Where(d => d.TimeStamp.Date >= nextDate.Date).ToList();
                if (divsToAddThisSpan.Any())
                {
                    var quantity = Quantity(transactions, date);
                    divAddedValue.Add(divsToAddThisSpan.Sum(d => d.UserValue) / quantity);
                    divsToAddThisSpan.ForEach(d => divsToAdd.Remove(d));
                }
                else
                    divAddedValue.Add(0);

                dates.Add(date);
                date = nextDate;
            }

            var price = pitValues.First().UserPrice;
            dates.Add(pitValues.First().TimeStamp.Date);
            dates.Reverse();
            divAddedValue.Reverse();

            var points = new List<ValuePointDto>();
            for (int i = -1; i < dates.Count - 1; i++)
            {
                var point = i == -1 ? new ValuePointDto(price, dateFrom) : CreatePoint(price, pitValues, dates[i], dates[i + 1], divAddedValue[i]);
                points.Add(point);
                price = point.RelativeValue;
            }

            AddQuantity(points, transactions);
            AddTotalValue(points, transactions);

            return points;

            DateTime SubtractInterval(DateTime d)
            {
                switch (interval)
                {
                    case PerformanceInterval.Week:
                        return d.AddDays(-7);
                    case PerformanceInterval.Month:
                        return d.AddMonths(-1);
                    case PerformanceInterval.Quarter:
                        return d.AddMonths(-3);
                    default:
                        throw new ArgumentOutOfRangeException($"Unsupported {nameof(interval)} {interval}");
                }
            }
        }

        private static void AddQuantity(List<ValuePointDto> points, List<Transaction> transactions)
        {
            foreach (var point in points)
                point.Quantity = Quantity(transactions, point.Date);
        }

        private static double Quantity(List<Transaction> transactions, DateTime date) => transactions.Where(t => t.StockValue.TimeStamp.Date <= date).Sum(t => t.Quantity);

        private static void AddTotalValue(List<ValuePointDto> points, List<Transaction> transactions)
        {
            foreach (var point in points)
                point.TotalValue = point.Quantity * point.RelativeValue;
        }

        private static ValuePointDto CreatePoint(double value, List<PitStockValue> pitValues, DateTime date, DateTime nextDate, double dividendPerShare = 0)
        {
            var inBetweenValues = pitValues.Where(p => p.TimeStamp.Date > date && p.TimeStamp < nextDate).ToList();
            foreach (var pit in inBetweenValues)
            {
                value = GrowthHelper.FuturePrice(value, pit.DailyGrowth, (pit.TimeStamp.Date.Date - date).Days);
                date = pit.TimeStamp.Date.Date;
            }

            var pv = pitValues.FirstOrDefault(p => p.TimeStamp.Date >= nextDate) ?? pitValues.OrderByDescending(p => p.TimeStamp).First();

            return new ValuePointDto(GrowthHelper.FuturePrice(value + dividendPerShare, pv.DailyGrowth, (nextDate - date).Days), nextDate, dividendPerShare);
        }
    }

    public class ValuePointDto
    {
        public double Quantity { get; set; }
        public double TotalValue { get; set; }
        public double RelativeValue { get; set; }
        public DateTime Date { get; }
        /// <summary>
        /// Per share
        /// </summary>
        public double Dividend { get; set; }

        public ValuePointDto(double relativeValue, DateTime date, double dividend = 0)
        {
            RelativeValue = relativeValue;
            Date = date;
            Dividend = dividend;
        }
    }

    public enum PerformanceInterval
    {
        None,
        Week,
        Month,
        Quarter,
    }
}