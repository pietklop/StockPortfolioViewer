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
            while (date > dateFrom)
            {
                points.Add(CreatePoint(pitValues, date));
                date = SubtractInterval(date);
            }
            if (fromStart) points.Add(CreatePoint(pitValues, dateFrom.Value));

            var stock = pitValues.First().Stock;
            points.Reverse(0, points.Count);
            var transactions = stock.Transactions.ToList();
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
                point.Quantity = Quantity(point.Date);

            double Quantity(DateTime date) => transactions.Where(t => t.StockValue.TimeStamp.Date <= date).Sum(t => t.Quantity);
        }

        private static void AddTotalValue(List<ValuePointDto> points, List<Transaction> transactions)
        {
            foreach (var point in points)
                point.TotalValue = point.Quantity * point.RelativeValue;
        }

        private static ValuePointDto CreatePoint(List<PitStockValue> pitValues, DateTime date)
        {
            var pv = pitValues.FirstOrDefault(p => p.TimeStamp.Date >= date) ?? throw new Exception($"No pitValue found");
            return new ValuePointDto(pv.PastPrice((pv.TimeStamp - date).Days), date);
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