﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DAL;
using DAL.Entities;
using log4net;
using Messages.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ImportProcessor
    {
        private readonly ILog log;
        private readonly StockDbContext db;
        private readonly StockService stockService;

        public ImportProcessor(ILog log, StockDbContext db, StockService stockService)
        {
            this.log = log;
            this.db = db;
            this.stockService = stockService;
        }

        public (int, int) Process(TransactionImportDto import)
        {
            var currencies = new List<Currency>();
            if (import.Dividends.Any(d => d.Currency != Constants.UserCurrency))
                currencies = db.Currencies.ToList();

            int nAddedTransactions = 0;
            int nAddedDividends = 0;
            foreach (var t in import.Transactions.OrderBy(t => t.TimeStamp).ToList())
                if (stockService.AddTransaction(t)) nAddedTransactions++;
            foreach (var importDividend in import.Dividends)
                if (stockService.AddDividend(importDividend, currencies)) nAddedDividends++;

            db.SaveChanges();

            log.Debug($"Successful imported {nAddedTransactions} transactions and {nAddedDividends} dividends");

            return (nAddedTransactions, nAddedDividends);
        }

        public int Process(StockValueImportDto import)
        {
            var stocks = db.Stocks
                .Include(s => s.Currency)
                .Include(s => s.LastKnownStockValue.StockValue)
                .Include(s => s.Transactions)
                .ToList();
            int stocksUpdated = 0;

            foreach (var sv in import.StockValues)
            {
                var stock = stocks.SingleOrDefault(s => s.Isin == sv.Isin) ?? throw new Exception($"Stock Name:'{sv.Name}' Isin:{sv.Isin} could not be found");
                if (stock.Currency.Key != sv.Currency) throw new Exception($"Currencies ({stock.Currency.Key} vs {sv.Currency}) do not match for '{sv.Name}' Isin:{sv.Isin}");

                if (stock.LastKnownStockValue.StockValue.TimeStamp >= sv.TimeStamp)
                    continue;

                if (sv.Quantity > 0 && Math.Abs(stock.Transactions.Sum(t => t.Quantity) - sv.Quantity) > 0.001) throw new Exception($"The total stock quantity for {stock} does not match");

                stockService.UpdateStockPrice(stock, sv.ClosePrice, sv.TimeStamp);
                stocksUpdated++;
            }

            UpdateCurrencies(import);

            db.SaveChanges();

            return stocksUpdated;
        }

        private void UpdateCurrencies(StockValueImportDto import)
        {
            var currKeys = import.StockValues.Select(s => s.Currency).Distinct().ToList();

            var currenciesDb = db.Currencies.ToList();
            foreach (var currKey in currKeys)
            {
                var currencyDb = currenciesDb.Single(c => c.Key == currKey);
                currencyDb.Ratio = import.StockValues.First(s => s.Currency == currKey).CurrencyRatio;
                currencyDb.LastUpdate = DateTime.Now;
            }
        }
    }
}