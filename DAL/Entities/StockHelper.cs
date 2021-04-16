using Constants = Core.Constants;

namespace DAL.Entities
{
    public static class StockHelper
    {
        public static bool IsUserCurrency(this Stock stock) =>
            stock.Currency.Key == Constants.UserCurrency;
    }
}