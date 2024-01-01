using System;

namespace Dashboard.Helpers
{
    public static class DateHelper
    {
        public static string GetMonthShort(this DateTime date)
        {
            switch (date.Month)
            {
                case 1:
                    return "jan";
                case 2:
                    return "feb";
                case 3:
                    return "mar";
                case 4:
                    return "apr";
                case 5:
                    return "may";
                case 6:
                    return "jun";
                case 7:
                    return "jul";
                case 8:
                    return "aug";
                case 9:
                    return "sep";
                case 10:
                    return "oct";
                case 11:
                    return "nov";
                case 12:
                    return "dec";
                default:
                    throw new Exception($"Not existing month");
            }
        }

        public static string GetQuarterShort(this DateTime date)
        {
            if (date.Month <= 3) return "Q1";
            if (date.Month <= 6) return "Q2";
            if (date.Month <= 9) return "Q3";
            return "Q4";
        }
    }
}