using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Entities;
using log4net;
using Messages.UI.Overview;
using Microsoft.EntityFrameworkCore;

namespace Services.Ui
{
    public class StockOverviewService
    {
        private readonly ILog log;
        private readonly StockDbContext db;

        public StockOverviewService(ILog log, StockDbContext db)
        {
            this.log = log;
            this.db = db;
        }

        public List<StockViewModel> GetStockList()
        {
            var stocks = db.Stocks
                .Include(s => s.Dividends)
                .Include(s => s.LastKnownStockValue.StockValue)
                .Include(s => s.Transactions).ThenInclude(t => t.StockValue)
                .Where(s => s.Transactions.Sum(t => t.Quantity) > 0)
                .ToList();

            var list = new List<StockViewModel>(stocks.Count());

            foreach (var stock in stocks)
            {
                var avgBuyPrice = stock.Transactions.DetermineAvgBuyUserPrice();
                var nStocks = stock.Transactions.Sum(t => t.Quantity);
                var currentValue = stock.LastKnownUserPrice * nStocks
                                   + stock.Dividends.Sum(d => d.UserValue - d.UserCosts - d.UserTax);
                var virtualBuyValue = avgBuyPrice * nStocks;
                var profit = currentValue - virtualBuyValue;
                list.Add(new StockViewModel
                {
                    Name = stock.Name,
                    Value = currentValue,
                    Profit = profit,
                    ProfitFraction = profit / virtualBuyValue, 
                });
            }

            var totalValue = list.Sum(l => l.Value);
            foreach (var stockItem in list)
                stockItem.PortFolioFraction = stockItem.Value / totalValue;

            return list;
        }
    }
}