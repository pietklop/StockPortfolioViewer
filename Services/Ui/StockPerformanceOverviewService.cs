using DAL.Entities;
using Messages.UI.Overview;
using System.Collections.Generic;
using System;
using System.Linq;
using Core;
using Services.Helpers;
using DAL;
using log4net;
using Microsoft.EntityFrameworkCore;

namespace Services.Ui
{
    public class StockPerformanceOverviewService
    {
        private readonly ILog log;
        private readonly StockDbContext db;
        private DateTime dateTo;

        public StockPerformanceOverviewService(ILog log, StockDbContext db, DateTime? dateTo = null)
        {
            this.log = log;
            this.db = db;
            this.dateTo = dateTo ?? DateTime.Today;
        }

        public List<StockPerformanceOverviewModel> GetStockList(List<string> isins, PerformanceInterval interval)
        {
            var intervals = new List<Interval>();
            if (interval == PerformanceInterval.Year && dateTo.Month <= 3) dateTo = dateTo.AddMonths(-dateTo.Month);
            dateTo = DetermineEndOfPeriod(interval, dateTo);
            var span = interval.ToTimeSpan();

            var tmpDateTo = dateTo;
            for (int i = 0; i < 4; i++)
            {
                var tmpInterval = new Interval(tmpDateTo, span);
                intervals.Add(tmpInterval);
                tmpDateTo = tmpInterval.DateFrom;
            }

            var dateFrom = intervals.Last().DateFrom;

            var stocks = db.Stocks
                .Include(s => s.Dividends)
                .Include(s => s.LastKnownStockValue.StockValue)
                .Include(s => s.StockValues.Where(sv => sv.TimeStamp > dateFrom.AddDays(-14) && sv.TimeStamp < dateTo.AddDays(14))) // take 14 days margin
                .Include(s => s.Transactions).ThenInclude(t => t.StockValue)
                .Where(p => isins == null && (p.Transactions.Sum(t => t.Quantity) > 0 || p.Transactions.OrderByDescending(t => t.Id).First().StockValue.TimeStamp >= dateFrom) || isins != null && isins.Contains(p.Isin))
                .ToList();

            var nTotalValueFields = 5;
            var list = new List<StockPerformanceOverviewModel>(stocks.Count()+nTotalValueFields);
            if (AllStocks())
            {
                list.Add(new StockPerformanceOverviewModel { Name = Constants.Total });
                list.Add(new StockPerformanceOverviewModel { Name = Constants.Bought });
                list.Add(new StockPerformanceOverviewModel { Name = Constants.Sold });
                list.Add(new StockPerformanceOverviewModel { Name = Constants.ValueGain });
                list.Add(new StockPerformanceOverviewModel { Name = StockDetailProperties.Dividend });
            }
            list.Add(new StockPerformanceOverviewModel { Name = Constants.TotalValue });

            var startPrices = new Dictionary<Stock, double>(stocks.Count);
            for (int i = 0; i < intervals.Count; i++)
            {
                var stockList = new List<PerformancePeriod>();

                foreach (var stock in stocks)
                {
                    var svm = GetOrCreateStockPerformanceOverviewModel(i, stock);

                    var startPrice = startPrices.TryGetValue(stock, out var price) ? price : (double?)null;
                    var pp = CalculatePerformancePeriod(stock, intervals[i], startPrice);
                    startPrices[stock] = pp.UserPriceStartOfPeriod!.Value;
                    stockList.Add(pp);

                    svm.SetPerformance(i, pp.Performance-1);
                }
                if (AllStocks())
                    CalculateAndSetTotals(i, stockList);
                var svmTotValue = list.Single(x => x.Name == Constants.TotalValue);
                svmTotValue.SetPerformance(i, stockList.Sum(s => s.UserValueEndOfPeriod));
            }

            return list;

            StockPerformanceOverviewModel GetOrCreateStockPerformanceOverviewModel(int i, Stock stock)
            {
                if (i > 0) return list.Single(x => x.Isin == stock.Isin);

                var nStocks = stock.Transactions.Sum(t => t.Quantity);
                var svm = new StockPerformanceOverviewModel
                {
                    Name = $"{stock.Name}",
                    Isin = stock.Isin,
                    Value = nStocks == 0 ? 0 : stock.LastKnownUserPrice * nStocks,
                };
                if (AllStocks())
                    list.Add(svm);
                else
                    list.Insert(0, svm); // put the single stock at the spot of the total performance (due to $ and % formatting)
                return svm;
            }

            bool SingleStock() => isins != null;
            bool AllStocks() => isins == null;

            void CalculateAndSetTotals(int i, List<PerformancePeriod> stockList)
            {
                var bought = stockList.Sum(s => s.UserValueBought);
                var sold = stockList.Sum(s => s.UserValueSold);
                var valStart = stockList.Sum(s => s.UserValueStartOfPeriod);
                var valEnd = stockList.Sum(s => s.UserValueEndOfPeriod);
                var div = stockList.Sum(s => s.UserDividend);
                var gainedValue = valEnd - valStart - bought + sold + div; // including dividend
                var baseVal = valStart + Math.Max(0, (bought - sold) / 2); // when more bought during the year, take average over the year

                var svmTot = list.Single(x => x.Name == Constants.Total);
                svmTot.SetPerformance(i, gainedValue / baseVal);
                var svmBought = list.Single(x => x.Name == Constants.Bought);
                svmBought.SetPerformance(i, bought);
                var svmSold = list.Single(x => x.Name == Constants.Sold);
                svmSold.SetPerformance(i, sold);
                var svmDiv = list.Single(x => x.Name == StockDetailProperties.Dividend);
                svmDiv.SetPerformance(i, div);
                var svmIncrease = list.Single(x => x.Name == Constants.ValueGain);
                svmIncrease.SetPerformance(i, gainedValue);
            }
        }

        private static DateTime DetermineEndOfPeriod(PerformanceInterval interval, DateTime dateTo)
        {
            switch (interval)
            {
                case PerformanceInterval.Month:
                    if (dateTo.Month == 12) return new DateTime(dateTo.Year + 1, 1, 1);
                    return new DateTime(dateTo.Year, dateTo.Month + 1, 1);
                case PerformanceInterval.Quarter:
                    if (dateTo.Month < 4) return new DateTime(dateTo.Year, 4, 1);
                    if (dateTo.Month < 7) return new DateTime(dateTo.Year, 7, 1);
                    if (dateTo.Month < 10) return new DateTime(dateTo.Year, 10, 1);
                    return new DateTime(dateTo.Year + 1, 1, 1);
                case PerformanceInterval.Year:
                    return new DateTime(dateTo.Year + 1, 1, 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(interval), interval, null);
            }
        }

        private PerformancePeriod CalculatePerformancePeriod(Stock stock, Interval interval, double? startPricePrevious)
        {
            DateTime dateFrom = interval.DateFrom;
            DateTime dateTo = interval.DateTo;

            var startPrice = stock.StockValues.OrderByDescending(v => v.TimeStamp).FirstOrDefault(v => v.TimeStamp <= dateFrom)?.UserPrice ?? 0;
            var initStockQt = stock.Transactions.Where(t => t.StockValue.TimeStamp <= dateFrom).Sum(t => t.Quantity);
            var startValue = initStockQt * startPrice;

            var endPrice = startPricePrevious ?? stock.StockValues.OrderByDescending(v => v.TimeStamp).FirstOrDefault(v => v.TimeStamp <= dateTo)?.UserPrice ?? 0;
            var transactions = stock.Transactions.Where(t => t.StockValue.TimeStamp > dateFrom && t.StockValue.TimeStamp <= dateTo).ToList();
            var endValue = (initStockQt + transactions.Sum(t => t.Quantity)) * endPrice;
            var valueBought = transactions.Where(t => t.Quantity > 0).Sum(t => t.Quantity * t.StockValue.UserPrice);
            var valueSold = -transactions.Where(t => t.Quantity < 0).Sum(t => t.Quantity * t.StockValue.UserPrice);

            var dividend = stock.Dividends.Where(d => d.TimeStamp > dateFrom && d.TimeStamp <= dateTo).Sum(d => d.UserValue - d.UserCosts);

            return new PerformancePeriod(stock, dateFrom, interval, startValue, endValue, startPrice, valueBought, valueSold, dividend);
        }
    }

    /// <summary>
    /// Performance of given period.
    /// There can not be any transactions within this period
    /// </summary>
    class PerformancePeriod
    {
        public Stock Stock { get; }
        public DateTime StartDate { get; }
        public Interval Interval { get; }
        public double UserValueStartOfPeriod { get; }
        /// <summary>
        /// Excluding transactions and dividend
        /// </summary>
        public double UserValueEndOfPeriod { get; }
        public double? UserPriceStartOfPeriod { get; }
        public double UserValueBought { get; }
        public double UserValueSold { get; }
        public double UserDividend { get; }
        public double Performance { get; }

        public PerformancePeriod(Stock stock, DateTime startDate, Interval interval, double userValueStartOfPeriod, double userValueEndOfPeriod, double? userPriceStartOfPeriod, double userValueBought, double userValueSold, double userDividend)
        {
            Stock = stock;
            StartDate = startDate;
            Interval = interval;
            UserValueStartOfPeriod = userValueStartOfPeriod;
            UserValueEndOfPeriod = userValueEndOfPeriod;
            UserPriceStartOfPeriod = userPriceStartOfPeriod;
            UserValueBought = userValueBought;
            UserValueSold = userValueSold;
            UserDividend = userDividend;
            Performance = (userValueEndOfPeriod + userValueSold + userDividend) / (userValueStartOfPeriod + userValueBought);
        }

        public override string ToString() => $"{Stock.Name} {StartDate:dd-MM-yyyy} {UserValueEndOfPeriod:F2}";
    }

    class Interval
    {
        public DateTime DateFrom { get; }
        public DateTime DateTo { get; }

        public Interval(DateTime dateTo, TimeSpan span)
        {
            DateFrom = dateTo - span;
            DateTo = dateTo;
        }

        public override string ToString() => $"{DateFrom:dd-MM} => {DateTo:dd-MM-yyyy}";
    }
}