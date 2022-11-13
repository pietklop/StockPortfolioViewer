using System;
using System.Linq;

namespace Services.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetLatest(this DateTime dateTime, params DateTime[] others)
        {
            var largest = others.Max();
            return dateTime > largest ? dateTime : largest;
        }
    }
}