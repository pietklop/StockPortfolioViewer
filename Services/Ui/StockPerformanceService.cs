using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Entities;
using log4net;
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
                .Where(t => isin == null || t.Stock.Isin == isin)
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

            points.Reverse(0, points.Count);

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
                var baseValue = points.First().Value;
                points.ForEach(p => p.Value *=100 / baseValue);

                return points;
            }
        }

        private ValuePointDto CreatePoint(List<PitStockValue> pitValues, DateTime date)
        {
            var pv = pitValues.FirstOrDefault(p => p.TimeStamp.Date >= date) ?? throw new Exception($"No pitValue found");
            return new ValuePointDto(pv.PastPrice((pv.TimeStamp - date).Days), date);
        }
    }

    public class ValuePointDto
    {
        public double Value { get; set; }
        public DateTime Date { get; }

        public ValuePointDto(double value, DateTime date)
        {
            Value = value;
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