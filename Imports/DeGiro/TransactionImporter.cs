using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Core;
using log4net;
using Messages.Dtos;

namespace Imports.DeGiro
{
    public static class TransactionImporter
    {
        private const int dateColIndex = 0;
        private const int nameColIndex = 3;
        private const int isinColIndex = 4;
        private const int actionColIndex = 5;
        private const int currencyColIndex = 7;
        private const int valueColIndex = 8;
        private const int guidColIndex = 11;

        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public static TransactionImportDto Import(string[] lines, bool debugMode)
        {
            var transactions = new List<TransactionDto>();
            var dividends = new List<DividendDto>();
            var currConversions = new List<CurrencyConvert>();

            var fieldsList = new List<string[]>(lines.Length-1);
            foreach (var line in lines.Skip(1))
                fieldsList.Add(line.SmartSplit(','));

            MakeSureTaxFollowsDividend(fieldsList);

            foreach (var fields in fieldsList)
            {
                var lineType = GetLineType(fields[actionColIndex], debugMode);
                switch (lineType)
                {
                    case LineType.Buy:
                    case LineType.Sell:
                        ProcessBuySell(lineType, fields, ref transactions);
                        break;
                    case LineType.CurrencyConversion:
                        ProcessCurrencyConversion(fields, ref transactions, ref currConversions);
                        break;
                    case LineType.CapitalReturn:
                    case LineType.Dividend:
                        dividends.Add(ProcessDividend(fields, lineType == LineType.CapitalReturn));
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
                        var msg = $"Not supported LineType: {lineType}";
                        if (debugMode)
                            throw new ArgumentOutOfRangeException(msg);
                        else
                            log.Warn(msg);
                        break;
                }
            }

            EnrichDividendData(ref dividends, currConversions);

            return new TransactionImportDto(transactions, dividends);
        }

        /// <summary>
        /// Make sure the tax line comes after the dividend line
        /// </summary>
        private static void MakeSureTaxFollowsDividend(List<string[]> fieldsList)
        {
            var divTaxDict = new Dictionary<string, int>(); // <isin, index>
            for (int i = 0; i < fieldsList.Count; i++)
            {
                var lineType = GetLineType(fieldsList[i][actionColIndex], false);
                var isin = fieldsList[i][isinColIndex];
                if (lineType == LineType.DividendTax)
                {
                    divTaxDict[isin] = i;
                    continue;
                }
                if (lineType != LineType.Dividend)
                    continue;
                if (!divTaxDict.ContainsKey(isin)) continue;
                // div tax is already listed => wrong order
                var divTaxIndex = divTaxDict[isin];
                // swap te order
                (fieldsList[i], fieldsList[divTaxIndex]) = (fieldsList[divTaxIndex], fieldsList[i]);
            }
        }

        private static void EnrichDividendData(ref List<DividendDto> dividends, List<CurrencyConvert> currConversions) => 
            dividends.ForEach(d => DividendCostsCalc.EnrichDividend(d, currConversions));

        private static void ProcessDividendTax(string[] fields, ref List<DividendDto> dividends)
        {
            var date = ExtractTimestamp(fields[dateColIndex]);
            var dividend = dividends.SingleOrDefault(d => d.Isin == fields[isinColIndex] && d.TimeStamp.Date == date.Date)
                           ?? throw new Exception($"Could not calc dividend tax for {fields[nameColIndex]}");
            dividend.Tax = Math.Abs(fields[valueColIndex].ToDouble());
        }

        private static void ProcessTransactionCosts(string[] fields, ref List<TransactionDto> transactions)
        {
            var transact = GetOrCreateTransaction(fields, ref transactions);
            transact.Costs += Math.Abs(fields[valueColIndex].ToDouble());
        }

        private static DividendDto ProcessDividend(string[] fields, bool capitalReturn)
        {
            return new DividendDto
            {
                Currency = fields[currencyColIndex],
                Isin = fields[isinColIndex],
                TimeStamp = ExtractTimestamp(fields[dateColIndex]),
                Value = fields[valueColIndex].ToDouble(),
                CurrencyRatio = fields[currencyColIndex] == Constants.UserCurrency ? 1 : 0, // ratio will be determined later because it is not provided in the same line
                IsCapitalReturn = capitalReturn,
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

        private static void ProcessCurrencyConversion(string[] fields, ref List<TransactionDto> transactions, ref List<CurrencyConvert> currConversions)
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

        private static void ProcessBuySell(LineType lineType, string[] fields, ref List<TransactionDto> transactions)
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

        private static LineType GetLineType(string actionField, bool debugMode)
        {
            // exact matches
            if (actionField == "")
                return LineType.Na;
            if (actionField == "ADR/GDR Externe Kosten")
                return LineType.Na;
            if (actionField == "DEGIRO transactiekosten")
                return LineType.TransactionCosts;
            if (actionField == "DEGIRO Transactiekosten en/of kosten van derden")
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
            if (actionField == "Kapitaalsuitkering")
                return LineType.CapitalReturn;
            if (actionField == "iDEAL storting")
                return LineType.Na;
            if (actionField == "iDEAL Deposit")
                return LineType.Na;
            if (actionField == "Reservation iDEAL / Sofort Deposit")
                return LineType.Na;
            if (actionField == "Terugstorting")
                return LineType.Na;
            if (actionField == "Rente")
                return LineType.Na;

            // starts with...
            if (actionField.StartsWith("DEGIRO Aansluitingskosten"))
                return LineType.Na;
            if (actionField.StartsWith("Flatex Interest"))
                return LineType.Na;
            if (actionField.StartsWith("Giro Exchange Connection Fee"))
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
            if (actionField.StartsWith("Overboeking "))
                return LineType.Na;

            if (debugMode)
                throw new Exception($"Unknown action: '{actionField}'");
            
            return LineType.Unknown;
        }
    }
}