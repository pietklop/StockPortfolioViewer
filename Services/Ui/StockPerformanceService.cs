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

        public List<ValuePointDto> GetValues(DateTime dateFrom, DateTime dateTo)
        {
            var allPitValues = GetAllPitStockValues(dateFrom, dateTo);
            var firstDate = allPitValues.First().TimeStamp.Date;
            var lastDate = allPitValues.Last().TimeStamp.Date;
            var interval = DetermineInterval(firstDate, lastDate);
            dateFrom = AlignWithInterval(interval, firstDate, dateTo);

            var groupedPitValues = allPitValues.GroupBy(p => p.StockId).ToList();

            var dict = new Dictionary<DateTime, List<ValuePointDto>>();
            foreach (var pitStockValues in groupedPitValues)
            {
                var p = GetPoints(pitStockValues.ToList(), interval, dateFrom, dateTo);
                for (int i = 0; i < p.Count; i++)
                {
                    var date = p[i].Date;
                    var growth = i == 0 ? 1 : p[i].RelativeValue / p[i-1].RelativeValue;
                    var vp = new ValuePointDto(growth, date, p[i].Dividend);
                    vp.TotalValue = p[i].TotalValue;
                    vp.Quantity = p[i].Quantity;
                    vp.Dividend = p[i].Dividend * vp.Quantity;
                    if (!dict.ContainsKey(date)) dict.Add(date, new List<ValuePointDto>());
                    dict[date].Add(vp);
                }
            }

            var resultList = new List<ValuePointDto>();
            var relativeValue = 1d;
            foreach (var pointsPerDate in dict.Values.OrderBy(d => d.First().Date))
            {
                var total = pointsPerDate.Sum(p => p.TotalValue);
                if (total <= 0) continue;
                relativeValue *= pointsPerDate.Sum(p => p.RelativeValue * p.TotalValue / total);
                var div = pointsPerDate.Sum(p => p.Dividend * p.Quantity);
                var dto = new ValuePointDto(relativeValue, pointsPerDate.First().Date, div);
                dto.TotalValue = total;
                resultList.Add(dto);
            }

            return ScalePointsForGraph(resultList);
        }

        private static DateTime AlignWithInterval(PerformanceInterval interval, DateTime firstDate, DateTime dateTo)
        {
            var date = dateTo;

            while (true)
            {
                var returnDate = date;
                date = SubtractInterval(interval, date);
                if (date < firstDate)
                    return returnDate;
            }
        }

        public List<ValuePointDto> GetValues(string isin, DateTime dateFrom , DateTime dateTo)
        {
            if (isin == null) return GetValues(dateFrom, dateTo);

            bool fromStart = dateFrom == null;
            var allPitValues = GetAllPitStockValues(dateFrom, dateTo, new []{isin});
            var pitValues = allPitValues.GroupBy(p => p.StockId).First().ToList();

            var firstDate = pitValues.First().TimeStamp.Date;
            var lastDate = pitValues.Last().TimeStamp.Date;
            var interval = DetermineInterval(firstDate, lastDate);
            List<ValuePointDto> points = GetPoints(pitValues, interval, firstDate, lastDate);

            return ScalePointsForGraph(points);
        }

        private PerformanceInterval DetermineInterval(DateTime dateFirst, DateTime dateLast)
        {
            int maxDataPointsXaxis = 40;
            var nDays = (dateLast - dateFirst).TotalDays;
            if (nDays / 7 < maxDataPointsXaxis) return PerformanceInterval.Week;
            if (nDays / 30 < maxDataPointsXaxis) return PerformanceInterval.Month;
            return PerformanceInterval.Quarter;
        }

        private List<ValuePointDto> ScalePointsForGraph(List<ValuePointDto> points)
        {   // make sure first value of range is 100
            var baseValue = points.First().RelativeValue;
            points.ForEach(p => p.RelativeValue *= 100 / baseValue);
            
            var maxDiv = points.Max(p => p.Dividend);
            if (maxDiv > 0)
                points.ForEach(p => p.Dividend *= 50 / maxDiv); // max div is scaled to 50% in graph
            
            var maxTotal = points.Max(p => p.TotalValue);
            if (maxTotal > 0)
                points.ForEach(p => p.TotalValue *= 100 / maxTotal); // max total is scaled to 100% in graph

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
            var date = dateTo;
            var stock = pitValues.First().Stock;
            var transactions = stock.Transactions.ToList();
            var divsToAdd = stock.Dividends.ToList();
            var dates = new List<DateTime>();
            var divAddedValue = new List<double>();
            while (date > dateFrom)
            {
                var nextDate = SubtractInterval(interval, date);
                
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
        }

        private static DateTime SubtractInterval(PerformanceInterval interval, DateTime d)
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

            if (date != nextDate)
            {
                // determine the value for this date by interpolation
                var lastUsedPitValue = inBetweenValues.Last();
                var firstAfter = pitValues.First(p => p.TimeStamp.Date >= nextDate);
                var dailyGrowth = GrowthHelper.DailyGrowth(firstAfter.UserPrice / lastUsedPitValue.UserPrice, lastUsedPitValue.TimeStamp,  firstAfter.TimeStamp);
                int nDays = (int)(nextDate - date).TotalDays;
                value = GrowthHelper.FuturePrice(value, dailyGrowth, nDays);
            }

            return new ValuePointDto(value+dividendPerShare, nextDate, dividendPerShare);
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