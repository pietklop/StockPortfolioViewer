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
using Services.Helpers;

namespace Services
{
    public class StockService
    {
        private readonly ILog log;
        private readonly StockDbContext db;
        private readonly Settings settings;

        public StockService(ILog log, StockDbContext db, Settings settings)
        {
            this.log = log;
            this.db = db;
            this.settings = settings;
        }

        public Stock GetStockOrThrow(string isin) =>
            GetStock(isin) ?? throw new Exception($"Could not find stock with isin: '{isin}'");

        private Stock GetStock(string isin)
        {
            return db.Stocks
                .Include(s => s.Currency)
                .Include(s => s.LastKnownStockValue.StockValue)
                .SingleOrDefault(s => s.Isin == isin);
        }

        public Stock GetOrCreateStock(string name, string isin, string currency)
        {
            var stock = GetStock(isin);

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

        public Stock UpdateStockPrice(string isin, double nativePrice, DateTime? lastPriceUpdate = null) =>
            UpdateStockPrice(GetStockOrThrow(isin), nativePrice, lastPriceUpdate);

        public Stock UpdateStockPrice(Stock stock, double nativePrice, DateTime? lastPriceUpdate = null)
        {
            log.Info($"Set latest stock price: {stock} to: {stock.Currency.Symbol}{nativePrice}");

            var now = DateTime.Now;

            double userPrice;
            if (stock.IsUserCurrency()) userPrice = nativePrice;
            else
            {
                var curr = db.Currencies.Get(stock.Currency.Key);
                if ((now - curr.LastUpdate).TotalDays >= settings.CurrencyRatioExpiresAfterHours)
                    throw new Exception($"Currency '{curr}' ratio has expired.");
                userPrice = nativePrice.ToUserCurrency(curr.Ratio);
            }

            stock.LastKnownStockValue.LastUpdate = now;
            lastPriceUpdate ??= now;
            if (stock.LastKnownStockValue.StockValue.TimeStamp.Date == lastPriceUpdate.Value.Date)
            {   // do not save multiple value updates per day
                stock.LastKnownStockValue.StockValue.TimeStamp = lastPriceUpdate.Value;
                stock.LastKnownStockValue.StockValue.NativePrice = nativePrice;
                stock.LastKnownStockValue.StockValue.UserPrice = userPrice;
            }
            else
                stock.LastKnownStockValue.StockValue = CreatePitStockValue();

            UpdateDailyGrowth(stock.LastKnownStockValue.StockValue, false);
            
            return stock;

            PitStockValue CreatePitStockValue()
            {
                return new PitStockValue
                {
                    Stock = stock,
                    TimeStamp = lastPriceUpdate.Value,
                    NativePrice = nativePrice,
                    UserPrice = userPrice,
                };
            }
        }

        public bool AddTransaction(TransactionDto dto)
        {
            log.Info($"Create transaction: {dto.Name} Quantity: {dto.Quantity} on {dto.TimeStamp.ToShortDateString()}");
            var stock = GetOrCreateStock(dto.Name, dto.Isin, dto.Currency);

            if (stock.Currency.Key != dto.Currency) 
                throw new Exception($"Stock ({stock}) currency ('{stock.Currency.Key}') should be equal to transaction currency ('{dto.Currency}')");

            if (DuplicateTransaction(dto))
            {
                log.Debug($"Transaction ({dto.Guid}) does already exist => skip");
                return false;
            }
            ValidateEnoughToSell();

            var pitStockValue = GetOrCreatePitStockValue(stock.LastKnownStockValue == null);
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
                stock.LastKnownStockValue = new LastKnownStockValue {StockValue = pitStockValue, LastUpdate = dto.TimeStamp};
            else if (stock.LastKnownStockValue.StockValue.TimeStamp < dto.TimeStamp)
            {
                stock.LastKnownStockValue.StockValue = pitStockValue;
                stock.LastKnownStockValue.LastUpdate = dto.TimeStamp;
            }

            return true;

            void ValidateEnoughToSell()
            {
                if (dto.Quantity < 0)
                {
                    var transactions = db.Transactions.Where(t => t.StockId == stock.Id).ToList();
                    var stockTotal = transactions.Sum(t => t.Quantity);
                    if (stockTotal + dto.Quantity < 0) throw new Exception($"Not enough {stock} to sell. Trans.Ref: {dto.Guid}");
                }
            }

            PitStockValue GetOrCreatePitStockValue(bool initialTransaction)
            {
                var psv = db.PitStockValues.FirstOrDefault(p => p.Stock.Isin == dto.Isin && p.TimeStamp.Date == dto.TimeStamp.Date);
                if (psv != null)
                {   // StockValue does already exist for this day => update
                    psv.NativePrice = dto.Price;
                    psv.UserPrice = dto.Price.ToUserCurrency(dto.CurrencyRatio, dto.Currency);
                }
                else
                    psv = new PitStockValue
                    {
                        Stock = stock,
                        TimeStamp = dto.TimeStamp,
                        NativePrice = dto.Price,
                        UserPrice = dto.Price.ToUserCurrency(dto.CurrencyRatio, dto.Currency),
                    };

                UpdateDailyGrowth(psv, initialTransaction);

                return psv;
            }
        }

        private void UpdateDailyGrowth(PitStockValue psv, bool initialTransaction)
        {
            if (initialTransaction)
                psv.DailyGrowth = 1;
            else
            {
                var previousPsv = db.PitStockValues.First(p => p.Stock.Isin == psv.Stock.Isin && p.TimeStamp < psv.TimeStamp)
                                  ?? throw new Exception($"Could not find previous PitStockValue {psv.Stock}");
                psv.DailyGrowth = GrowthHelper.DailyGrowth(previousPsv, psv);
            }
        }

        private bool DuplicateTransaction(TransactionDto dto) =>
            db.Transactions.Any(t => t.ExtRef == dto.Guid && t.Stock.Isin == dto.Isin && t.StockValue.TimeStamp.Date == dto.TimeStamp.Date);

        public bool AddDividend(DividendDto dto)
        {
            var stock = db.Stocks
                            .Include(s => s.Dividends)
                            .SingleOrDefault(s => s.Isin == dto.Isin) ?? throw new Exception($"Can not find stock {dto.Isin} to book dividend from {dto.TimeStamp.ToShortDateString()}");
            log.Info($"Create dividend for {stock} Value: {dto.Value} on {dto.TimeStamp.ToShortDateString()}");

            if (DuplicateDividend(stock, dto.TimeStamp))
            {
                log.Debug($"Dividend already exists for {stock} on {dto.TimeStamp.ToShortDateString()} => skip");
                return false;
            }

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

            return true;
        }

        private bool DuplicateDividend(Stock stock, DateTime dtoTimeStamp) =>
            stock.Dividends.Any(d => d.TimeStamp.Date == dtoTimeStamp.Date);
    }
}