using Messages.Dtos;
using System.Collections.Generic;
using System.Linq;
using System;
using Core;
using Imports.DeGiro;

namespace Imports.IBKR
{
    public static class IbkrStockValueImporter
    {
        private const int currencyColIndex = 0;
        private const int nameColIndex = 2;
        private const int isinColIndex = 3;
        private const int dateColIndex = 4;
        private const int quantityColIndex = 5;
        private const int valueColIndex = 6;
        private const int priceColIndex = 7;

        public static StockValueImportDto Import(string[] lines)
        {
            var stockValues = new List<StockValueDto>(lines.Length - 1);

            var fieldsList = new List<string[]>(lines.Length - 1);
            foreach (var line in lines.Skip(1))
            {
                fieldsList.Add(line.SmartSplit('|'));
            }

            var lineNr = 0;
            for (; lineNr < fieldsList.Count; lineNr++)
            {
                var fields = fieldsList[lineNr];

                var name = fields[nameColIndex];
                if (name.Contains("DIVIDEND RIGHTS"))
                    continue; // skip, is not really a position

                if (fields[0] == TradeImporter.currencyFirstColumnHeader)
                    break;

                var stockVal = new StockValueDto();

                stockVal.Name = name;
                stockVal.Isin = fields[isinColIndex];
                stockVal.TimeStamp = fields[dateColIndex].ExtractDate().AddHours(20); // end of day
                stockVal.ClosePrice = fields[priceColIndex].ToDouble();
                stockVal.Currency = fields[currencyColIndex];
                stockVal.Quantity = fields[quantityColIndex].ToDouble();

                stockValues.Add(stockVal);
            }

            var currConversions = CurrencyHandler.ParseCurrencyData(fieldsList.Skip(lineNr).ToList());

            ApplyCurrencyDataOnPositions(ref stockValues, currConversions);

            return new StockValueImportDto(stockValues);
        }

        private static void ApplyCurrencyDataOnPositions(ref List<StockValueDto> stockValues, List<CurrencyConvert> currConversions)
        {
            foreach (var stockValue in stockValues)
            {
                if (stockValue.Currency == Constants.UserCurrency)
                {
                    stockValue.CurrencyRatio = 1;
                    continue;
                }

                var curr = currConversions.SingleOrDefault(c => c.Currency == stockValue.Currency)
                           ?? throw new Exception($"Could not find matching currency for '{stockValue.Name}'");

                stockValue.CurrencyRatio = curr.RatioPerUserCurr;
            }
        }
    }

}