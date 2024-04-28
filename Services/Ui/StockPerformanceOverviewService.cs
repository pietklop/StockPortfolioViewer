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

        public List<StockPerformanceOverviewModel> GetStockList(PerformanceInterval interval)
        {
            var intervals = new List<Interval>();
            if (interval == PerformanceInterval.Year && dateTo.Month <= 3) dateTo = dateTo.AddMonths(dateTo.Month);
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
                .ToList();

            var list = new List<StockPerformanceOverviewModel>(stocks.Count()+1);

            if (interval == PerformanceInterval.Year)
                list.Add(new StockPerformanceOverviewModel { Name = Constants.Total });

            for (int i = 0; i < intervals.Count; i++)
            {
                var dict = new Dictionary<Stock, List<PerformancePeriod>>();

                foreach (var stock in stocks)
                {
                    var svm = GetOrCreateStockPerformanceOverviewModel(i, stock);

                    var pp = CalculatePerformancePeriods(stock, intervals[i].DateFrom, intervals[i].DateTo);
                    dict[stock] = pp;

                    svm.SetPerformance(i, CalculatePerformance(pp));
                }

                if (interval == PerformanceInterval.Year)
                {
                    var svm = list.Single(x => x.Name == Constants.Total);
                    svm.SetPerformance(i, GetTotalYearPerformance(stocks, dict, intervals[i].DateFrom)-1);
                }
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
                list.Add(svm);
                return svm;
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

        /// <summary>
        /// Determine periods. Each transaction start with a new period
        /// </summary>
        private List<PerformancePeriod> CalculatePerformancePeriods(Stock stock, DateTime dateFrom, DateTime dateTo)
        {
            var lastStockQuantity = 0d;
            var moments = new List<Moment>();

            var fromValue = stock.StockValues.OrderByDescending(v => v.TimeStamp).FirstOrDefault(v => v.TimeStamp <= dateFrom)?.UserPrice ?? 0;
            if (fromValue > 0)
                moments.Add(CreateMoment(stock.Transactions.OrderByDescending(t => t.Id).First(t => t.StockValue.TimeStamp <= dateFrom), dateFrom, fromValue));
            else // stocks not in possession before date from
            {
                fromValue = stock.StockValues.OrderBy(v => v.TimeStamp).FirstOrDefault(v => v.TimeStamp >= dateFrom)?.UserPrice ?? 0;
                if (fromValue > 0)
                    moments.Add(CreateMoment(stock.Transactions.OrderBy(t => t.StockValue.TimeStamp).First(t => t.StockValue.TimeStamp >= dateFrom), userPrice: fromValue));
                else // stock not in possession during this period
                    return null;
            }

            var date = moments[0].Date < dateFrom ? dateFrom : moments[0].Date.AddDays(1);
            var soldAll = false;
            var nStocksByDividend = 0d;
            while (true) // loop over transactions within given period
            {
                var nextTransaction = stock.Transactions
                    .OrderBy(t => t.StockValue.TimeStamp)
                    .FirstOrDefault(t => t.StockValue.TimeStamp >= date && t.StockValue.TimeStamp <= dateTo);
                if (nextTransaction == null)
                {
                    var userPrice = stock.StockValues.OrderByDescending(v => v.TimeStamp).FirstOrDefault(v => v.TimeStamp.Date <= dateTo)?.UserPrice ?? 0;
                    if (userPrice > 0 && !soldAll) moments.Add(new Moment(dateTo, lastStockQuantity, userPrice, nStocksByDividend));
                    break;
                }

                if (nextTransaction.IsStockDividend())
                {
                    nStocksByDividend = nextTransaction.Quantity;
                    lastStockQuantity = stock.Transactions.Where(t => t.StockValue.TimeStamp <= nextTransaction.StockValue.TimeStamp).Sum(t => t.Quantity);
                    date = nextTransaction.StockValue.TimeStamp.Date.AddDays(1);
                    continue;
                }

                var moment = CreateMoment(nextTransaction, nStocksByDiv: nStocksByDividend);
                nStocksByDividend = 0;
                if (moment.StockQuantity == 0)
                {
                    if (soldAll) break; // everything already sold, so this moment adds nothing
                    soldAll = true;
                }
                moments.Add(moment);
                date = nextTransaction.StockValue.TimeStamp.Date.AddDays(1);
            }

            if (moments.All(m => m.StockQuantity == 0)) return null;

            var performancePeriods = new List<PerformancePeriod>();

            var previousMoment = moments.First();
            var lastMomentValue = previousMoment.StockQuantity * previousMoment.UserPrice;
            performancePeriods.Add(new PerformancePeriod(stock, previousMoment.Date, null, 1, lastMomentValue, lastMomentValue, previousMoment.UserPrice));
            foreach (var moment in moments.Skip(1))
            {
                var dividend = stock.Dividends.Where(d => d.TimeStamp >= previousMoment.Date && d.TimeStamp <= moment.Date).Sum(d => d.UserValue - d.UserCosts);

                var finalSum = moment.UserPrice * (previousMoment.StockQuantity + moment.NStocksAddedByDividend) + dividend;
                var finalSumInlcTransaction = finalSum + (moment.StockQuantity - previousMoment.StockQuantity) * moment.UserPrice;
                var performance = finalSum / (previousMoment.UserPrice * previousMoment.StockQuantity);
                int nDays = (int)(moment.Date - previousMoment.Date).TotalDays;
                var dailyPerformance = GrowthHelper.DailyGrowth(performance, nDays);

                performancePeriods.Add(new PerformancePeriod(stock, previousMoment.Date, nDays, dailyPerformance, finalSum, finalSumInlcTransaction, moment.UserPrice));
                previousMoment = moment;
            }

            return performancePeriods;

            Moment CreateMoment(Transaction transaction, DateTime? dt = null, double? userPrice = null, double nStocksByDiv = 0)
            {
                dt ??= transaction.StockValue.TimeStamp;
                lastStockQuantity = stock.Transactions.Where(t => t.StockValue.TimeStamp <= dt).Sum(t => t.Quantity);
                return new Moment(dt.Value, lastStockQuantity, userPrice ?? transaction.StockValue.UserPrice, nStocksByDiv);
            }
        }

        private double CalculatePerformance(List<PerformancePeriod> periods)
        {
            if (periods == null || periods.Count <= 1) return 0;

            var per = periods.Where(p => p.NDays.HasValue).ToList();
            var avgDailyPerformance = per.Sum(p => p.UserValueEndOfPeriod * p.NDays!.Value * p.DailyPerformance) / per.Sum(p => p.NDays!.Value * p.UserValueEndOfPeriod);

            var fromDate = periods[0].StartDate;
            var toDate = periods[^1].EndDate();
            return GrowthHelper.Performance(avgDailyPerformance, (int)(toDate-fromDate).TotalDays)-1;
        }

        /// <summary>
        /// todo: This works not correct
        /// </summary>
        private double GetTotalYearPerformance(List<Stock> stocks, Dictionary<Stock, List<PerformancePeriod>> dict, DateTime dateFrom)
        {
            var monthValues = new double[13];
            var transactionValues = new double[13];
            var transactionSum = 0d;
            var startDate = new DateTime(dateFrom.Year, 1, 1);
            for (int month = 1; month <= 12; month++)
            {
                var fromDate = startDate.AddMonths(month-1);
                var toDate = startDate.AddMonths(month).AddDays(-1);
                foreach (var stock in stocks)
                {
                    var periods = dict[stock];
                    if (periods == null || periods.Count <= 1) continue;
                    if (month == 1)
                    {
                        var lastPeriod = dict[stock].Last();
                        lastPeriod.NDays--; // set from 1-1 to 31-12
                    }

                    var firstPeriod = periods.First();
                    if (firstPeriod.StartDate.Month > month)
                        continue; // no stocks in possession at this time

                    if (firstPeriod.StartDate.Month == month) // set initial period
                    {
                        monthValues[month - 1] += periods.Single(p => p.NDays == null).UserValueEndOfPeriod;
                        fromDate = firstPeriod.StartDate.AddDays(1); // make sure initial transaction is out of range
                    }

                    var period = FindPeriod(month, periods);
                    if (period == null)
                        continue; // probably all sold

                    var transactions = stock.Transactions.IsBetween(fromDate, toDate).ToList();
                    var nMonthsToEndPeriod = period.EndDate().Month - month;
                    if (nMonthsToEndPeriod > 0)
                    {
                        if (transactions.Any(t => t.StockValue.UserPrice > 0.1)) // excl. stock div
                            throw new Exception($"ERRRR");
                        var nDays = (int)(period.EndDate() - period.EndDate().AddMonths(-nMonthsToEndPeriod)).TotalDays;
                        monthValues[month] += GrowthHelper.PastPrice(period.UserValueEndOfPeriod, period.DailyPerformance, nDays);
                        continue;
                    }
                    monthValues[month] += period.UserValueEndOfPeriodInclTransaction;

                    var value = transactions.Sum(t => t.Quantity * t.StockValue.UserPrice);
                    transactionValues[month] = value;
                    transactionSum += value;
                    //monthValues[month] -= transactionSum;
                }
            }

            var totalPerformance = 1d;
            var performances = new double[13]; // for debugging
            for (int i = 1; i <= 12; i++)
            {
                if (monthValues[i - 1] == 0 || monthValues[i] == 0) continue;
                var performance = (monthValues[i]-transactionValues[i]) / monthValues[i - 1];
                performances[i] = performance;
                totalPerformance *= performance;
            }

            return totalPerformance;

            PerformancePeriod FindPeriod(int month, List<PerformancePeriod> periods)
            {
                var period = periods.OrderByDescending(p => p.StartDate).FirstOrDefault(p => p.NDays.HasValue && month == p.EndDate().Month);
                if (period != null) return period;
                // no period found that ends this month
                return periods.OrderBy(p => p.StartDate).FirstOrDefault(p => p.NDays.HasValue && month >= p.StartDate.Month && month <= p.EndDate().Month);
            }
        }
    }

    class Moment
    {
        public DateTime Date { get; }
        public double StockQuantity { get; }
        public double UserPrice { get; }
        /// <summary>
        /// In case dividend is paid by adding stocks
        /// </summary>
        public double NStocksAddedByDividend { get; }

        public Moment(DateTime date, double stockQuantity, double userPrice, double nStocksByDividend = 0)
        {
            Date = date;
            StockQuantity = stockQuantity;
            UserPrice = userPrice;
            NStocksAddedByDividend = nStocksByDividend;
        }

        public override string ToString() => $"{Date:dd-MM-yyyy} nStocks:{StockQuantity} Price:{UserPrice:F2}";
    }

    /// <summary>
    /// Performance of given period.
    /// There can not be any transactions within this period
    /// </summary>
    class PerformancePeriod
    {
        public Stock Stock { get; }
        public DateTime StartDate { get; }
        /// <summary>
        /// Null when it is the initial period
        /// </summary>
        public int? NDays { get; set; }
        public double DailyPerformance { get; }
        /// <summary>
        /// Including dividend
        /// Excluding transaction at end period
        /// </summary>
        public double UserValueEndOfPeriod { get; }

        public double UserValueEndOfPeriodInclTransaction { get; }
        public double UserPriceEndOfPeriod { get; }

        public PerformancePeriod(Stock stock, DateTime startDate, int? nDays, double dailyPerformance, double userValueEndOfPeriod, double userValueEndOfPeriodInclTransaction, double userPriceEndOfPeriod)
        {
            Stock = stock;
            StartDate = startDate;
            NDays = nDays;
            DailyPerformance = dailyPerformance;
            UserValueEndOfPeriod = userValueEndOfPeriod;
            UserValueEndOfPeriodInclTransaction = userValueEndOfPeriodInclTransaction;
            UserPriceEndOfPeriod = userPriceEndOfPeriod;
        }

        public DateTime EndDate()
        {
            if (NDays == null) throw new Exception($"Could not determine the end-date of an initial period");
            return StartDate.AddDays(NDays.Value);
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