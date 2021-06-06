using System;
using System.IO;
using System.Net;
using Core;
using log4net;
using Messages.StockDataApi;

namespace StockDataApi.General
{
    public abstract class DataRetrieverBase
    {
        /// <summary>
        /// Order when multiple retrievers are available
        /// -1 means not available
        /// </summary>
        public int Priority { get; }
        public string Name => GetName();
        protected readonly ILog log;
        private readonly Settings settings;
        protected readonly string baseUrl;
        protected readonly string apiKey;

        protected DataRetrieverBase(ILog log, Settings settings, string baseUrl, string apiKey, int priority)
        {
            Priority = priority;
            if (!baseUrl.EndsWith("/")) baseUrl += "/";
            this.log = log;
            this.settings = settings;
            this.baseUrl = baseUrl;
            this.apiKey = apiKey;
        }

        protected abstract string ComposeRequest(string[] commandParameters);
        protected abstract string ComposeRequest(string command);

        public abstract string GetName();
        public abstract StockQuoteDto GetStockQuote(string symbol);
        public abstract bool CanRetrieveCurrencies();
        public abstract double GetCurrencyRate(string foreignCurrency);
        public abstract bool DataIsDayBehind();

        protected string Get(string[] commandParameters) => GetByUrl(ComposeRequest(commandParameters));

        protected string Get(string command) => GetByUrl(ComposeRequest(command));

        private string GetByUrl(string requestUrl)
        {
            log.Debug($"Do get-request: '{requestUrl}'");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);

            try
            {
                string data = "";
                using (HttpWebResponse response = (HttpWebResponse) request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    data = reader.ReadToEnd();
                    log.Debug(data);
                    if (settings.DebugMode)
                    {
                        string fileName = $"{DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH'.'mm'.'ss'.'fff")}.txt";
                        var path = Path.Combine(settings.DebugPath, "ReceivedData", fileName);
                        File.WriteAllText(path, data);
                    }
                }

                return data;

            }
            catch (WebException wex)
            {
                var code = ((HttpWebResponse)wex.Response).StatusCode;
                if (code == HttpStatusCode.NotFound)
                {
                    log.Info($"Not found");
                    return null;
                }

                log.Error($"WebException occured during web request", wex);
                return null;
            }
            catch (Exception ex)
            {
                log.Error($"Exception occured during web request", ex);
                return null;
            }
        }

        protected DateTime FromUtcToLocalTime(DateTime dateTime)
        {
            var now = DateTime.Now;
            var diff = now.TimeOfDay - now.ToUniversalTime().TimeOfDay;
            return dateTime + diff;
        }

        protected DateTime AddMissingCloseTime(DateTime date) =>
            date.Date == DateTime.Now.Date ? DateTime.Now : date.AddHours(18);

        public override string ToString() => Name;
    }
}