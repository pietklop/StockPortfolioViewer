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

            int nDaysDivExDiff = 14; // payout is always later then ex-div date

            foreach (var dividend in dividends)
            {
                var stock = dividend.Stock;
                var psv = ClosestValue(stock.StockValues, dividend.TimeStamp);
                var nStocksPit = stock.Transactions.Where(t => t.StockValue.TimeStamp < dividend.TimeStamp.AddDays(-nDaysDivExDiff)).Sum(t => t.Quantity);
                var nettDividend = dividend.UserValue - dividend.UserTax - dividend.UserCosts;
                var divFraction = nettDividend / psv.UserPrice / nStocksPit;
                var percString = $"{divFraction:P2}";
                if (dividend.IsCapitalReturn())
                {}
                else if (stock.DividendPayoutInterval == DividendPayoutInterval.Unknown)
                    percString += $" (?)";
                else
                    percString += $" ({divFraction * stock.DividendPayoutInterval.ToYearMultiplier():P1})";

                string dateString = dividend.TimeStamp.ToShortDateString();
                if (!dividend.IsCapitalReturn() && CurrentlyOwned(stock) && LastDividendOfStock(dividend) && ExpectedNextDividendOrUnknown(stock.DividendPayoutInterval, dividend.TimeStamp))
                    dateString += "*";

                list.Add(new DividendViewModel
                {
                    Name = stock.Name,
                    Date = dateString,
                    NettValue = nettDividend.FormatUserCurrency(),
                    Tax = dividend.IsCapitalReturn() ? "" : dividend.UserTax.FormatUserCurrency(),
                    Costs = dividend.UserCosts.FormatUserCurrency(),
                    PayoutInterval = dividend.IsCapitalReturn() ? "Cap. return" : stock.DividendPayoutInterval.ToString(),
                    Percentage = percString,
                });
            }

            return list;

            bool CurrentlyOwned(Stock stock) => stock.Transactions.Sum(t => t.Quantity) > 0;

            bool LastDividendOfStock(Dividend dividend) => dividends.Where(d => d.UserTax > 0).First(d => d.StockId == dividend.StockId).Id == dividend.Id;

            bool ExpectedNextDividendOrUnknown(DividendPayoutInterval interval, DateTime date)
            {
                if (interval == DividendPayoutInterval.Unknown || interval == DividendPayoutInterval.Accumulated)
                    return true; // wrong/missing configuration
                if (interval == DividendPayoutInterval.GrowthStock)
                    return false;
                int intervalInDays = (int)(365 / interval.ToYearMultiplier()) + 5;
                return (DateTime.Now - date).TotalDays > intervalInDays;
            }

            PitStockValue ClosestValue(ICollection<PitStockValue> stockStockValues, DateTime date) =>
                stockStockValues.OrderBy(s => (date - s.TimeStamp).Duration()).First();
        }

    }
}