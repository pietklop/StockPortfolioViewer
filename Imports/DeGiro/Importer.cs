using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core;
using Messages.Dtos;

namespace Imports.DeGiro
{
    public class Importer
    {
        private const int dateColIndex = 0;
        private const int nameColIndex = 3;
        private const int isinColIndex = 4;
        private const int actionColIndex = 5;
        private const int currencyColIndex = 7;
        private const int valueColIndex = 8;
        private const int guidColIndex = 11;

        public ImportDto Import(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var transactions = new List<TransactionDto>();
            var dividends = new List<DividendDto>();
            var currConversions = new List<CurrencyConvert>();

            foreach (var line in lines.Skip(1))
            {
                var fields = line.SmartSplit(',');
                var lineType = GetLineType(fields[actionColIndex]);
                switch (lineType)
                {
                    case LineType.Buy:
                    case LineType.Sell:
                        ProcessBuySell(lineType, fields, ref transactions);
                        break;
                    case LineType.CurrencyConversion:
                        ProcessCurrencyConversion(fields, ref transactions, ref currConversions);
                        break;
                    case LineType.Dividend:
                        dividends.Add(ProcessDividend(fields));
                        break;
                    case LineType.DividendTax:
                        ProcessDividendTax(fields, ref dividends);
                        break;
                    case LineType.CorporateActionCosts:
                        // not used by now, dividend costs are calculated and not extracted
                        break;
                    case LineType.TransactionCosts:
                        ProcessTransactionCosts(fields, ref transactions);
                        break;
                    case LineType.Na:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Not supported LineType: {lineType}");
                }
            }

            EnrichDividendData(ref dividends, currConversions);

            return new ImportDto(transactions, dividends);
        }

        private void EnrichDividendData(ref List<DividendDto> dividends, List<CurrencyConvert> currConversions) => 
            dividends.ForEach(d => DividendCostsCalc.EnrichDividend(d, currConversions));

        private void ProcessDividendTax(string[] fields, ref List<DividendDto> dividends)
        {
            var date = ExtractTimestamp(fields[dateColIndex]);
            var dividend = dividends.SingleOrDefault(d => d.Isin == fields[isinColIndex] && d.TimeStamp.Date == date.Date)
                           ?? throw new Exception($"Could not max dividend tax for {fields[nameColIndex]}");
            dividend.Tax = Math.Abs(fields[valueColIndex].ToDouble());
        }

        private void ProcessTransactionCosts(string[] fields, ref List<TransactionDto> transactions)
        {
            var transact = GetOrCreateTransaction(fields, ref transactions);
            transact.Costs += Math.Abs(fields[valueColIndex].ToDouble());
        }

        private DividendDto ProcessDividend(string[] fields)
        {
            return new DividendDto
            {
                Currency = fields[currencyColIndex],
                Isin = fields[isinColIndex],
                TimeStamp = ExtractTimestamp(fields[dateColIndex]),
                Value = fields[valueColIndex].ToDouble(),
                CurrencyRatio = fields[currencyColIndex] == Constants.UserCurrency ? 1 : 0, // ratio will be determined later because it is not provided in the same line
            };
        }

        private static TransactionDto GetOrCreateTransaction(string[] fields, ref List<TransactionDto> transactions)
        {
            string guid = fields[guidColIndex];
            var transaction = transactions.SingleOrDefault(t => t.Guid == guid);
            if (transaction == null)
            {
                transaction = new TransactionDto {Name = fields[nameColIndex], Isin = fields[isinColIndex], Guid = fields[guidColIndex]};
                transactions.Add(transaction);
            }

            return transaction;
        }

        private void ProcessCurrencyConversion(string[] fields, ref List<TransactionDto> transactions, ref List<CurrencyConvert> currConversions)
        {
            if (string.IsNullOrEmpty(fields[6]))
                return;
            var ratio = fields[6].ToDouble();
            if (!string.IsNullOrEmpty(fields[guidColIndex]))
            {
                var transact = GetOrCreateTransaction(fields, ref transactions);
                if (string.IsNullOrEmpty(fields[6])) return;
                if (fields[currencyColIndex] == Constants.UserCurrency) return;
                transact.CurrencyRatio = ratio;
            }
            if (fields[currencyColIndex] == Constants.UserCurrency) return;
            currConversions.Add(new CurrencyConvert(fields[currencyColIndex], ExtractTimestamp(fields[dateColIndex]), ratio));
        }

        private void ProcessBuySell(LineType lineType, string[] fields, ref List<TransactionDto> transactions)
        {
            var transact = GetOrCreateTransaction(fields, ref transactions);

            transact.Currency = fields[currencyColIndex];
            transact.TimeStamp = ExtractTimestamp(fields[dateColIndex]);
            var actionFields = fields[actionColIndex].Split(" ");
            if (lineType == LineType.Buy)
                transact.Quantity += actionFields[1].ToDouble();
            else
                transact.Quantity -= actionFields[1].ToDouble();
            transact.Price = actionFields[3].ToDouble();
        }

        private static DateTime ExtractTimestamp(string dateField)
        {
            var dateFields = dateField.Split("-");
            return new DateTime(int.Parse(dateFields[2]), int.Parse(dateFields[1]), int.Parse(dateFields[0]));
        }

        private static LineType GetLineType(string actionField)
        {
            // exact matches
            if (actionField == "")
                return LineType.Na;
            if (actionField == "DEGIRO transactiekosten")
                return LineType.TransactionCosts;
            if (actionField == "DEGIRO Corporate Action Kosten")
                return LineType.CorporateActionCosts;
            if (actionField == "Degiro Cash Sweep Transfer")
                return LineType.Na;
            if (actionField == "Dividend")
                return LineType.Dividend;
            if (actionField == "Dividendbelasting")
                return LineType.DividendTax;
            if (actionField == "EUR")
                return LineType.Na;
            if (actionField == "Flatex Cash Sweep Transfer")
                return LineType.Na;
            if (actionField == "iDEAL storting")
                return LineType.Na;
            if (actionField == "iDEAL Deposit")
                return LineType.Na;
            if (actionField == "Reservation iDEAL / Sofort Deposit")
                return LineType.Na;
            if (actionField == "Terugstorting")
                return LineType.Na;

            // starts with...
            if (actionField.StartsWith("DEGIRO Aansluitingskosten"))
                return LineType.Na;
            if (actionField.StartsWith("Valuta "))
                return LineType.CurrencyConversion;
            if (actionField.StartsWith("Koop"))
                return LineType.Buy;
            if (actionField.StartsWith("Verkoop"))
                return LineType.Sell;
            if (actionField.StartsWith("Conversie geldmarktfonds"))
                return LineType.Na;
            if (actionField.StartsWith("Koersverandering"))
                return LineType.Na;

            throw new Exception($"Unknown action: '{actionField}'");
        }
    }
}