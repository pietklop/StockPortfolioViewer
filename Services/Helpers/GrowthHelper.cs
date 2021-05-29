using System;
using DAL.Entities;

namespace Services.Helpers
{
    public static class GrowthHelper
    {
        public static double DailyGrowth(PitStockValue pit0, PitStockValue pit1)
        {
            var nDays = Math.Round((pit1.TimeStamp.Date - pit0.TimeStamp.Date).TotalDays);
            if (nDays < 0) throw new Exception($"Parameter 2 must have a date later in time ({pit0.Stock})");
            if (nDays == 0) return 1;

            var factor = pit1.UserPrice / pit0.UserPrice;
            return Math.Pow(factor, 1 / nDays);
        }

        public static double PastPrice(this PitStockValue pitStockValue, int nDaysBack) => pitStockValue.UserPrice / Math.Pow(pitStockValue.DailyGrowth, nDaysBack);
    }
}