using System;

namespace Imports.IBKR
{
    public static class StringHelperIbkr
    {
        public static DateTime ExtractDate(this string dateField) => DateTime.ParseExact(dateField, "yyyyMMdd", null);
    }
}