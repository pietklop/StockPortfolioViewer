using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DAL;
using DAL.Entities;
using log4net;
using Messages.UI.StockDetails;
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

        public List<StockPropertyViewModel> GetDetails(string stockIsin)
        {
            var stock = db.Stocks
                            .Include(s => s.AreaShares).ThenInclude(a => a.Area)
                            .Include(s => s.Currency)
                            .Include(s => s.Dividends)
                            .Include(s => s.LastKnownStockValue.StockValue)
                            .Include(s => s.SectorShares).ThenInclude(a => a.Sector)
                            .Include(s => s.Transactions).ThenInclude(t => t.StockValue)
                            .SingleOrDefault(s => s.Isin == stockIsin) ?? throw new Exception($"Could not find stock with Isin: '{stockIsin}'");

            return new List<StockPropertyViewModel>
            {
                new StockPropertyViewModel{Name = StockDetailProperties.Name, Value = stock.Name, UnderlineRow = true},
                new StockPropertyViewModel{Name = "Isin", Value = stock.Isin},
                new StockPropertyViewModel{Name = StockDetailProperties.Symbol, Value = stock.Symbol, UnderlineRow = true},
                new StockPropertyViewModel{Name = "Avg buy price", Value = FormatCurrency(stock.Transactions.DetermineAvgBuyNativePrice())},
                new StockPropertyViewModel{Name = StockDetailProperties.CurrentPrice, Value = FormatCurrency(stock.LastKnownStockValue.StockValue.NativePrice), UnderlineRow = true},
                new StockPropertyViewModel{Name = StockDetailProperties.LastPriceUpdate, Value = LastUpdateSince(), UnderlineRow = true},
                new StockPropertyViewModel{Name = "Currency", Value = stock.Currency.Key},
                new StockPropertyViewModel{Name = "Area", Value = FirstOrMultiple(stock.AreaShares.Select(a => a.Area.Name).ToArray())},
                new StockPropertyViewModel{Name = "Sector", Value = FirstOrMultiple(stock.SectorShares.Select(a => a.Sector.Name).ToArray())},
            };

            string FormatCurrency(double value) => $"{stock.Currency.Symbol}{value:#,###.##}";

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