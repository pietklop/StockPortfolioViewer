using System;
using Services.Ui;

namespace Services.Helpers
{
    public static class PerformanceIntervalHelper
    {
        public static TimeSpan ToTimeSpan(this PerformanceInterval interval)
        {
            switch (interval)
            {
                case PerformanceInterval.Week:
                    return TimeSpan.FromDays(7);
                case PerformanceInterval.Month:
                    return TimeSpan.FromDays(30);
                case PerformanceInterval.Quarter:
                    return TimeSpan.FromDays(91);
                case PerformanceInterval.Year:
                    return TimeSpan.FromDays(365);
                default:
                    throw new ArgumentOutOfRangeException(nameof(interval), interval, null);
            }
        }
    }
}