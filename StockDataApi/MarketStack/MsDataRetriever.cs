using System;
using System.Linq;
using log4net;
using Messages.StockDataApi;
using Newtonsoft.Json.Linq;
using StockDataApi.General;

namespace StockDataApi.MarketStack
{
    /// <summary>
    /// Request limits:
    /// - 1000 per month
    /// - 5 per sec
    /// With one call you can retrieve 1000 data points in time 
    /// </summary>
    public class MsDataRetriever : DataRetrieverBase
    {
        public const string ConstName = "MarketStack";

        public MsDataRetriever(ILog log, string baseUrl, string apiKey, int priority) : base(log, baseUrl, apiKey, priority)
        {

        }

        protected override string ComposeRequest(string[] commandParameters)
        {
            string commands = string.Join("&", commandParameters.Skip(1));
            return $"{baseUrl}{commandParameters[0]}?&access_key={apiKey}&{commands}";
        }

        protected override string ComposeRequest(string command) =>
            throw new NotImplementedException();

        public override StockQuoteDto GetStockQuote(string symbol)
        {
            string response = Get(new []{"eod", $"symbols={symbol}", "limit=1"});
            dynamic data = JObject.Parse(response);
            var child = data["data"];
            var element = child[0];
            var price = (double)element["close"];
            var dateTime = AddMissingCloseTime((DateTime)element["date"]);
         
            return new StockQuoteDto(symbol, price, dateTime);
        }

        public override string GetName() => ConstName;

        public override bool CanRetrieveCurrencies() => false;
        public override double GetCurrencyRate(string foreignCurrency) =>
            throw new NotSupportedException($"{nameof(GetCurrencyRate)} is not supported for {ConstName}");
    }
}