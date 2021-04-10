using DAL;
using DAL.Entities;

namespace TestConsole
{
    public static class CurrencySetup
    {
        public static void AddCurrencies(StockDbContext db)
        {
            AddCurrency(db, "EUR", "€");
            AddCurrency(db, "USD", "$");

            db.SaveChanges();
        }

        private static void AddCurrency(StockDbContext db, string key, string symbol = null)
        {
            db.Currencies.Add(new Currency { Key = key, Symbol = symbol ?? key});
        }
    }
}