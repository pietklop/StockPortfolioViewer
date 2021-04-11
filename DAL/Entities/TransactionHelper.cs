using System.Collections.Generic;
using System.Linq;

namespace DAL.Entities
{
    public static class TransactionHelper
    {
        public static double DetermineAvgBuyUserPrice(this ICollection<Transaction> transactions)
        {
            var buyTransactions = transactions.Where(t => t.Quantity > 0).ToList();
            return buyTransactions.Sum(t => t.Quantity * t.StockValue.UserPrice) / buyTransactions.Sum(t => t.Quantity);
        }

        public static double DetermineAvgBuyNativePrice(this ICollection<Transaction> transactions)
        {
            var buyTransactions = transactions.Where(t => t.Quantity > 0).ToList();
            return buyTransactions.Sum(t => t.Quantity * t.StockValue.NativePrice) / buyTransactions.Sum(t => t.Quantity);
        }
    }
}