using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly List<DataRetrieverService> dataRetrieverServices;

        public DataRetrieverManager(ILog log, StockDbContext db)
        {
            this.log = log;
            this.db = db;
            dataRetrieverServices = ResolveServices();
        }

        private List<DataRetrieverService> ResolveServices()
        {
            var drs = db.DataRetrievers.Where(d => d.Priority > 0).ToList();

            var services = new List<DataRetrieverService>();
            foreach (var drDb in drs)
                services.Add(CastleContainer.ResolveRetrieverService(drDb));

            return services;
        }

        public void TryUpdateStocks(TimeSpan? olderThan = null)
        {
            if (olderThan == null) olderThan = TimeSpan.FromSeconds(10);
            var stocks = db.Stocks
                .Include(s => s.Currency)
                .Include(s => s.LastKnownStockValue)
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
                stocks.Where(s => (DateTime.Now - s.LastKnownStockValue.LastUpdate).TotalSeconds > olderThan.Value.TotalSeconds).ToList();
            
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
        }
    }
}