using Messages.StockDataApi;

namespace Services.DataCollection
{
    public interface ICanRetrieveData
    {
        StockQuoteDto GetStockQuote(string symbol, string type = null);
    }
}