﻿using System;
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
    public class TransactionOverviewService
    {
        private readonly ILog log;
        private readonly StockDbContext db;

        public TransactionOverviewService(ILog log, StockDbContext db)
        {
            this.log = log;
            this.db = db;
        }

        public List<TransactionViewModel> GetStockList(string isin = null)
        {
            var transactions = db.Transactions
                .Include(t => t.Stock.Currency)
                .Include(t => t.Stock.LastKnownStockValue.StockValue)
                .Include(t => t.StockValue)
                .Where(t => isin == null || t.Stock.Isin == isin)
                .OrderByDescending(t => t.StockValue.TimeStamp).ToList();

            var list = new List<TransactionViewModel>(transactions.Count());

            int month = transactions.First().StockValue.TimeStamp.Month;
            int year = transactions.First().StockValue.TimeStamp.Year;
            var monthlyTransactions = new List<Transaction>();
            var annualTransactions = new List<Transaction>();

            foreach (var transaction in transactions)
            {
                var currSymbol = transaction.Stock.Currency.Symbol;
                var date = transaction.StockValue.TimeStamp.Date;
                if (date.Month != month)
                {
                    AddMonthlySubTotal();
                    month = date.Month;
                }
                if (date.Year != year)
                {
                    AddAnnualSubTotal();
                    year = date.Year;
                }
                monthlyTransactions.Add(transaction);
                annualTransactions.Add(transaction);
                var transactionPrice = transaction.StockValue.NativePrice;
                var currentPrice = transaction.Stock.LastKnownStockValue.StockValue.NativePrice;
                var diffPerc = (currentPrice - transactionPrice) / transactionPrice*100;
                var haveAnyStock = transactions.Where(t => t.StockId == transaction.StockId).Sum(t => t.Quantity) > 0;
                list.Add(new TransactionViewModel
                {
                    Name = transaction.Stock.Name,
                    Date = date.ToShortDateString(),
                    Quantity = transaction.Quantity,
                    Price = transactionPrice.FormatCurrency(currSymbol, false),
                    CurrentPrice = haveAnyStock ? $"{currentPrice.FormatCurrency(currSymbol, false)} ({diffPerc:F1}%)" : "",
                    NativeValue = NativeValue(transaction).FormatCurrency(currSymbol),
                    UserValue = UserValue(transaction).FormatUserCurrency(),
                    Costs = transaction.UserCosts.FormatUserCurrency(),
                    CurrRatio = CurrencyRatio(transaction),
                    HiddenPrice = transaction.StockValue.NativePrice,
                });
            }

            AddMonthlySubTotal();
            AddAnnualSubTotal();

            return list;

            string CurrencyRatio(Transaction transaction)
            {
                if (transaction.Stock.Currency.Key == Constants.UserCurrency) return "=";
                var ratio = transaction.StockValue.NativePrice / transaction.StockValue.UserPrice;
                return ratio.FormatCurrency(transaction.Stock.Currency.Symbol, false);
            }

            void AddMonthlySubTotal()
            {
                if (monthlyTransactions.Count == 0 || isin != null) return;
                list.Add(new TransactionViewModel
                {
                    Name = $"{TransactionViewModel.SumOf} {new DateTime(2000, month, 1):MMMM} {year}",
                    NativeValue = monthlyTransactions.Sum(UserValue).FormatUserCurrency(),
                    Quantity = monthlyTransactions.Sum(t => t.Quantity),
                });
                monthlyTransactions.Clear();
            }

            void AddAnnualSubTotal()
            {
                if (annualTransactions.Count == 0 || isin != null) return;
                list.Add(new TransactionViewModel
                {
                    Name = $"{TransactionViewModel.AnnualSumOf} {year}",
                    NativeValue = annualTransactions.Sum(UserValue).FormatUserCurrency(),
                    Quantity = annualTransactions.Sum(t => t.Quantity),
                });
                annualTransactions.Clear();
            }

            double NativeValue(Transaction transaction) => transaction.StockValue.NativePrice * transaction.Quantity;
            double UserValue(Transaction transaction) => transaction.StockValue.UserPrice * transaction.Quantity;
        }

    }
}