﻿using System;
using Core;
using log4net;
using Messages.StockDataApi;
using Newtonsoft.Json.Linq;
using StockDataApi.General;

namespace StockDataApi.IexCloud
{
    /// <summary>
    /// USA stocks and ETFs only
    /// Limits are more complex for this service.
    /// Calls have a different weighing.
    /// Simple <see cref="GetStockSymbol"/> costs one credit
    /// 50k credits per month
    /// </summary>
    public class IexDataRetriever : DataRetrieverBase
    {
        public const string ConstName = "IEX Cloud";

        public IexDataRetriever(ILog log, Settings settings, string baseUrl, string apiKey, int priority) : base(log, settings, baseUrl, apiKey, priority)
        {
        }

        /// <summary>
        /// Gets all available symbols
        /// Expensive call in credits and data
        /// </summary>
        public string GetAllSymbols() => Get($"ref-data/symbols");

        private string GetStockSymbol(string isin)
        {
            throw new NotImplementedException();
            return Get($"ref-data/isin/{isin}"); // could not make it work :(
        }

        public override StockQuoteDto GetStockQuote(string symbol)
        {
            var response = Get($"stock/{symbol}/quote");
            if (response == null) throw new Exception($"ServiceHost could not find symbol '{symbol}'");
            dynamic data = JObject.Parse(response);
            var price = (double)data["latestPrice"].Value;
            long epochMsec = (long)data["latestUpdate"].Value;
            var lastPriceUpdate = FromUtcToLocalTime(DateTimeOffset.FromUnixTimeMilliseconds(epochMsec).DateTime);
            return new StockQuoteDto(symbol, price, lastPriceUpdate);
        }

        protected override string ComposeRequest(string[] commandParameters)
        {
            throw new NotImplementedException();
        }

        protected override string ComposeRequest(string command) =>
            $"{baseUrl}{command}?token={apiKey}";

        public override string GetName() => ConstName;

        public override bool DataIsDayBehind() => false;

        public override bool CanRetrieveCurrencies() => false;
        public override double GetCurrencyRate(string foreignCurrency)
        {
            var response = Get($"fx/latest?symbols={Constants.UserCurrency}{foreignCurrency}");
            dynamic data = JObject.Parse(response);
            var child = data["data"];
            var element = child[0];
            throw new NotImplementedException();
        }

    }
}