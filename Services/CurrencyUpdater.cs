using System;
using System.Linq;
using Core;
using DAL;
using log4net;
using Services.DI;
using StockDataApi.AlphaVantage;

namespace Services
{
    public class CurrencyUpdater
    {
        private readonly ILog log;
        private readonly StockDbContext db;
        private readonly Settings settings;

        public CurrencyUpdater(ILog log, StockDbContext db, Settings settings)
        {
            this.log = log;
            this.db = db;
            this.settings = settings;
        }

        public void Run()
        {
            log.Debug($"Run {nameof(CurrencyUpdater)}");
            var currencies = db.Currencies.Where(c => c.Key != Constants.UserCurrency).ToList();

            var dr = CastleContainer.Resolve<AvDataRetriever>();

            foreach (var currency in currencies)
            {
                if ((DateTime.Now - currency.LastUpdate).TotalDays < settings.CurrencyRatioExpiresAfterDays)
                    continue;
                currency.Ratio = dr.GetCurrencyRate(currency.Key);
                currency.LastUpdate = DateTime.Now;
                log.Info($"Updated currency '{currency}' Ratio: 1 {Constants.UserCurrency} = {currency.Symbol}{currency.Ratio}");
            }

            db.SaveChanges();
        }
    }
}