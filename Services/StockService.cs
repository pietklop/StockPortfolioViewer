using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DAL;
using DAL.Entities;
using Imports;
using log4net;
using Messages.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class StockService
    {
        private readonly ILog log;
        private readonly StockDbContext db;

        public StockService(ILog log, StockDbContext db)
        {
            this.log = log;
            this.db = db;
        }

        public Stock GetOrCreateStock(string name, string isin, string currency)
        {
            var stock = db.Stocks
                .Include(s => s.Currency)
                .Include(s => s.LastKnownStockValue.StockValue)
                .FirstOrDefault(s => s.Isin == isin);

            if (stock == null)
            {
                log.Info($"Create stock: {name} - {isin}");
                var curr = db.Currencies.SingleOrDefault(c => c.Key == currency) ?? new Currency {Key = currency};
                stock = new Stock { Name = name, Isin = isin, Currency = curr };
                var sector = db.Sectors.SingleOrDefault(s => s.Name == Constants.Unknown) ?? throw new Exception($"Could not find sector: {Constants.Unknown}");
                stock.SectorShares ??= new List<SectorShare>();
                stock.SectorShares.Add(new SectorShare {Sector = sector, Fraction = 1});

                var area = db.Areas.SingleOrDefault(s => s.Name == Constants.Unknown) ?? throw new Exception($"Could not find area: {Constants.Unknown}");
                stock.AreaShares ??= new List<AreaShare>();
                stock.AreaShares.Add(new AreaShare {Area = area, Fraction = 1});

                db.Stocks.Add(stock);
            }

            return stock;
        }

        public void AddTransaction(TransactionDto dto)
        {
            log.Info($"Create transaction: {dto.Name} Quantity: {dto.Quantity} on {dto.TimeStamp.ToShortDateString()}");
            var stock = GetOrCreateStock(dto.Name, dto.Isin, dto.Currency);
            if (stock.Currency.Key != dto.Currency) 
                throw new Exception($"Stock currency ('{stock.Currency.Key}') should be equal to transaction currency ('{dto.Currency}')");

            if (DuplicateTransaction(dto))
            {
                log.Debug($"Transaction ({dto.Guid}) does already exist => skip");
                return;
            }
            ValidateEnoughToSell();

            var pitStockValue = CreatePitStockValue();
            var trans = new Transaction
            {
                Stock = stock,
                StockValue = pitStockValue,
                Created = DateTime.Now,
                Quantity = dto.Quantity,
                UserCosts = dto.Costs.ToUserCurrency(dto.CurrencyRatio, dto.Currency),
                ExtRef = dto.Guid,
            };

            db.Transactions.Add(trans);
            db.SaveChanges(); // we need to save each transition because a later one can be the sell

            if (stock.LastKnownStockValue == null)
                stock.LastKnownStockValue = new LastKnownStockValue {StockValue = pitStockValue};
            else if (stock.LastKnownStockValue.StockValue.TimeStamp < dto.TimeStamp)
                stock.LastKnownStockValue.StockValue = pitStockValue;

            void ValidateEnoughToSell()
            {
                if (dto.Quantity < 0)
                {
                    var transactions = db.Transactions.Where(t => t.StockId == stock.Id).ToList();
                    var stockTotal = transactions.Sum(t => t.Quantity);
                    if (stockTotal + dto.Quantity < 0) throw new Exception($"Not enough {stock} to sell. Trans.Ref: {dto.Guid}");
                }
            }

            PitStockValue CreatePitStockValue()
            {
                return new PitStockValue
                {
                    Stock = stock,
                    TimeStamp = dto.TimeStamp,
                    NativePrice = dto.Price,
                    UserPrice = dto.Price.ToUserCurrency(dto.CurrencyRatio, dto.Currency),
                };
            }
        }

        private bool DuplicateTransaction(TransactionDto dto) =>
            db.Transactions.Any(t => t.ExtRef == dto.Guid && t.Stock.Isin == dto.Isin && t.StockValue.TimeStamp.Date == dto.TimeStamp.Date);

        public void AddDividend(DividendDto dto)
        {
            var stock = db.Stocks.SingleOrDefault(s => s.Isin == dto.Isin) ?? throw new Exception($"Can not find stock {dto.Isin} to book dividend");
            log.Info($"Create dividend for {stock} Value: {dto.Value} on {dto.TimeStamp.ToShortDateString()}");

            var div = new Dividend
            {
                Stock = stock,
                Created = DateTime.Now,
                TimeStamp = dto.TimeStamp,
                NativeValue = Constants.UserCurrency != dto.Currency || stock.Currency.Key == Constants.UserCurrency ? (double?)dto.Value : null,
                UserValue = dto.Value.ToUserCurrency(dto.CurrencyRatio, dto.Currency),
                UserCosts = dto.Costs.ToUserCurrency(dto.CurrencyRatio, dto.Currency),
                UserTax = dto.Tax.ToUserCurrency(dto.CurrencyRatio, dto.Currency),
            };

            db.Dividends.Add(div);
        }
    }
}