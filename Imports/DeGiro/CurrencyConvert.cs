using System;
using System.Collections.Generic;
using System.Linq;

namespace Imports.DeGiro
{
    public class CurrencyConvert
    {
        public string Currency { get; }
        public DateTime Date { get; }
        public double RatioPerUserCurr { get; }

        public CurrencyConvert(string currency, DateTime date, double ratioPerUserCurr)
        {
            Currency = currency;
            Date = date;
            RatioPerUserCurr = ratioPerUserCurr;
        }
    }

    public static class CurrencyConvertHelper
    {
        public static CurrencyConvert GetClosestBy(this List<CurrencyConvert> list, string currency, DateTime date)
        {
            var currList = list.Where(l => l.Currency == currency).ToList();
            if (currList.Count == 0) throw new Exception($"Could not find currency ratio for {currency}");

            var closest = currList.OrderBy(c => Math.Abs((c.Date.Date - date.Date).TotalDays)).FirstOrDefault();
            if (Math.Abs((closest.Date.Date - date.Date).TotalDays) > 5)
                throw new Exception($"Could not find ratio for {currency} close to {date.ToShortDateString()}");

            return closest;
        }
    }
}