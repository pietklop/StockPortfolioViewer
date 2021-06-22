using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DAL;
using DAL.Entities;
using log4net;
using Messages.UI.Overview;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;

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
                .Include(s => s.AreaShares).ThenInclude(a => a.Area)
                .Include(s => s.StockRetrieverCompatibilities).ThenInclude(c => c.DataRetriever)
                .Include(s => s.Dividends)
                .Include(s => s.LastKnownStockValue.StockValue)
                .Include(s => s.SectorShares).ThenInclude(ss => ss.Sector)
                .Include(s => s.Transactions).ThenInclude(t => t.StockValue)
                .Where(s => s.Transactions.Sum(t => t.Quantity) > 0)
                .ToList();

            var list = new List<StockViewModel>(stocks.Count());

            double totVirtualBuyValue = 0;
            foreach (var stock in stocks)
            {
                var avgBuyPrice = stock.Transactions.DetermineAvgBuyUserPrice();
                var nStocks = stock.Transactions.Sum(t => t.Quantity);
                var currentValue = stock.LastKnownUserPrice * nStocks
                                   + stock.Dividends.Sum(d => d.UserValue - d.UserCosts - d.UserTax);
                var virtualBuyValue = avgBuyPrice * nStocks;
                totVirtualBuyValue += virtualBuyValue;
                var profit = stock.Transactions.Sum(t => -t.Quantity * t.StockValue.UserPrice) + currentValue;
                list.Add(new StockViewModel
                {
                    Name = StockName(stock),
                    Isin = stock.Isin,
                    Value = currentValue,
                    Profit = profit,
                    ProfitFraction = profit / virtualBuyValue,
                    LastPriceChange = LastUpdateSince(stock),
                    CompatibleDataRetrievers = string.Join(",", stock.StockRetrieverCompatibilities.OrderBy(c => c.DataRetriever.Priority).Where(c => c.DataRetriever.Priority > 0 && c.Compatibility == RetrieverCompatibility.True).Select(c => c.DataRetriever.Name.Substring(0, 3)))
                });
            }

            var totalValue = list.Sum(l => l.Value);
            foreach (var stockItem in list)
                stockItem.PortFolioFraction = stockItem.Value / totalValue;

            var totalProfit = list.Sum(l => l.Profit);
            list.Add(new StockViewModel
            {
                Name = Constants.Total,
                Value = totalValue,
                Profit = totalProfit,
                ProfitFraction = totalProfit / totVirtualBuyValue,
                PortFolioFraction = list.Sum(l => l.PortFolioFraction),
            });
            return list.OrderByDescending(l => l.Value).ToList();

            string LastUpdateSince(Stock stock) => (DateTime.Now - stock.LastKnownStockValue.StockValue.TimeStamp).TimeAgo();

            // add suffix to stockName in case data is incomplete
            string StockName(Stock stock)
            {
                if (stock.AreaShares.Count == 1 && stock.AreaShares.Single().Area.Name == Constants.Unknown)
                    return StockNameMarkUnknown(stock);
                if (stock.SectorShares.Count == 1 && stock.SectorShares.Single().Sector.Name == Constants.Unknown)
                    return StockNameMarkUnknown(stock);

                return stock.Name;
            }

            string StockNameMarkUnknown(Stock stock) => $"{stock.Name} (Unk.)";
        }
    }
}