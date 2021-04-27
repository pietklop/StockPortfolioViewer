using System;
using log4net;
using Messages.StockDataApi;
using Newtonsoft.Json.Linq;
using StockDataApi.General;

namespace StockDataApi.TwelveData
{
    /// <summary>
    /// Request limits:
    /// - 800 per day
    /// - 12 per minute
    /// </summary>
    public class TdDataRetriever : DataRetrieverBase
    {
        public const string ConstName = "Twelve Data";

        public TdDataRetriever(ILog log, string baseUrl, string apiKey, int priority) : base(log, baseUrl, apiKey, priority)
        {
        }

        protected override string ComposeRequest(string[] commandParameters) => throw new NotImplementedException();

        protected override string ComposeRequest(string command) => $"{baseUrl}{command}&apikey={apiKey}";

        public override StockQuoteDto GetStockQuote(string symbol)
        {
            string response = Get($"quote?symbol={symbol}");
            dynamic data = JObject.Parse(response);
            var price = (double)data["close"];
            var dateTime = AddMissingCloseTime((DateTime)data["datetime"]);

            return new StockQuoteDto(symbol, price, dateTime);
        }

        public override string GetName() => ConstName;

        public override bool CanRetrieveCurrencies() => false;
        public override double GetCurrencyRate(string foreignCurrency) =>
            throw new NotSupportedException($"{nameof(GetCurrencyRate)} is not supported for {ConstName}");
    }
}