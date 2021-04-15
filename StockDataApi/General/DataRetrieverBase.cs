using System;
using System.IO;
using System.Net;
using log4net;
using Messages.StockDataApi;

namespace StockDataApi.General
{
    public abstract class DataRetrieverBase
    {
        protected readonly ILog log;
        protected readonly string baseUrl;
        protected readonly string apiKey;

        protected DataRetrieverBase(ILog log, string baseUrl, string apiKey)
        {
            if (!baseUrl.EndsWith("/")) baseUrl += "/";
            this.log = log;
            this.baseUrl = baseUrl;
            this.apiKey = apiKey;
        }

        protected abstract string ComposeRequest(string[] commandParameters);
        protected abstract string ComposeRequest(string command);

        public abstract StockQuoteDto GetStockQuote(string symbol);

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

    }
}