using System;
using System.Collections.Generic;
using System.Linq;
using Messages.Dtos;

namespace Imports.DeGiro
{
    public static class StockValueImporter
    {
        private const int nameColIndex = 0;
        private const int isinColIndex = 1;
        private const int priceColIndex = 3;
        private const int currColIndex = 4;
        private const int valueColIndex = 5;
        private const int valueInUserCurrColIndex = 6;

        public static StockValueImportDto Import(string[] lines)
        {
            var stockValues = new List<StockValueDto>(lines.Length-1);

            foreach (var line in lines.Skip(1))
            {
                var fields = line.SmartSplit(',');
                if (fields[isinColIndex] == "") continue;

                var stockVal = new StockValueDto();

                stockVal.Name = fields[nameColIndex];
                stockVal.Isin = fields[isinColIndex];
                stockVal.TimeStamp = DateTime.Today.AddDays(-1).AddHours(20); // end of yesterday
                stockVal.ClosePrice = fields[priceColIndex].ToDouble();
                stockVal.Currency = fields[currColIndex];
                if (stockVal.Currency == "GBX") 
                    stockVal.Currency = "GBP";

                var valueUsd = fields[valueColIndex].ToDouble();
                var valueUserCurr = fields[valueInUserCurrColIndex].ReplaceComma().ToDouble();

                stockVal.CurrencyRatio = valueUsd / valueUserCurr;

                stockValues.Add(stockVal);
            }

            return new StockValueImportDto(stockValues);
        }
    }
}