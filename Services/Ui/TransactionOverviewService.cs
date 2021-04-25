using System.Collections.Generic;
using System.Linq;
using DAL;
using log4net;
using Messages.UI.Overview;
using Microsoft.EntityFrameworkCore;

namespace Services.Ui
{
    public class TransactionOverviewService
    {
        private readonly ILog log;
        private readonly StockDbContext db;

        public TransactionOverviewService(ILog log, StockDbContext db)
        {
            this.log = log;
            this.db = db;
        }

        public List<TransactionViewModel> GetStockList()
        {
            var transactions = db.Transactions
                .Include(t => t.Stock.Currency)
                .Include(t => t.StockValue)
                .OrderByDescending(t => t.StockValue.TimeStamp).ToList();

            var list = new List<TransactionViewModel>(transactions.Count());

            foreach (var transaction in transactions)
            {
                var currSymbol = transaction.Stock.Currency.Symbol;
                list.Add(new TransactionViewModel
                {
                    Name = transaction.Stock.Name,
                    Date = transaction.StockValue.TimeStamp.ToShortDateString(),
                    Quantity = transaction.Quantity,
                    Price = transaction.StockValue.NativePrice.FormatCurrency(currSymbol, false),
                    Value = (transaction.StockValue.NativePrice * transaction.Quantity).FormatCurrency(currSymbol),
                    Costs = transaction.UserCosts.FormatUserCurrency(),
                });
            }

            return list;
        }

    }
}