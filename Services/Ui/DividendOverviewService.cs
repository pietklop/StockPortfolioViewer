using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DAL;
using DAL.Entities;
using log4net;
using Messages.UI.Overview;
using Microsoft.EntityFrameworkCore;

namespace Services.Ui
{
    public class DividendOverviewService
    {
        private readonly ILog log;
        private readonly StockDbContext db;

        public DividendOverviewService(ILog log, StockDbContext db)
        {
            this.log = log;
            this.db = db;
        }

        public List<DividendViewModel> GetStockList(string isin = null)
        {
            var dividends = db.Dividends
                .Include(t => t.Stock.Currency)
                .Include(t => t.Stock.StockValues)
                .Include(t => t.Stock.Transactions).ThenInclude(t => t.StockValue)
                .Where(t => isin == null || t.Stock.Isin == isin)
                .OrderByDescending(t => t.TimeStamp).ToList();

            var list = new List<DividendViewModel>(dividends.Count());

            foreach (var dividend in dividends)
            {
                var stock = dividend.Stock;
                var psv = ClosestValue(stock.StockValues, dividend.TimeStamp);
                var currSymbol = dividend.Stock.Currency.Symbol;
                var nStocksPit = stock.Transactions.Where(t => t.StockValue.TimeStamp < dividend.TimeStamp).Sum(t => t.Quantity);
                var divFraction = dividend.UserValue / psv.UserPrice / nStocksPit;
                var percString = $"{divFraction:P2}";
                if (stock.DividendPayoutInterval == DividendPayoutInterval.Unknown)
                    percString += $" (?)";
                else
                    percString += $" ({divFraction * stock.DividendPayoutInterval.ToYearMultiplier():P1})";

                string dateString = dividend.TimeStamp.ToShortDateString();
                if (LastDividendOfStock(dividend) && ExpectedNextDividend(stock.DividendPayoutInterval, dividend.TimeStamp))
                    dateString += "*";

                list.Add(new DividendViewModel
                {
                    Name = stock.Name,
                    Date = dateString,
                    Value = (dividend.UserValue).FormatUserCurrency(),
                    PayoutInterval = stock.DividendPayoutInterval.ToString(),
                    Percentage = percString,
                });
            }

            return list;

            bool LastDividendOfStock(Dividend dividend) => dividends.First(d => d.StockId == dividend.StockId).Id == dividend.Id;

            bool ExpectedNextDividend(DividendPayoutInterval interval, DateTime date)
            {
                int intervalInDays = (int)(365 / interval.ToYearMultiplier()) + 5;
                return (DateTime.Now - date).TotalDays > intervalInDays;
            }

            PitStockValue ClosestValue(ICollection<PitStockValue> stockStockValues, DateTime date) =>
                stockStockValues.OrderBy(s => (date - s.TimeStamp).Duration()).First();
        }

    }
}