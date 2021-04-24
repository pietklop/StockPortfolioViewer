using System;

namespace Core
{
    public class Settings
    {
        public int CurrencyRatioExpiresAfterDays { get; set; }
        public int AutoUpdateIexStocksAfterMinutes { get; set; }
        [Obsolete]
        public string AlphaVantageBaseUrl { get; set; }
        [Obsolete]
        public string AlphaVantageApiKey { get; set; }
        [Obsolete]
        public string IexCloudBaseUrl { get; set; }
        [Obsolete]
        public string IexCloudApiKey { get; set; }
    }
}