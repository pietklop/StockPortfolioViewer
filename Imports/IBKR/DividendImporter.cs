using Messages.Dtos;
using System.Collections.Generic;
using System.Linq;
using Core;
using Imports.DeGiro;

namespace Imports.IBKR
{
    public static class DividendImporter
    {
        private const int currencyColIndex = 0;
        private const int nameColIndex = 2;
        private const int isinColIndex = 3;
        private const int exDivDateColIndex = 4;
        private const int grossValueColIndex = 10;
        private const int actionIdColIndex = 12;

        public static TransactionImportDto Import(string[] lines)
        {
            var unfilteredDividends = new List<DividendDto>();

            foreach (var line in lines.Skip(1))
            {
                var fields = line.SmartSplit('|');
                unfilteredDividends.Add(CreateDividend(fields));
            }

            var dividends = new List<DividendDto>();
            foreach (var div in unfilteredDividends)
            {
                if (dividends.Any(d => d.Isin == div.Isin)) continue;
                var largestDiv = unfilteredDividends.Where(d => d.Isin == div.Isin).OrderByDescending(d => d.Value).First();
                dividends.Add(largestDiv);
            }

            return new TransactionImportDto(new List<TransactionDto>(), dividends);
        }

        private static DividendDto CreateDividend(string[] fields)
        {
            var currency = fields[currencyColIndex];
            var value = fields[grossValueColIndex].ToDouble();

            return new DividendDto
            {
                Isin = fields[isinColIndex],
                TimeStamp = fields[exDivDateColIndex].ExtractDate(),
                Currency = currency,
                Value = value,
                CurrencyRatio = currency == Constants.UserCurrency ? 1 : 0,
                Tax = value * 0.15,
                Costs = 0,
                IsCapitalReturn = false,
                ExtRef = fields[actionIdColIndex],
            };
        }
    }
}