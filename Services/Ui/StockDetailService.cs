using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DAL;
using DAL.Entities;
using log4net;
using Messages.UI;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;

namespace Services.Ui
{
    public class StockDetailService
    {
        private readonly ILog log;
        private readonly StockDbContext db;

        public StockDetailService(ILog log, StockDbContext db)
        {
            this.log = log;
            this.db = db;
        }

        public List<PropertyViewModel> GetDetails(string stockIsin)
        {
            var stock = db.Stocks
                            .Include(s => s.AreaShares).ThenInclude(a => a.Area)
                            .Include(s => s.Currency)
                            .Include(s => s.Dividends)
                            .Include(s => s.LastKnownStockValue.StockValue)
                            .Include(s => s.SectorShares).ThenInclude(a => a.Sector)
                            .Include(s => s.Transactions).ThenInclude(t => t.StockValue)
                            .SingleOrDefault(s => s.Isin == stockIsin) ?? throw new Exception($"Could not find stock with Isin: '{stockIsin}'");

            var transactions = stock.Transactions;
            var nStocks = transactions.Sum(t => t.Quantity);
            var userBuyValue = transactions.IsBuy().Sum(t => t.Quantity * t.StockValue.UserPrice);
            var userSalesValue = transactions.IsSell().Sum(t => -t.Quantity * t.StockValue.UserPrice);
            var userTransactionCosts = transactions.Sum(t => t.UserCosts);
            var currentUserValue = stock.LastKnownUserPrice * nStocks;
            var userDividendValue = stock.Dividends.Sum(d => d.UserValue - d.UserCosts);
            var profit = currentUserValue + userSalesValue - userBuyValue - userTransactionCosts + userDividendValue;
            var alarmCondition = stock.AlarmCondition.ToString();
            if (stock.AlarmCondition != AlarmCondition.None)
                alarmCondition += $" {stock.AlarmThreshold}";

            return new List<PropertyViewModel>
            {
                new PropertyViewModel{Name = StockDetailProperties.Name, Value = stock.Name, UnderlineRow = true},
                new PropertyViewModel{Name = "Isin", Value = stock.Isin},
                new PropertyViewModel{Name = "Quantity", Value = nStocks},
                new PropertyViewModel{Name = StockDetailProperties.Symbol, Value = stock.Symbol, UnderlineRow = true},
                new PropertyViewModel{Name = StockDetailProperties.CurrentPrice, Value = FormatCurrency(stock.LastKnownStockValue.StockValue.NativePrice), UnderlineRow = true},
                new PropertyViewModel{Name = StockDetailProperties.LastPriceUpdate, Value = LastUpdateSince(), UnderlineRow = true},
                new PropertyViewModel{Name = "Avg buy price", Value = FormatCurrency(stock.Transactions.DetermineAvgBuyNativePrice())},
                new PropertyViewModel{Name = "First buy", Value = $"{transactions.OrderBy(t => t.StockValue.TimeStamp).First().StockValue.TimeStamp.ToShortDateString()}"},
                new PropertyViewModel{Name = StockDetailProperties.Bought, Value = $"{FormatUserCurrency(userBuyValue)}  [{transactions.IsBuy().Count()}]", UnderlineRow = true},
                new PropertyViewModel{Name = StockDetailProperties.Sold, Value = $"{FormatUserCurrency(userSalesValue)}  [{transactions.IsSell().Count()}]", UnderlineRow = true},
                new PropertyViewModel{Name = "Current total value", Value = $"{FormatUserCurrency(currentUserValue)}"},
                new PropertyViewModel{Name = "Profit", Value = $"{FormatUserCurrency(profit)}"},
                new PropertyViewModel{Name = StockDetailProperties.Dividend, Value = $"{FormatUserCurrency(userDividendValue)}  [{stock.Dividends.Count}]", UnderlineRow = true},
                new PropertyViewModel{Name = StockDetailProperties.DividendPayout, Value = stock.DividendPayoutInterval, UnderlineRow = true},
                new PropertyViewModel{Name = StockDetailProperties.TransactionCosts, Value = $"{FormatUserCurrency(userTransactionCosts)}  [{transactions.Count}]", UnderlineRow = true},
                new PropertyViewModel{Name = "Currency", Value = stock.Currency.Key},
                new PropertyViewModel{Name = StockDetailProperties.ExpenseRatio, Value = $"{stock.ExpenseRatio:P2}", UnderlineRow = true},
                new PropertyViewModel{Name = StockDetailProperties.Area, Value = FirstOrMultiple(stock.AreaShares.Select(a => a.Area.Name).ToArray()), UnderlineRow = true},
                new PropertyViewModel{Name = StockDetailProperties.Sector, Value = FirstOrMultiple(stock.SectorShares.Select(a => a.Sector.Name).ToArray()), UnderlineRow = true},
                new PropertyViewModel{Name = StockDetailProperties.AlarmCondition, Value = alarmCondition, UnderlineRow = true},
                new PropertyViewModel{Name = StockDetailProperties.Remarks, Value = stock.Remarks, UnderlineRow = true},
            };

            string FormatUserCurrency(double value) => value.FormatUserCurrency();
            string FormatCurrency(double value) => value.FormatCurrency(stock.Currency.Symbol, false);

            string FirstOrMultiple(string[] fields)
            {
                if (fields.Length == 0) return Constants.Unknown;
                if (fields.Length == 1) return fields[0];
                return "(Multiple)";
            }

            string LastUpdateSince() => (DateTime.Now - stock.LastKnownStockValue.StockValue.TimeStamp).TimeAgo();
        }
    }
}