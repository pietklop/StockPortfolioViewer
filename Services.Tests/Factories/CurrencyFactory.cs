using System;
using Core;
using DAL;
using DAL.Entities;

namespace Services.Tests.Factories
{
    public static class CurrencyFactory
    {
        public static Currency AddUserCurrency(StockDbContext db, DateTime? lastUpdate = null) => AddTest(db, Constants.UserCurrency, 1, lastUpdate);
        public static Currency AddUsd(StockDbContext db, DateTime? lastUpdate = null) => AddTest(db, "USD", 1.2, lastUpdate);

        public static Currency AddTest(StockDbContext db, string key = Constants.UserCurrency, double ratio = 1, DateTime? lastUpdate = null)
        {
            return db.Currencies.Add(CreateTest(key, ratio, lastUpdate)).Entity;
        }

        public static Currency CreateTest(string key = Constants.UserCurrency, double ratio = 1, DateTime? lastUpdate = null)
        {
            return new Currency
            {
                Key = key,
                Ratio = ratio,
                LastUpdate = lastUpdate ?? DateTime.Now,
            };
        }
    }
}