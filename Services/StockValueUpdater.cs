using System;
using System.Linq;
using Core;
using DAL;
using log4net;
using Services.DI;
using StockDataApi.IexCloud;

namespace Services
{
    public class StockValueUpdater
    {
        private readonly ILog log;
        private readonly StockDbContext db;
        private readonly StockService stockService;
        private readonly Settings settings;

        public StockValueUpdater(ILog log, StockDbContext db, StockService stockService, Settings settings)
        {
            this.log = log;
            this.db = db;
            this.stockService = stockService;
            this.settings = settings;
        }

        public void Run()
        {
            log.Debug($"Run {nameof(StockValueUpdater)}");
            AutoUpdateIexCompatibleStocks();
        }

        private static bool IsWeekend(DateTime? date = null)
        {
            var day = (date ?? DateTime.Now).DayOfWeek;
            return day == DayOfWeek.Saturday || day == DayOfWeek.Sunday;
        }

        private void AutoUpdateIexCompatibleStocks()
        {
            var dr = CastleContainer.Resolve<IexDataRetriever>();

            var stocks = db.Stocks
                .Where(s => s.StockRetrieverCompatibilities.Any(sc => sc.DataRetriever.Name == IexDataRetriever.Name && sc.IsCompatible))
                .Select(s => new
                {
                    name = s.Name,
                    isin = s.Isin,
                    lastUpdate = s.LastKnownStockValue.LastUpdate,
                    drRef = s.StockRetrieverCompatibilities.Single(sc => sc.DataRetriever.Name == IexDataRetriever.Name).StockRef,
                }).ToList();

            foreach (var stock in stocks)
            {
                if (IsWeekend() && LastUpdateWasThisWeekend(stock.lastUpdate))
                    continue;
                if ((DateTime.Now - stock.lastUpdate).TotalMinutes < settings.AutoUpdateIexStocksAfterMinutes)
                    continue;
                log.Debug($"GetStockQuote for {stock.name} using {IexDataRetriever.Name}");
                var data = dr.GetStockQuote(stock.drRef);
                log.Debug($"SaveStockPrice for {stock.name} Price: {data.Price:F2}");

                stockService.UpdateStockPrice(stock.isin, data.Price, data.LastPriceUpdate);
            }

            db.SaveChanges();

            bool LastUpdateWasThisWeekend(DateTime lastUpdate)
            {
                if ((DateTime.Now - lastUpdate).TotalDays > 2) return false;
                return IsWeekend(lastUpdate);
            }
        }
    }
}