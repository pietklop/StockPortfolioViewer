using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Entities
{
    public static class TransactionHelper
    {
        public static double DetermineAvgBuyUserPrice(this ICollection<Transaction> transactions)
        {
            var buyTransactions = transactions.IsBuy().ToList();
            return buyTransactions.Sum(t => t.Quantity * t.StockValue.UserPrice) / buyTransactions.Sum(t => t.Quantity);
        }

        public static double DetermineAvgSellUserPrice(this ICollection<Transaction> transactions)
        {
            var sellTransactions = transactions.IsSell().ToList();
            return sellTransactions.Sum(t => t.Quantity * t.StockValue.UserPrice) / sellTransactions.Sum(t => t.Quantity);
        }

        public static double DetermineAvgBuyNativePrice(this ICollection<Transaction> transactions)
        {
            var buyTransactions = transactions.IsBuy().ToList();
            return buyTransactions.Sum(t => t.Quantity * t.StockValue.NativePrice) / buyTransactions.Sum(t => t.Quantity);
        }

        public static IEnumerable<Transaction> IsBuy(this ICollection<Transaction> transactions) =>
            transactions.Where(t => t.Quantity > 0);

        public static IEnumerable<Transaction> IsSell(this ICollection<Transaction> transactions) =>
            transactions.Where(t => t.Quantity < 0);

        public static IEnumerable<Transaction> IsBetween(this ICollection<Transaction> transactions, DateTime dateFrom, DateTime dateTo) =>
            transactions.Where(t => t.StockValue.TimeStamp.Date >= dateFrom && t.StockValue.TimeStamp.Date <= dateTo);

        public static bool IsStockDividend(this Transaction transactions) => transactions.StockValue.UserPrice <= 0.1;
    }
}