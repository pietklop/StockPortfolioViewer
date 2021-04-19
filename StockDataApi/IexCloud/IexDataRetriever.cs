using System;
using Core;
using log4net;
using Messages.StockDataApi;
using Newtonsoft.Json.Linq;
using StockDataApi.General;

namespace StockDataApi.IexCloud
{
    /// <summary>
    /// Limits are more complex for this service.
    /// Calls have a different weighing.
    /// Simple <see cref="GetStockSymbol"/> costs one credit
    /// 50k credits per month
    /// </summary>
    public class IexDataRetriever : DataRetrieverBase
    {
        public const string Name = "IEX Cloud";

        public IexDataRetriever(ILog log, Settings settings) : base(log, settings.IexCloudBaseUrl, settings.IexCloudApiKey)
        {
        }

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
            return new StockQuoteDto(symbol, price, DateTimeOffset.FromUnixTimeMilliseconds(epochMsec).DateTime);
        }

        protected override string ComposeRequest(string[] commandParameters)
        {
            throw new NotImplementedException();
        }

        protected override string ComposeRequest(string command) =>
            $"{baseUrl}{command}?token={apiKey}";
    }
}