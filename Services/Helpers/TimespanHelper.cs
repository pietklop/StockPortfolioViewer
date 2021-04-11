using System;

namespace Services.Helpers
{
    public static class TimespanHelper
    {
        public static string TimeAgo(this TimeSpan timespan)
        {
            if (timespan.TotalDays > 2) return $"{Math.Round(timespan.TotalDays)} days ago";
            if (timespan.TotalHours > 2) return $"{Math.Round(timespan.TotalHours)} hours ago";
            return $"{Math.Round(timespan.TotalMinutes)} min ago";
        }
    }
}