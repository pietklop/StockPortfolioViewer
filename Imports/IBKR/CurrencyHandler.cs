using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Imports.DeGiro;

namespace Imports.IBKR
{
    public static class CurrencyHandler
    {
        private const int dateColIndex = 0;
        private const int fromCurrColIndex = 1;
        private const int eurColIndex = 2;
        private const int rateColIndex = 3;

        public static List<CurrencyConvert> ParseCurrencyData(List<string[]> lines)
        {
            var records = new List<CurrencyConvert>();

            if (lines.First().Length != 4) throw new Exception($"Expected 4 columns instead of {lines.First().Length}");

            foreach (var fields in lines)
            {
                if (fields[eurColIndex] != Constants.UserCurrency) continue;
                var date = fields[dateColIndex].ExtractDate();
                var fromCurr = fields[fromCurrColIndex];
                var rate = 1 / fields[rateColIndex].ToDouble();
                records.Add(new CurrencyConvert(fromCurr, date, rate));
            }

            return records;
        }
    }
}