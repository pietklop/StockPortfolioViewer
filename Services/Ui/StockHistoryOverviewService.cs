using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Entities;
using Messages.UI.Overview;
using Microsoft.EntityFrameworkCore;

namespace Services.Ui
{
    public class StockHistoryOverviewService
    {
        private readonly StockDbContext db;

        public StockHistoryOverviewService(StockDbContext db)
        {
            this.db = db;
        }

        public List<StockHistoryViewModel> GetStockList()
        {
            var stocks = db.Stocks
                .Include(s => s.Dividends)
                .Include(s => s.LastKnownStockValue.StockValue)
                .Include(s => s.StockValues)
                .Include(s => s.Transactions).ThenInclude(t => t.StockValue)
                .Where(s => s.Transactions.Sum(t => t.Quantity) <= 0)
                .ToList();

            var list = new List<StockHistoryViewModel>(stocks.Count());

            foreach (var stock in stocks)
            {
                var avgBuyPrice = stock.Transactions.DetermineAvgBuyUserPrice();
                var avgSellPrice = stock.Transactions.DetermineAvgSellUserPrice();
                var nStocks = stock.Transactions.IsBuy().Sum(t => t.Quantity);
                var transactions = stock.Transactions.OrderBy(t => t.StockValue.TimeStamp).ToList();
                var totalBuyValue = avgBuyPrice * nStocks;
                var totalSellValue = avgSellPrice * nStocks;
                var profit = totalSellValue - totalBuyValue 
                             + stock.Dividends.Sum(d => d.UserValue - d.UserCosts - d.UserTax)
                             - transactions.Sum(t => t.UserCosts);

                var svm = new StockHistoryViewModel()
                {
                    Name = stock.Name,
                    Isin = stock.Isin,
                    Value = totalSellValue,
                    Profit = profit,
                    ProfitFraction = profit / totalBuyValue,
                    FirstBuy = transactions.First().StockValue.TimeStamp.ToShortDateString(),
                    LastSell = transactions.Last().StockValue.TimeStamp.ToShortDateString(),
                };
                list.Add(svm);
            }

            return list;
        }
    }
}