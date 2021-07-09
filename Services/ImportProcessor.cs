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

        public (int, int) Process(TransactionImportDto import)
        {
            int nAddedTransactions = 0;
            int nAddedDividends = 0;
            foreach (var t in import.Transactions.OrderBy(t => t.TimeStamp).ToList())
                if (stockService.AddTransaction(t)) nAddedTransactions++;
            foreach (var importDividend in import.Dividends)
                if (stockService.AddDividend(importDividend)) nAddedDividends++;

            db.SaveChanges();

            log.Debug($"Successful imported {nAddedTransactions} transactions and {nAddedDividends} dividends");

            return (nAddedTransactions, nAddedDividends);
        }
    }
}