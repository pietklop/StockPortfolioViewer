using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DAL;
using DAL.Entities;
using log4net;
using Microsoft.EntityFrameworkCore;
using Services.DI;

namespace Services.DataCollection
{
    public class DataRetrieverManager
    {
        private readonly ILog log;
        private readonly StockDbContext db;
        private readonly Settings settings;
        private readonly List<DataRetrieverService> dataRetrieverServices;

        public DataRetrieverManager(ILog log, StockDbContext db, Settings settings)
        {
            this.log = log;
            this.db = db;
            this.settings = settings;
            dataRetrieverServices = ResolveServices();
        }

        private List<DataRetrieverService> ResolveServices()
        {
            var drs = db.DataRetrievers.ToList();

            var services = new List<DataRetrieverService>();
            foreach (var drDb in drs)
                services.Add(CastleContainer.ResolveRetrieverService(drDb));

            return services;
        }

        public void TryUpdateCurrencies()
        {
            var dr = dataRetrieverServices.OrderBy(d => d.Priority).FirstOrDefault(d => d.CanRetrieveCurrencies)
                ?? throw new Exception($"Could not find any retriever to update currencies");

            log.Debug($"Check for outdated currencies");
            var currencies = db.Currencies.Where(c => c.Key != Constants.UserCurrency).ToList();

            foreach (var currency in currencies)
            {
                if ((DateTime.Now - currency.LastUpdate).TotalHours < settings.CurrencyRatioExpiresAfterHours)
                    continue;
                dr.UpdateCurrency(currency);
            }

            db.SaveChanges();
        }

        public double EuroPrice() => db.Currencies.Single(c => c.Key == "USD").Ratio;

        public void TryUpdateStocks() => TryUpdateStocks(TimeSpan.FromMinutes(settings.StockUpdateAfterMinutes));

        public void TryUpdateStocks(TimeSpan olderThan)
        {
            var stocks = db.Stocks
                .Include(s => s.Currency)
                .Include(s => s.LastKnownStockValue.StockValue)
                .Include(s => s.StockRetrieverCompatibilities).ThenInclude(c => c.DataRetriever)
                .Where(s => s.StockRetrieverCompatibilities.Any(c => c.DataRetriever.Priority > 0 && c.Compatibility == RetrieverCompatibility.True))
                .OrderBy(s => s.LastKnownStockValue.LastUpdate)
                .ToList();

            log.Debug($"{stocks.Count} stocks have a compatible retriever");
            stocks = WhereOutdated();
            log.Debug($"{stocks.Count} stocks are outdated, older than: {olderThan}");
            if (IsWeekend())
            {
                stocks = WhereNotUpdatedInTheWeekend();
                log.Debug($"{stocks.Count} stocks are not yet updated this weekend");
            }

            stocks.ForEach(TryUpdateStock);

            db.SaveChanges();

            List<Stock> WhereOutdated() =>
                stocks.Where(s => (DateTime.Now - s.LastKnownStockValue.LastUpdate).TotalSeconds > olderThan.TotalSeconds).ToList();
            
            List<Stock> WhereNotUpdatedInTheWeekend() => stocks.Where(s => !IsWeekend(s.LastKnownStockValue.LastUpdate)).ToList();
        }

        private static bool IsWeekend(DateTime? date = null)
        {
            var day = (date ?? DateTime.Now).DayOfWeek;
            return day == DayOfWeek.Saturday || day == DayOfWeek.Sunday;
        }

        private void TryUpdateStock(Stock stock)
        {
            var drDbs = stock.StockRetrieverCompatibilities
                .OrderBy(c => c.DataRetriever.Priority)
                .Where(c => c.DataRetriever.Priority > 0 && c.Compatibility == RetrieverCompatibility.True)
                .ToList();

            if (AllCompatibleRetrieversGiveDataDayBehind() && stock.LastKnownStockValue.LastUpdate.Date == DateTime.Now.Date)
            {
                log.Debug($"All retrievers provide day behind data for {stock} and stock is updated today, so abort");
                return;
            }

            var drService = GetFirstAvailableService();
            if (drService == null)
            {
                log.Debug($"Could not update stock {stock} because no retriever is currently ready for a request");
                return;
            }

            log.Debug($"Update stock {stock} with {drService.Name}");

            drService.UpdateStockQuote(stock);

            DataRetrieverService GetFirstAvailableService()
            {
                foreach (var drDb in drDbs)
                {
                    var dr = dataRetrieverServices.Single(d => d.Name == drDb.DataRetriever.Name);
                    if (dr.CanCall())
                        return dr;
                    log.Debug($"{dr.Name} is limited at the moment");
                }

                return null;
            }

            bool AllCompatibleRetrieversGiveDataDayBehind() => dataRetrieverServices.Where(d => drDbs.Select(b => b.DataRetriever.Name).Contains(d.Name)).All(d => d.DataIsDayBehind);
        }
    }
}