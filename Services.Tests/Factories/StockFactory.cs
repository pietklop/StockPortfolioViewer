using System;
using System.Collections.Generic;
using DAL;
using DAL.Entities;

namespace Services.Tests.Factories
{
    public static class StockFactory
    {
        public static Stock AddTest(StockDbContext db, string name = "Stock1", string isin = "isin1", Currency currency = null)
        {
            var stock = db.Stocks.Add(new Stock
            {
                Name = name,
                Isin = isin,
                Currency = currency ?? CurrencyFactory.CreateTest(),

            }).Entity;

            db.SaveChanges();

            return stock;
        }

        public static PitStockValue AddValue(this Stock stock, double nativePrice, DateTime dateTime, double currencyRatio = 1, bool updateLastKnownValue = false)
        {
            stock.StockValues ??= new List<PitStockValue>();

            var stockValue = new PitStockValue
            {
                TimeStamp = dateTime,
                NativePrice = nativePrice,
                UserPrice = nativePrice.ToUserCurrency(currencyRatio),
            };
            stock.StockValues.Add(stockValue);

            if (updateLastKnownValue)
                stock.LastKnownStockValue = new LastKnownStockValue { LastUpdate = dateTime, StockValue = stockValue };

            return stockValue;
        }

        public static Dividend AddDividend(this Stock stock, DateTime dateTime, double nativeValue, double currencyRatio = 1)
        {
            stock.Dividends ??= new List<Dividend>();

            var div = new Dividend
            {
                Created = dateTime,
                TimeStamp = dateTime,
                NativeValue = nativeValue,
                UserValue = nativeValue.ToUserCurrency(currencyRatio),
            };

            stock.Dividends.Add(div);

            return div;
        }

        public static Transaction AddBuy(this Stock stock, DateTime dateTime, double quantity, double nativePrice, double currencyRatio = 1, double costs = 0)
        {
            if (quantity <= 0) throw new Exception($"{nameof(quantity)} must be larger than 0");
            return stock.AddTransaction(dateTime, quantity, nativePrice, currencyRatio, costs);
        }

        public static Transaction AddSell(this Stock stock, DateTime dateTime, double quantity, double nativePrice, double currencyRatio = 1, double costs = 0)
        {
            if (quantity <= 0) throw new Exception($"{nameof(quantity)} must be larger than 0");
            return stock.AddTransaction(dateTime, -quantity, nativePrice, currencyRatio, costs);
        }

        private static Transaction AddTransaction(this Stock stock, DateTime dateTime, double quantity, double nativePrice, double currencyRatio, double costs)
        {
            stock.Transactions ??= new List<Transaction>();

            var transaction = new Transaction
            {
                Created = dateTime,
                Quantity = quantity,
                StockValue = new PitStockValue
                {
                    Stock = stock,
                    TimeStamp = dateTime,
                    NativePrice = nativePrice,
                    UserPrice = nativePrice.ToUserCurrency(currencyRatio),
                },
                UserCosts = costs.ToUserCurrency(currencyRatio),
            };

            stock.Transactions.Add(transaction);

            return transaction;
        }
    }
}