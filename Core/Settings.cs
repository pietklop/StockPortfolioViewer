namespace Core
{
    public class Settings
    {
        public bool DebugMode { get; set; }
        public string DebugPath { get; set; }
        public string DbFileNamePath { get; set; }
        public int CurrencyRatioExpiresAfterHours { get; set; }
        public int StockUpdateAfterMinutes { get; set; }
        public double BaseAnnualPerformance { get; set; }
    }
}