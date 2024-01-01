using System;
using DAL.Entities;

namespace Services.Helpers
{
    public static class GrowthHelper
    {
        public static double DailyGrowth(PitStockValue pit0, PitStockValue pit1) => DailyGrowth(pit1.UserPrice / pit0.UserPrice, pit0.TimeStamp.Date, pit1.TimeStamp.Date);

        public static double DailyGrowth(double performance, DateTime dateStart, DateTime dateEnd)
        {
            var nDays = (int)(dateEnd.Date - dateStart.Date).TotalDays;
            if (nDays < 0) throw new Exception($"{nameof(dateEnd)} 2 must have a date later in time");

            return DailyGrowth(performance, nDays);
        }

        public static double DailyGrowth(double performance, int nDays)
        {
            if (nDays < 0) throw new Exception($"Date diff can not be negative");
            if (nDays <= 1) return performance;

            return Math.Pow(performance, 1d / nDays);
        }

        public static double Performance(double dailyPerformance, int nDays) => Math.Pow(dailyPerformance, nDays);

        public static double AnnualPerformance(double dailyPerformance) => Performance(dailyPerformance, 365);

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

        public static double PastPrice(double price, double dailyGrowth, int nDaysBack) => price / Math.Pow(dailyGrowth, nDaysBack);
        public static double FuturePrice(double price, double dailyGrowth, int nDays) => price * Math.Pow(dailyGrowth, Math.Max(nDays, 1));
    }
}