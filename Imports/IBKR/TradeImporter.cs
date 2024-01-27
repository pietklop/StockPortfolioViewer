using Imports.DeGiro;
using Messages.Dtos;
using System.Collections.Generic;
using System.Linq;
using Core;
using System;

namespace Imports.IBKR
{
    public static class TradeImporter
    {
        public const string currencyFirstColumnHeader = "Date/Time";
        private const int nameColIndex = 0;
        private const int isinColIndex = 2;
        private const int currencyColIndex = 3;
        private const int priceColIndex = 4;
        private const int quantityColIndex = 6;
        private const int dateColIndex = 7;
        private const int assetTypeColIndex = 8;
        private const int actionColIndex = 9;
        private const int commissionCurrencyColIndex = 10;
        private const int commissionColIndex = 11;
        private const int tradeIdColIndex = 12;

        public static TransactionImportDto Import(string[] lines)
        {

            var transactions = new List<TransactionDto>();

            var fieldsList = new List<string[]>(lines.Length - 1);
            foreach (var line in lines.Skip(1))
                fieldsList.Add(line.SmartSplit('|'));

            var lineNr = 0;
            for (; lineNr < fieldsList.Count; lineNr++)
            {
                var fields = fieldsList[lineNr];
                if (fields[0] == currencyFirstColumnHeader)
                    break;

                MergeOrCreateTransaction(fields, ref transactions);
            }

            var currConversions = CurrencyHandler.ParseCurrencyData(fieldsList.Skip(lineNr).ToList());

            ApplyCurrencyDataOnTransactions(ref transactions, currConversions);

            return new TransactionImportDto(transactions, new List<DividendDto>());
        }

        private static void ApplyCurrencyDataOnTransactions(ref List<TransactionDto> transactions, List<CurrencyConvert> currConversions)
        {
            foreach (var transaction in transactions)
            {
                if (transaction.Currency == Constants.UserCurrency)
                {
                    transaction.CurrencyRatio = 1;
                    continue;
                }

                var curr = currConversions.SingleOrDefault(c => c.Date.Date == transaction.TimeStamp.Date && c.Currency == transaction.Currency)
                           ?? throw new Exception($"Could not find matching currency for trade(Id): {transaction.Guid}");

                transaction.CurrencyRatio = curr.RatioPerUserCurr;
                transaction.Costs *= curr.RatioPerUserCurr; // to user currency
            }
        }

        private static void MergeOrCreateTransaction(string[] fields, ref List<TransactionDto> transactions)
        {
            const string stock = "STK";

            if (!fields[tradeIdColIndex].HasValue()) return;
            if (fields[assetTypeColIndex] != stock) return;

            var isin = fields[isinColIndex];
            var date = fields[dateColIndex].ExtractDate();
            var price = fields[priceColIndex].ToDouble();
            var quantity = fields[quantityColIndex].ToDouble();
            var isBuy = IsBuy();
            if (isBuy == (quantity < 0)) throw new Exception($"Unexpected sign in quantity. TradeId: {fields[tradeIdColIndex]}");
            var costs = -fields[commissionColIndex].ToDouble();

            var transactionSameStockSameDate = transactions.SingleOrDefault(t => t.Isin == isin && t.TimeStamp.Date == date);

            if (transactionSameStockSameDate != null)
            {   // combine transactions
                if (isBuy == transactionSameStockSameDate.Quantity < 0) throw new Exception($"Can not buy and sell the same stock on the same date. TradeId: {fields[tradeIdColIndex]}");
                var newCombinedPrice = (transactionSameStockSameDate.Price * transactionSameStockSameDate.Quantity + price * quantity) / (quantity + transactionSameStockSameDate.Quantity);
                transactionSameStockSameDate.Quantity += quantity;
                transactionSameStockSameDate.Costs += costs;
                transactionSameStockSameDate.Price = newCombinedPrice;
                return;
            }

            if (fields[currencyColIndex] != fields[commissionCurrencyColIndex])
                throw new Exception($"Transaction and commission currencies are different! TradeId: {fields[tradeIdColIndex]}");

            var transaction = new TransactionDto
            {
                Name = fields[nameColIndex],
                Isin = isin,
                Currency = fields[currencyColIndex],
                Quantity = quantity,
                Costs = costs, // needs to be converted to UserCurrency!
                Price = price,
                TimeStamp = date,
                Guid = fields[tradeIdColIndex]
            };
            transactions.Add(transaction);

            bool IsBuy()
            {
                switch (fields[actionColIndex])
                {
                    case "BUY":
                        return true;
                    case "SELL":
                        return false;
                    default:
                        throw new Exception($"Could not determine buy/sell from: '{fields[actionColIndex]}' TradeId: {fields[tradeIdColIndex]}");
                }
            }
        }

    }
}