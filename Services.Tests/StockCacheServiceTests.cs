using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Entities;
using FakeItEasy;
using log4net;
using Services.Tests.Factories;
using Services.Tests.Infra;
using Shouldly;
using Xunit;

namespace Services.Tests
{
    public class StockCacheServiceTests : DbTestBase
    {
        private readonly StockCacheService _stockCacheService;

        public StockCacheServiceTests()
        {
            _stockCacheService = new StockCacheService(A.Fake<ILog>(), new StockDbContextFactory(Options));
        }

        [Fact]
        public void Reload_LoadsCompleteStockGraph()
        {
            using (var db = CreateDbContext())
            {
                var stock = StockFactory.AddTest(db);
                var buy = stock.AddBuy(new DateTime(2025, 1, 2), 10, 100);
                stock.AddValue(110, new DateTime(2025, 2, 3));
                stock.AddDividend(new DateTime(2025, 3, 4), 5);
                stock.LastKnownStockValue = new LastKnownStockValue { StockValue = buy.StockValue, LastUpdate = buy.StockValue.TimeStamp };
                var continent = AreaFactory.AddTest(db, "Europe", true);
                stock.AreaShares = new List<AreaShare> { new AreaShare { Area = new Area { Name = "Netherlands", Continent = continent }, Fraction = 1 } };
                stock.SectorShares = new List<SectorShare> { new SectorShare { Sector = new Sector { Name = "Tech" }, Fraction = 1 } };
                db.SaveChanges();
            }

            _stockCacheService.Reload();

            var cachedStock = _stockCacheService.Stocks.ShouldHaveSingleItem();
            _stockCacheService.GetStockOrThrow("isin1").ShouldBeSameAs(cachedStock);
            cachedStock.Currency.ShouldNotBeNull();
            cachedStock.StockValues.Count.ShouldBe(2);
            cachedStock.AreaShares.Single().Area.Continent!.Name.ShouldBe("Europe");
            cachedStock.SectorShares.Single().Sector.Name.ShouldBe("Tech");

            var transaction = _stockCacheService.Transactions.ShouldHaveSingleItem();
            transaction.Stock.ShouldBeSameAs(cachedStock);
            transaction.StockValue.ShouldBeSameAs(cachedStock.StockValues.Single(v => v.Id == transaction.StockValue.Id));
            cachedStock.LastKnownStockValue.StockValue.ShouldBeSameAs(transaction.StockValue);
            _stockCacheService.StockValues.ShouldAllBe(v => v.Stock == cachedStock);
            _stockCacheService.Dividends.ShouldHaveSingleItem().Stock.ShouldBeSameAs(cachedStock);
        }

        [Fact]
        public void Stocks_NotLoaded_LoadsOnFirstUse()
        {
            using (var db = CreateDbContext())
                StockFactory.AddTest(db);

            _stockCacheService.Stocks.ShouldHaveSingleItem();
        }

        [Fact]
        public void Reload_AfterDatabaseChange_ContainsChanges()
        {
            using (var db = CreateDbContext())
            {
                StockFactory.AddTest(db).AddBuy(new DateTime(2025, 1, 2), 10, 100);
                db.SaveChanges();
            }
            _stockCacheService.Reload();

            using (var db = CreateDbContext())
            {
                db.Stocks.Single().AddSell(new DateTime(2025, 2, 3), 4, 120);
                StockFactory.AddTest(db, "Stock2", "isin2", db.Currencies.Single());
                db.SaveChanges();
            }
            _stockCacheService.Transactions.Count().ShouldBe(1);

            _stockCacheService.Reload();

            _stockCacheService.Stocks.Count.ShouldBe(2);
            _stockCacheService.GetStockOrThrow("isin1").Transactions.Sum(t => t.Quantity).ShouldBe(6);
        }

        [Fact]
        public void ReloadStock_OnlyReplacesThatStock()
        {
            using (var db = CreateDbContext())
            {
                StockFactory.AddTest(db);
                StockFactory.AddTest(db, "Stock2", "isin2", db.Currencies.Local.Single());
            }
            _stockCacheService.Reload();
            var stock2 = _stockCacheService.GetStockOrThrow("isin2");

            using (var db = CreateDbContext())
            {
                db.Stocks.Single(s => s.Isin == "isin1").Name = "Renamed";
                db.SaveChanges();
            }

            _stockCacheService.ReloadStock("isin1");

            _stockCacheService.Stocks.Select(s => s.Name).ShouldBe(new[] { "Renamed", "Stock2" });
            _stockCacheService.GetStockOrThrow("isin1").Name.ShouldBe("Renamed");
            _stockCacheService.GetStockOrThrow("isin2").ShouldBeSameAs(stock2);
        }

        [Fact]
        public void GetStockOrThrow_UnknownIsin_Throws()
        {
            using (CreateDbContext()) { }

            Should.Throw<Exception>(() => _stockCacheService.GetStockOrThrow("unknown"));
        }
    }
}
