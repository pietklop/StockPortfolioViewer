using System;

namespace Core
{
    public enum DividendPayoutInterval
    {
        Unknown,
        /// <summary>
        /// No or temporary no payout
        /// </summary>
        GrowthStock,
        Accumulated,
        PerMonth,
        Quarterly,
        Per6Months,
        Annually,
    }

    public static class DividendPayoutIntervalHelper
    {
        public static double ToYearMultiplier(this DividendPayoutInterval interval)
        {
            switch (interval)
            {
                case DividendPayoutInterval.PerMonth:
                    return 12;
                case DividendPayoutInterval.Quarterly:
                    return 4;
                case DividendPayoutInterval.Per6Months:
                    return 2;
                case DividendPayoutInterval.Annually:
                    return 1;
                default:
                    throw new ArgumentOutOfRangeException($"Unsupported interval: {interval}");
            }
        }
    }
}