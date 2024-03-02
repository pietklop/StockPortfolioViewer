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

        public static double TotalPortfolioValue { get; private set; }

        public List<StockViewModel> GetStockList(bool reload, List<string> isins)
        {
            if (cachedStockList != null && !reload)
                return cachedStockList;

            int days30Back = 30;
            var profitDateFrom = DateTime.Now.AddDays(-days30Back).Date;

            var stocks = db.Stocks
                .Include(s => s.AreaShares).ThenInclude(a => a.Area)
                //.Include(s => s.StockRetrieverCompatibilities).ThenInclude(c => c.DataRetriever)
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
            var valueProfitProduct7Days = 0d;
            var valueProfitProduct30Days = 0d;
            foreach (var stock in stocks)
            {
                var avgBuyPrice = stock.Transactions.DetermineAvgBuyUserPrice();
                var nStocks = stock.Transactions.Sum(t => t.Quantity);
                var currentValue = stock.LastKnownUserPrice * nStocks + stock.Dividends.Sum(d => d.UserValue - d.UserCosts);
                var virtualBuyValue = avgBuyPrice * nStocks;
                totVirtualBuyValue += virtualBuyValue;
                var profit = stock.Transactions.Sum(t => -t.Quantity * t.StockValue.UserPrice) + currentValue;
                var svm = new StockViewModel
                {
                    Name = $"{StockName(stock)}{AlarmSuffix(stock)}",
                    Isin = stock.Isin,
                    Value = currentValue,
                    Profit = profit,
                    ProfitFraction = profit / virtualBuyValue,
                    ProfitFractionLast30Days = ProfitFraction(stock, days30Back, nStocks),
                    ProfitFractionLast7Days = ProfitFraction(stock, 8, nStocks),
                    LastPriceChange = LastUpdateSince(stock),
                    Remark = Remark(stock),
                    //CompatibleDataRetrievers = string.Join(",", stock.StockRetrieverCompatibilities.OrderBy(c => c.DataRetriever.Priority).Where(c => c.DataRetriever.Priority > 0 && c.Compatibility == RetrieverCompatibility.True).Select(c => c.DataRetriever.Name.Substring(0, 3)))
                };
                list.Add(svm);
                valueProfitProduct7Days += svm.Value * svm.ProfitFractionLast7Days;
                valueProfitProduct30Days += svm.Value * svm.ProfitFractionLast30Days;
            }

            if (TotalPortfolioValue <= 0 || reload && isins == null) TotalPortfolioValue = list.Sum(l => l.Value);
            var totalValue = list.Sum(l => l.Value);
            foreach (var stockItem in list)
                stockItem.PortFolioFraction = stockItem.Value / TotalPortfolioValue;

            var totalProfit = list.Sum(l => l.Profit);
            list.Add(new StockViewModel
            {
                Name = Constants.Total,
                Value = totalValue,
                Profit = totalProfit,
                ProfitFraction = totalProfit / totVirtualBuyValue,
                ProfitFractionLast30Days = valueProfitProduct30Days / totalValue,
                ProfitFractionLast7Days = valueProfitProduct7Days / totalValue,
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
                var divPerShare = stock.Dividends.Where(d => d.TimeStamp > dateFrom).Sum(d => d.UserValue - d.UserCosts) / nStocks;
                return (stock.LastKnownUserPrice + divPerShare - historicValue) / historicValue;
            }

            string Remark(Stock stock)
            {
                if (stock.AlarmCondition == AlarmCondition.None)
                    return stock.Remarks;
                if (stock.AlarmCondition == AlarmCondition.LowerThan)
                    return $"<{stock.AlarmThreshold} {stock.Remarks}";
                if (stock.AlarmCondition == AlarmCondition.HigherThan)
                    return $">{stock.AlarmThreshold} {stock.Remarks}";
                return stock.Remarks;
            }
        }

        private string AlarmSuffix(Stock stock)
        {
            switch (stock.AlarmCondition)
            {
                case AlarmCondition.None:
                    return "";
                case AlarmCondition.LowerThan:
                    return stock.LastKnownStockValue.StockValue.NativePrice <= stock.AlarmThreshold ? "-" : "";
                case AlarmCondition.HigherThan:
                    return stock.LastKnownStockValue.StockValue.NativePrice >= stock.AlarmThreshold ? "+" : "";
                default:
                    throw new ArgumentOutOfRangeException($"Invalid {nameof(AlarmCondition)} {stock.AlarmCondition}");
            }
        }
    }
}