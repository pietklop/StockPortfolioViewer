using System.Linq;
using DAL;
using log4net;
using Messages.Dtos;

namespace Services
{
    public class ImportProcessor
    {
        private readonly ILog log;
        private readonly StockDbContext db;
        private readonly StockService stockService;

        public ImportProcessor(ILog log, StockDbContext db, StockService stockService)
        {
            this.log = log;
            this.db = db;
            this.stockService = stockService;
        }

        public void Process(ImportDto import)
        {
            import.Transactions.OrderBy(t => t.TimeStamp).ToList().ForEach(stockService.AddTransaction);
            import.Dividends.ForEach(stockService.AddDividend);
        }
    }
}