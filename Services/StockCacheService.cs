using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DAL;
using DAL.Entities;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    /// <summary>
    /// Keeps all stocks, including their transactions, dividends, stock values and distributions, in memory.
    /// Loaded once at startup and completely reloaded after an import.
    /// The entities are not tracked by any <see cref="StockDbContext"/>, treat them as read-only
    /// </summary>
    public class StockCacheService
    {
        private readonly ILog log;
        private readonly StockDbContextFactory dbContextFactory;
        private List<Stock> stocks;
        private Dictionary<string, Stock> stocksByIsin;

        public StockCacheService(ILog log, StockDbContextFactory dbContextFactory)
        {
            this.log = log;
            this.dbContextFactory = dbContextFactory;
        }

        public IReadOnlyList<Stock> Stocks
        {
            get
            {
                if (stocks == null) Reload();
                return stocks;
            }
        }

        public IEnumerable<Transaction> Transactions => Stocks.SelectMany(s => s.Transactions);
        public IEnumerable<Dividend> Dividends => Stocks.SelectMany(s => s.Dividends);
        public IEnumerable<PitStockValue> StockValues => Stocks.SelectMany(s => s.StockValues);

        /// <param name="isins">null => all stocks</param>
        public IEnumerable<Stock> GetStocks(ICollection<string> isins) =>
            isins == null ? Stocks : Stocks.Where(s => isins.Contains(s.Isin));

        public Stock GetStockOrThrow(string isin) =>
            GetStock(isin) ?? throw new Exception($"Could not find stock with isin: '{isin}'");

        public Stock GetStock(string isin)
        {
            if (stocksByIsin == null) Reload();
            return stocksByIsin.GetValueOrDefault(isin);
        }

        public void Reload()
        {
            var stopwatch = Stopwatch.StartNew();
            using var db = dbContextFactory.CreateDbContext();

            var loadedStocks = StocksQuery(db).ToList();
            stocksByIsin = loadedStocks.ToDictionary(s => s.Isin);
            stocks = loadedStocks;

            log.Info($"Loaded {stocks.Count} stocks, {Transactions.Count()} transactions, {Dividends.Count()} dividends and {StockValues.Count()} stock values in {stopwatch.ElapsedMilliseconds}ms");
        }

        /// <summary>
        /// Reload a single stock, for example after it has been edited
        /// </summary>
        public void ReloadStock(string isin)
        {
            if (stocks == null)
            {
                Reload();
                return;
            }

            using var db = dbContextFactory.CreateDbContext();
            var stock = StocksQuery(db).SingleOrDefault(s => s.Isin == isin);

            var index = stocks.FindIndex(s => s.Isin == isin);
            if (stock == null)
            {
                if (index >= 0) stocks.RemoveAt(index);
                stocksByIsin.Remove(isin);
                return;
            }

            if (index >= 0) stocks[index] = stock;
            else stocks.Add(stock);
            stocksByIsin[isin] = stock;
        }

        private static IQueryable<Stock> StocksQuery(StockDbContext db) =>
            db.Stocks
                .Include(s => s.Currency)
                .Include(s => s.LastKnownStockValue.StockValue)
                .Include(s => s.StockValues)
                .Include(s => s.Transactions).ThenInclude(t => t.StockValue)
                .Include(s => s.Dividends)
                .Include(s => s.AreaShares).ThenInclude(a => a.Area.Continent)
                .Include(s => s.SectorShares).ThenInclude(ss => ss.Sector)
                .OrderBy(s => s.Id)
                .AsSplitQuery() // prevent cartesian explosion of all collections
                .AsNoTrackingWithIdentityResolution(); // same db record => same instance, so the navigations are fixed up
    }
}
