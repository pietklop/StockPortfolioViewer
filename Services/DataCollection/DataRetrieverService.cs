using System;
using System.Linq;
using Core;
using DAL;
using DAL.Entities;
using log4net;
using Messages.StockDataApi;
using Microsoft.EntityFrameworkCore;
using StockDataApi.General;

namespace Services.DataCollection
{
    /// <summary>
    /// Instance per retriever
    /// </summary>
    public class DataRetrieverService
    {
        private readonly ILog log;
        private readonly StockDbContext db;
        private readonly DataRetrieverBase dataRetriever;
        private readonly StockService stockService;
        public string Name => dataRetriever.Name;
        public int Priority => dataRetriever.Priority;
        public bool CanRetrieveCurrencies => dataRetriever.CanRetrieveCurrencies();

        public DataRetrieverService(ILog log, StockDbContext db, DataRetrieverBase dataRetriever, StockService stockService)
        {
            this.log = log;
            this.db = db;
            this.dataRetriever = dataRetriever;
            this.stockService = stockService;
        }

        public void UpdateCurrency(Currency currency)
        {
            currency.Ratio = dataRetriever.GetCurrencyRate(currency.Key);
            currency.LastUpdate = DateTime.Now;
            log.Info($"Updated currency '{currency}' Ratio: 1 {Constants.UserCurrency} = {currency.Symbol}{currency.Ratio}");
            UpdateCallCount();
        }

        public StockQuoteDto UpdateStockQuote(Stock stock)
        {
            var quote = dataRetriever.GetStockQuote(stock.StockRetrieverCompatibilities.Single(c => c.DataRetriever.Name == Name).StockRef);
            stockService.UpdateStockPrice(stock, quote.Price, quote.LastPriceUpdate);
            UpdateCallCount();
            return quote;
        }

        private void UpdateCallCount()
        {
            var retrieverDb = GetRetrieverEntity();
            retrieverDb.RetrieverLimitations.ToList().ForEach(l => l.UpdateAfterRequest(retrieverDb.LastRequest));
            retrieverDb.LastRequest = DateTime.Now;
        }

        /// <summary>
        /// Due to limitations
        /// </summary>
        public bool CanCall()
        {
            var retrieverDb = GetRetrieverEntity();
            var result = retrieverDb.RetrieverLimitations.All(l => l.CanCall(retrieverDb.LastRequest));
            log.Debug($"CanCall: {result} {retrieverDb.Name}");
            return result;
        }

        private DataRetriever GetRetrieverEntity() =>
            db.DataRetrievers.Include(d => d.RetrieverLimitations).Include(d => d.StockRetrieverCompatibilities).Single(d => d.Name == Name);
    }
}