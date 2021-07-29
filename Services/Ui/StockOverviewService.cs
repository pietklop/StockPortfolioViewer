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
        private static List<StockViewModel> cachedStockList;

        public StockOverviewService(ILog log, StockDbContext db)
        {
            this.log = log;
            this.db = db;
        }

        public double TotalPortfolioValue => cachedStockList.Where(c => c.Name != Constants.Total).Sum(c => c.Value);

        public List<StockViewModel> GetStockList(bool reload, List<string> isins)
        {
            if (cachedStockList != null && !reload)
                return cachedStockList;

            int days30Back = 30;
            var profitDateFrom = DateTime.Now.AddDays(-days30Back).Date;

            var stocks = db.Stocks
                .Include(s => s.AreaShares).ThenInclude(a => a.Area)
                .Include(s => s.StockRetrieverCompatibilities).ThenInclude(c => c.DataRetriever)
                .Include(s => s.Dividends)
                .Include(s => s.LastKnownStockValue.StockValue)
                .Include(s => s.SectorShares).ThenInclude(ss => ss.Sector)
                .Include(s => s.StockValues.Where(sv => sv.TimeStamp > profitDateFrom))
                .Include(s => s.Transactions).ThenInclude(t => t.StockValue)
                .Where(s => s.Transactions.Sum(t => t.Quantity) > 0)
                .Where(s => isins == null || isins.Contains(s.Isin))
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
                    ProfitFractionLast30Days = ProfitFraction(stock, days30Back, nStocks),
                    ProfitFractionLast7Days = ProfitFraction(stock, 7, nStocks),
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
                ProfitFractionLast30Days = double.NaN,
                ProfitFractionLast7Days = double.NaN,
                PortFolioFraction = list.Sum(l => l.PortFolioFraction),
            });

            cachedStockList = list.OrderByDescending(l => l.Value).ToList();
            return cachedStockList;

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

            double ProfitFraction(Stock stock, int nDays, double nStocks)
            {
                var dateFrom = DateTime.Now.AddDays(-nDays).Date;
                var historicValue = stock.StockValues.OrderBy(v => v.TimeStamp).FirstOrDefault(v => v.TimeStamp > dateFrom)?.UserPrice ?? 0;
                if (historicValue <= 0) return 0;
                var divPerShare = stock.Dividends.Where(d => d.TimeStamp > dateFrom).Sum(d => d.UserValue - d.UserCosts - d.UserTax) / nStocks;
                return (stock.LastKnownUserPrice + divPerShare - historicValue) / historicValue;
            }
        }
    }
}