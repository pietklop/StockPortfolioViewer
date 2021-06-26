﻿using System;
using System.Collections.Generic;
using System.Linq;
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
                .Include(t => t.StockValue)
                .Where(t => isin == null || t.Stock.Isin == isin)
                .OrderByDescending(t => t.StockValue.TimeStamp).ToList();

            var list = new List<TransactionViewModel>(transactions.Count());

            int month = transactions.First().StockValue.TimeStamp.Month;
            int year = transactions.First().StockValue.TimeStamp.Year;
            var monthlyTransactions = new List<Transaction>();

            foreach (var transaction in transactions)
            {
                var currSymbol = transaction.Stock.Currency.Symbol;
                var date = transaction.StockValue.TimeStamp.Date;
                if (date.Month != month)
                {
                    AddSubTotal();
                    month = date.Month;
                    year = date.Year;
                }
                monthlyTransactions.Add(transaction);
                list.Add(new TransactionViewModel
                {
                    Name = transaction.Stock.Name,
                    Date = date.ToShortDateString(),
                    Quantity = transaction.Quantity,
                    Price = transaction.StockValue.NativePrice.FormatCurrency(currSymbol, false),
                    Value = NativeValue(transaction).FormatCurrency(currSymbol),
                    Costs = transaction.UserCosts.FormatUserCurrency(),
                });
            }

            AddSubTotal();

            return list;

            void AddSubTotal()
            {
                if (monthlyTransactions.Count == 0 || isin != null) return;
                list.Add(new TransactionViewModel
                {
                    Name = $"{TransactionViewModel.SumOf} {new DateTime(2000, month, 1):MMMM} {year}",
                    Value = monthlyTransactions.Sum(UserValue).FormatUserCurrency(),
                    Quantity = monthlyTransactions.Sum(t => t.Quantity),
                });
                monthlyTransactions.Clear();
            }

            double NativeValue(Transaction transaction) => transaction.StockValue.NativePrice * transaction.Quantity;
            double UserValue(Transaction transaction) => transaction.StockValue.UserPrice * transaction.Quantity;
        }

    }
}