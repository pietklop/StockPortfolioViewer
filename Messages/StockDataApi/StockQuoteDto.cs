using System;

namespace Messages.StockDataApi
{
    public class StockQuoteDto
    {
        public string Symbol { get; }
        public double Price { get; }
        public DateTime LastPriceUpdate { get; }

        public StockQuoteDto(string symbol, double price, DateTime lastPriceUpdate)
        {
            Symbol = symbol;
            Price = price;
            LastPriceUpdate = lastPriceUpdate;
        }
    }
}