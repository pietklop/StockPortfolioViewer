using System;
using DAL.Entities;

namespace Services.Helpers
{
    public static class GrowthHelper
    {
        public static double DailyGrowth(PitStockValue pit0, PitStockValue pit1) => DailyGrowth(pit1.UserPrice / pit0.UserPrice, pit0.TimeStamp.Date, pit1.TimeStamp.Date);

        public static double DailyGrowth(double performance, DateTime dateStart, DateTime dateEnd)
        {
            var nDays = Math.Round((dateEnd - dateStart).TotalDays);
            if (nDays < 0) throw new Exception($"{nameof(dateEnd)} 2 must have a date later in time");
            if (nDays == 0) return 1;

            return Math.Pow(performance, 1 / nDays);
        }

        public static double AnnualPerformance(double performance, DateTime dateStart, DateTime dateEnd)
        {
            var years = (dateEnd - dateStart).TotalDays / 365;
            return performance / years;
        }

        public static double ExpectedPerformance(double annualPerformance, DateTime dateStart, DateTime dateEnd)
        {
            var now = DateTime.Today;
            var dailyGrowth = DailyGrowth(annualPerformance, now, now.AddDays(365));

            return Math.Pow(dailyGrowth, (dateEnd.Date - dateStart.Date).Days);
        }

        public static double PastPrice(this PitStockValue pitStockValue, int nDaysBack) => pitStockValue.UserPrice / Math.Pow(pitStockValue.DailyGrowth, nDaysBack);
    }
}