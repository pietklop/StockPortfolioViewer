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
            bool fromStart = dateFrom == null;
            dateFrom ??= DateTime.MinValue;
            dateTo ??= DateTime.Now;
            var pitValues = db.PitStockValues
                .Include(p => p.Stock).ThenInclude(s => s.Dividends)
                .Include(p => p.Stock).ThenInclude(s => s.Transactions).ThenInclude(t => t.StockValue)
                .Where(p => isin == null || p.Stock.Isin == isin)
                .Where(p => p.TimeStamp > dateFrom && p.TimeStamp.Date <= dateTo.Value.Date)
                .OrderBy(p => p.TimeStamp).ToList();

            var points = new List<ValuePointDto>();

            dateFrom = pitValues.First().TimeStamp.Date;
            var date = pitValues.Last().TimeStamp.Date;
            var stock = pitValues.First().Stock;
            var transactions = stock.Transactions.ToList();
            var divsToAdd = new List<Dividend>();
            divsToAdd.AddRange(stock.Dividends.ToList());
            var dates = new List<DateTime>();
            while (date > dateFrom)
            {
                var nextDate = SubtractInterval(date);;
                dates.Add(date);
                date = nextDate;
            }
            var price = pitValues.First().UserPrice;
            dates.Add(pitValues.First().TimeStamp.Date);
            dates.Reverse();

            for (int i = -1; i < dates.Count -1; i++)
            {
                var point = i == -1 ? new ValuePointDto(price, dateFrom.Value) : CreatePoint(price, pitValues, dates[i], dates[i+1]);
                points.Add(point);
                price = point.RelativeValue;
            }

            AddQuantity(points, transactions);
            AddTotalValue(points, transactions);

            return ScalePointsForGraph();

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

            List<ValuePointDto> ScalePointsForGraph()
            {   // make sure first value of range is 100
                var baseValue = points.First().RelativeValue;
                points.ForEach(p => p.RelativeValue *=100 / baseValue);

                return points;
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

        private static ValuePointDto CreatePoint(double value, List<PitStockValue> pitValues, DateTime date, DateTime nextDate, double addedValuePerShare = 0)
        {
            var inBetweenValues = pitValues.Where(p => p.TimeStamp.Date > date && p.TimeStamp < nextDate).ToList();
            foreach (var pit in inBetweenValues)
            {
                value = GrowthHelper.FuturePrice(value, pit.DailyGrowth, (pit.TimeStamp.Date.Date - date).Days);
                date = pit.TimeStamp.Date.Date;
            }
            var pv = pitValues.FirstOrDefault(p => p.TimeStamp.Date >= nextDate) ?? throw new Exception($"No pitValue found");

            return new ValuePointDto(GrowthHelper.FuturePrice(value + addedValuePerShare, pv.DailyGrowth, (nextDate - date).Days), nextDate);
        }
    }

    public class ValuePointDto
    {
        public double Quantity { get; set; }
        public double TotalValue { get; set; }
        public double RelativeValue { get; set; }
        public DateTime Date { get; }

        public ValuePointDto(double relativeValue, DateTime date)
        {
            RelativeValue = relativeValue;
            Date = date;
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