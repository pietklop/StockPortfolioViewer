using System;
using System.Globalization;
using Core;
using log4net;
using Messages.StockDataApi;
using Newtonsoft.Json.Linq;
using StockDataApi.General;

namespace StockDataApi.AlphaVantage
{
    /// <summary>
    /// Request limits:
    /// - 500 per day
    /// - 5 per min
    /// </summary>
    public class AvDataRetriever : DataRetrieverBase
    {
        public const string Name = "Alpha Vantage";

        public AvDataRetriever(ILog log, Settings settings) : base(log, settings.AlphaVantageBaseUrl, settings.AlphaVantageApiKey)
        {
        }

        public override StockQuoteDto GetStockQuote(string symbol)
        {
            string response = Get(new[] { "function=GLOBAL_QUOTE", $"symbol={symbol}" });
            dynamic data = JObject.Parse(response);
            var child = data["Global Quote"];
            var price = child["05. price"];
            var date = child["07. latest trading day"];
            return new StockQuoteDto(symbol, double.Parse(price.Value, CultureInfo.InvariantCulture), DateTime.Parse(date.Value));
        }

        public double GetCurrencyRate(string foreignCurrency)
        {
            string response = Get($"function=CURRENCY_EXCHANGE_RATE&from_currency={Constants.UserCurrency}&to_currency={foreignCurrency}");
            dynamic data = JObject.Parse(response);
            var child = data["Realtime Currency Exchange Rate"];
            var price = child["8. Bid Price"];
            return double.Parse(price.Value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Results are a bit disappointing because first result is not always the most common stock
        /// </summary>
        public string SearchStock(string searchString)
        {
            return Get(new[] { "function=SYMBOL_SEARCH", $"keywords={searchString}" });
        }

        protected override string ComposeRequest(string[] commandParameters)
        {
            string pars = string.Join("&", commandParameters);
            return ComposeRequest(pars);
        }

        protected override string ComposeRequest(string command) =>
            $"{baseUrl}query?{command}&apikey={apiKey}&datatype=json";
    }
}