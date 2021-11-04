using System.Collections.Generic;
using System.Linq;
using DAL;
using Messages.UI.Overview;
using Microsoft.EntityFrameworkCore;

namespace Services.Ui
{
    public class PortfolioDistributionService
    {
        private readonly StockDbContext db;

        public PortfolioDistributionService(StockDbContext db)
        {
            this.db = db;
        }

        public PortfolioDistributionDto GetCurrencyDistribution()
        {
            var data = db.Stocks
                .Where(s => s.Transactions.Sum(t => t.Quantity) > 0)
                .Select(s => new
                {
                    currency = s.Currency.Key,
                    value = s.Transactions.Sum(t => t.Quantity) * s.LastKnownUserPrice,
                }).ToList();
                
            var grouped = data.GroupBy(d => d.currency).Select(g => new
            {
                currency = g.Key,
                sum = g.Sum(x => x.value),
            }).OrderByDescending(s => s.sum).ToList();

            return new PortfolioDistributionDto("Currency distribution", grouped.Select(d => d.currency).ToArray(), grouped.Select(g => g.sum).ToArray());
        }

        public PortfolioDistributionDto GetAreaDistributionByContinent() => GetAreaDistribution(null, true);

        public PortfolioDistributionDto GetAreaDistribution(string isin = null, bool groupByContinent = false)
        {
            var data = db.Stocks
                .Include(s => s.LastKnownStockValue.StockValue)
                .Include(s => s.StockValues)
                .Include(s => s.Transactions)
                .Include(s => s.AreaShares).ThenInclude(a => a.Area.Continent)
                .Where(s => isin == null || s.Isin == isin)
                .Where(s => s.Transactions.Sum(t => t.Quantity) > 0)
                .ToList();

            var areaValueDict = new Dictionary<string, double>();

            foreach (var stock in data)
            {
                var value = stock.Transactions.Sum(t => t.Quantity) * stock.LastKnownUserPrice;
                foreach (var areaShare in stock.AreaShares)
                {
                    var name = groupByContinent ? areaShare.Area.Continent?.Name ?? areaShare.Area.Name : areaShare.Area.Name;
                    if (areaValueDict.ContainsKey(name))
                        areaValueDict[name] += value * areaShare.Fraction;
                    else
                        areaValueDict[name] = value * areaShare.Fraction;
                }
            }

            var sorted = areaValueDict.OrderByDescending(d => d.Value).Select(d => new
            {
                d.Key,
                d.Value,
            }).ToList();

            return new PortfolioDistributionDto("Area distribution", sorted.Select(d => d.Key).ToArray(), sorted.Select(g => g.Value).ToArray(), true);
        }

        public PortfolioDistributionDto GetSectorDistribution(string isin = null)
        {
            var data = db.Stocks
                .Include(s => s.LastKnownStockValue.StockValue)
                .Include(s => s.StockValues)
                .Include(s => s.Transactions)
                .Include(s => s.SectorShares).ThenInclude(a => a.Sector)
                .Where(s => isin == null || s.Isin == isin)
                .Where(s => s.Transactions.Sum(t => t.Quantity) > 0)
                .ToList();

            var sectorValueDict = new Dictionary<string, double>();

            foreach (var stock in data)
            {
                var value = stock.Transactions.Sum(t => t.Quantity) * stock.LastKnownUserPrice;
                foreach (var sectorShare in stock.SectorShares)
                {
                    if (sectorValueDict.ContainsKey(sectorShare.Sector.Name))
                        sectorValueDict[sectorShare.Sector.Name] += value * sectorShare.Fraction;
                    else
                        sectorValueDict[sectorShare.Sector.Name] = value * sectorShare.Fraction;
                }
            }

            var sorted = sectorValueDict.OrderByDescending(d => d.Value).Select(d => new
            {
                d.Key,
                d.Value,
            }).ToList();

            return new PortfolioDistributionDto("Sector distribution", sorted.Select(d => d.Key).ToArray(), sorted.Select(g => g.Value).ToArray());
        }
    }
}