using System.Linq;
using Constants = Core.Constants;

namespace DAL.Entities
{
    public static class StockHelper
    {
        public static bool IsUserCurrency(this Stock stock) =>
            stock.Currency.Key == Constants.UserCurrency;

        public static IQueryable<Stock> WhereEtf(this IQueryable<Stock> stocks) => stocks.Where(s => s.ExpenseRatio > 0);
        public static IQueryable<Stock> WhereSingleStock(this IQueryable<Stock> stocks) => stocks.Where(s => s.ExpenseRatio == 0);
    }
}