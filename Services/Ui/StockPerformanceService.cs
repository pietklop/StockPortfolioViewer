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

        public List<ValuePointDto> GetValues(DateTime dateFrom, DateTime dateTo, out PerformanceInterval interval)
        {
            var allPitValues = GetAllPitStockValues(dateFrom, dateTo);
            var firstDate = allPitValues.First().TimeStamp.Date;
            var lastDate = allPitValues.Last().TimeStamp.Date;
            interval = DetermineInterval(firstDate, lastDate);
            dateFrom = AlignWithInterval(interval, firstDate, lastDate);

            var groupedPitValues = allPitValues.GroupBy(p => p.StockId).ToList();

            var dict = new Dictionary<DateTime, List<ValuePointDto>>();
            var dates = CreateIntervals(interval, dateFrom, lastDate);
            foreach (var pitStockValues in groupedPitValues)
            {
                var points = GetPoints(pitStockValues.ToList(), dates);
                for (int i = 0; i < points.Count; i++)
                {
                    var date = points[i].Date;
                    var growth = i == 0 ? 1 : points[i].RelativeValue / points[i-1].RelativeValue;
                    var vp = new ValuePointDto(growth, date, points[i].Dividend);
                    vp.TotalValue = points[i].TotalValue;
                    vp.Quantity = points[i].Quantity;
                    if (vp.Quantity <= 0) continue;
                    vp.Dividend = points[i].Dividend * vp.Quantity;
                    if (!dict.ContainsKey(date)) dict.Add(date, new List<ValuePointDto>());
                    dict[date].Add(vp);
                }
            }

            var resultList = new List<ValuePointDto>();
            var relativeValue = 1d;
            DateTime? previousDate = null;
            double inBetweenRv = 0; // weighed extrapolation to next interval date
            double inBetweenTotal = 0;
            foreach (var pointsPerDate in dict.Values.OrderBy(d => d.First().Date))
            {
                if (!dates.Contains(pointsPerDate.First().Date))
                {
                    if (previousDate == null)
                        continue;
                    var selectedPoints = pointsPerDate.Where(p => p.RelativeValue != 1);
                    var selectedTotal = selectedPoints.Sum(p => p.TotalValue);
                    var tot = selectedTotal * (pointsPerDate.First().Date - previousDate.Value).Days / interval.Days(); // weighs relative to length of the interval
                    if (tot == 0) continue;
                    var rv = selectedPoints.Sum(p => p.RelativeValue * p.TotalValue / selectedTotal);
                    var tt = tot + inBetweenTotal;
                    inBetweenRv = rv * tot / tt + inBetweenRv * inBetweenTotal / tt;
                    inBetweenTotal = tt;
                    continue;
                }

                var total = pointsPerDate.Sum(p => p.TotalValue);
                if (total <= 0) continue;
                var tmpTot = total + inBetweenTotal;
                relativeValue *= pointsPerDate.Sum(p => p.RelativeValue * p.TotalValue / total) * total / tmpTot + inBetweenRv * inBetweenTotal / tmpTot;
                var div = pointsPerDate.Sum(p => p.Dividend * p.Quantity);
                var dto = new ValuePointDto(relativeValue, pointsPerDate.First().Date, div);
                dto.TotalValue = tmpTot;
                resultList.Add(dto);
                previousDate = pointsPerDate.First().Date;
                inBetweenRv = 0;
                inBetweenTotal = 0;
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

        public List<ValuePointDto> GetValues(string isin, DateTime dateFrom , DateTime dateTo, out PerformanceInterval interval)
        {
            if (isin == null) return GetValues(dateFrom, dateTo, out interval);

            bool fromStart = dateFrom == null;
            var allPitValues = GetAllPitStockValues(dateFrom, dateTo, new []{isin});
            var pitValues = allPitValues.GroupBy(p => p.StockId).First().ToList();
            var firstDate = pitValues.First().TimeStamp.Date;
            var lastDate = pitValues.Last().TimeStamp.Date;
            interval = DetermineInterval(firstDate, lastDate);
            var dates = CreateIntervals(interval, firstDate, dateTo);
            List<ValuePointDto> points = GetPoints(pitValues, dates);

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

        private List<DateTime> CreateIntervals(PerformanceInterval interval, DateTime dateFrom, DateTime dateTo)
        {
            var date = dateTo;
            var dates = new List<DateTime>();

            while (date > dateFrom && date > dateFrom)
            {
                var nextDate = SubtractInterval(interval, date);
                dates.Add(date);
                date = nextDate;
            }

            dates.Reverse();

            return dates;
        }

        private List<ValuePointDto> GetPoints(List<PitStockValue> pitValues, List<DateTime> dates)
        {
            var stock = pitValues.First().Stock;
            var transactions = stock.Transactions.ToList();
            var divsToAdd = stock.Dividends.ToList();
            var divAddedValue = new List<double>();
            var firstDate = pitValues.First().TimeStamp.Date;
            var lastDate = pitValues.Last().TimeStamp.Date;

            var adjustedDates = new List<DateTime>();
            adjustedDates.AddRange(dates);
            adjustedDates.RemoveAll(d => d.Date <= firstDate);
            adjustedDates.Insert(0, firstDate);
            adjustedDates.RemoveAll(d => d.Date >= lastDate);
            adjustedDates.Add(lastDate);

            for (int i = adjustedDates.Count - 1; i >= 0; i--)
            {
                var date = adjustedDates[i];
                var divsToAddThisSpan = new List<Dividend>();
                divsToAddThisSpan.AddRange(i == 0 ? divsToAdd : divsToAdd.Where(d => d.TimeStamp.Date >= adjustedDates[i-1].Date).ToList());
                if (divsToAddThisSpan.Any())
                {
                    var quantity = Quantity(transactions, date);
                    divAddedValue.Add(divsToAddThisSpan.Sum(d => d.UserValue) / quantity);
                    divsToAddThisSpan.ForEach(d => divsToAdd.Remove(d));
                }
                else
                    divAddedValue.Add(0);
            }

            var price = pitValues.First().UserPrice;
            divAddedValue.Reverse();

            var points = new List<ValuePointDto>();
            for (int i = -1; i < adjustedDates.Count - 1; i++)
            {
                var point = i == -1 ? new ValuePointDto(price, firstDate) : CreatePoint(price, pitValues, adjustedDates[i], adjustedDates[i + 1], divAddedValue[i]);
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

        private static double Quantity(List<Transaction> transactions, DateTime date)
        {
            var trs = transactions.Where(t => t.StockValue.TimeStamp.Date <= date).ToList();
            if (trs.Any() && trs.Last().Quantity < 0 && trs.Last().StockValue.TimeStamp.Date == date)
                trs.RemoveAt(trs.Count-1); // a sell means you had them till that date
            return trs.Sum(t => t.Quantity);
        }

        private static void AddTotalValue(List<ValuePointDto> points, List<Transaction> transactions)
        {
            foreach (var point in points)
                point.TotalValue = point.Quantity * point.RelativeValue;
        }

        private static ValuePointDto CreatePoint(double value, List<PitStockValue> pitValues, DateTime date, DateTime nextDate, double dividendPerShare = 0)
        {
            var inBetweenValues = pitValues.Where(p => p.TimeStamp.Date > date && p.TimeStamp < nextDate).ToList();
            // DailyGrowth is not used from DB but recalculated
            for (int i = 0; i < pitValues.Count - 1; i++)
            {
                var v = GrowthHelper.DailyGrowth(pitValues[i], pitValues[i+1]);
                pitValues[i+1].DailyGrowth = v;
            }

            foreach (var pit in inBetweenValues)
            {
                value = GrowthHelper.FuturePrice(value, pit.DailyGrowth, (pit.TimeStamp.Date.Date - date).Days);
                date = pit.TimeStamp.Date.Date;
            }

            if (date != nextDate)
            {
                // determine the value for this date by interpolation
                var lastUsedPitValue = inBetweenValues.LastOrDefault() ?? pitValues.OrderByDescending(p => p.TimeStamp).FirstOrDefault(p => p.TimeStamp < nextDate);
                var firstAfter = pitValues.FirstOrDefault(p => p.TimeStamp.Date >= nextDate) ?? pitValues.FirstOrDefault(p => p.TimeStamp.Date > lastUsedPitValue.TimeStamp.Date);
                if (firstAfter == null)
                {   // no data point available later or equal to nextDate
                    var dayDiff = (lastUsedPitValue.TimeStamp.Date - date).Days;
                    if (dayDiff > 0) value = GrowthHelper.FuturePrice(value, lastUsedPitValue.DailyGrowth, dayDiff);
                    return new ValuePointDto(value + dividendPerShare, lastUsedPitValue.TimeStamp.Date, dividendPerShare);
                }
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

    public static class PerformanceIntervalHelper
    {
        public static int Days(this PerformanceInterval interval)
        {
            switch (interval)
            {
                case PerformanceInterval.Week:
                    return 7;
                case PerformanceInterval.Month:
                    return 30;
                case PerformanceInterval.Quarter:
                    return 91;
                default:
                    throw new ArgumentOutOfRangeException($"Unsupported interval {interval}");
            }
        }
    }
}