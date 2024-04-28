using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL.Entities;
using FakeItEasy;
using log4net;
using Services.Tests.Factories;
using Services.Tests.Infra;
using Services.Ui;
using Shouldly;
using Xunit;

namespace Services.Tests.Ui
{
    public class StockPerformanceOverviewServiceTests : DbTestBase
    {
        [Fact]
        public void Test_one_stock_one_year_with_dividends()
        {
            var fromDate = new DateTime(2020, 1, 1);
            var toDate = new DateTime(2020, 12, 31);
            var initialPrice = 10.0;
            var finalPrice = 11.0;
            var nStocks = 100;
            var divPerStock1 = 0.2;
            var divPerStock2 = 0.3;

            // Arrange
            using (var db = CreateDbContext())
            {
                var stock = StockFactory.AddTest(db);

                stock.AddBuy(fromDate.AddDays(-1), nStocks, initialPrice);
                stock.AddValue(finalPrice, toDate.AddDays(-2), updateLastKnownValue: true);
                stock.AddDividend(fromDate.AddMonths(3), divPerStock1 * nStocks);
                stock.AddDividend(fromDate.AddMonths(9), divPerStock2 * nStocks);

                db.SaveChanges();
            }

            using (var db = CreateDbContext())
            {
                // Act
                var service = new StockPerformanceOverviewService(A.Fake<ILog>(), db, toDate);
                var stocks = service.GetStockList(PerformanceInterval.Year);

                // Assert
                var stock = stocks.Last();
                stock.Value.ShouldBe(finalPrice * nStocks);
                stock.PerformanceFractionT0.ShouldBe((finalPrice + divPerStock1 + divPerStock2) / initialPrice -1, 0.00001);
                stock.PerformanceFractionTMin1.ShouldBe(0);
            }
        }

        // Q1 2% (8% annually) Q2-4 3% (4% annually)
        [Fact]
        public void Test_one_stock_one_year_2buys()
        {
            var fromDate = new DateTime(2020, 1, 1);
            var toDate = new DateTime(2020, 12, 31);
            var initialPrice = 100.0;
            var secondBuyPrice = 102.0;
            var finalPrice = 102.0 * 1.03;
            var nStocks = 100;

            // Arrange
            using (var db = CreateDbContext())
            {
                var stock = StockFactory.AddTest(db);

                stock.AddBuy(fromDate.AddDays(-1), nStocks, initialPrice);
                stock.AddBuy(fromDate.AddMonths(3), nStocks, secondBuyPrice);
                stock.AddValue(finalPrice, toDate.AddDays(-2), updateLastKnownValue: true);

                db.SaveChanges();
            }

            using (var db = CreateDbContext())
            {
                // Act
                var service = new StockPerformanceOverviewService(A.Fake<ILog>(), db, toDate);
                var stocks = service.GetStockList(PerformanceInterval.Year);

                // Assert
                var stock = stocks.Last();
                stock.Value.ShouldBe(finalPrice * nStocks*2);
                stock.PerformanceFractionT0.ShouldBe((8.0 * 1 + 4.0 * 7)/(8 * 100), 0.001);
                stock.PerformanceFractionTMin1.ShouldBe(0);
            }
        }

        // Q1-2 2% (4% annually) Q3-4 -3% (-6% annually)
        [Fact]
        public void Test_one_stock_one_year_sell()
        {
            var fromDate = new DateTime(2020, 1, 1);
            var toDate = new DateTime(2020, 12, 31);
            var initialPrice = 100.0;
            var sellPrice = 102.0;
            var finalPrice = sellPrice * 0.97;
            var nStocks = 90;
            var nStocksSold = 30;

            // Arrange
            using (var db = CreateDbContext())
            {
                var stock = StockFactory.AddTest(db);

                stock.AddBuy(fromDate.AddDays(-1), nStocks, initialPrice);
                stock.AddSell(fromDate.AddMonths(6), nStocksSold, sellPrice);
                stock.AddValue(finalPrice, toDate.AddDays(-2), updateLastKnownValue: true);

                db.SaveChanges();
            }

            using (var db = CreateDbContext())
            {
                // Act
                var service = new StockPerformanceOverviewService(A.Fake<ILog>(), db, toDate);
                var stocks = service.GetStockList(PerformanceInterval.Year);

                // Assert
                var stock = stocks.Last();
                stock.Value.ShouldBe(finalPrice * (nStocks - nStocksSold));
                stock.PerformanceFractionT0.ShouldBe((4.0 * 3 + -6.0 * 2) / (5 * 100), 0.001);
                stock.PerformanceFractionTMin1.ShouldBe(0);
            }
        }

        // Q1-2 -1% (-2% annually) Q3 1% (4% annually)
        [Fact]
        public void Test_one_stock_one_year_sell_all()
        {
            var fromDate = new DateTime(2020, 1, 1);
            var toDate = new DateTime(2020, 12, 31);
            var initialPrice = 100.0;
            var sellPrice = 99;
            var finalPrice = sellPrice * 1.01;
            var nStocks = 90;

            // Arrange
            using (var db = CreateDbContext())
            {
                var stock = StockFactory.AddTest(db);

                stock.AddBuy(fromDate.AddDays(-1), nStocks, initialPrice);
                stock.AddSell(fromDate.AddMonths(6), 30, sellPrice);
                stock.AddSell(fromDate.AddMonths(9), 60, finalPrice);

                db.SaveChanges();
            }

            using (var db = CreateDbContext())
            {
                // Act
                var service = new StockPerformanceOverviewService(A.Fake<ILog>(), db, toDate);
                var stocks = service.GetStockList(PerformanceInterval.Year);

                // Assert
                var stock = stocks.Last();
                stock.Value.ShouldBe(0);
                // calculation below is not exact (using excel it was a perfect match [-0.375%])
                stock.PerformanceFractionT0.ShouldBe((-2.0 * 99 * 90 + 1.0 * 100 * 60)/((2*99*90+100*60)*100), 0.0013);
                stock.PerformanceFractionTMin1.ShouldBe(0);
            }
        }

        // Stock is bought during the period
        [Fact]
        public void Test_one_stock_buy_Q2()
        {
            var fromDate = new DateTime(2020, 1, 1);
            var toDate = new DateTime(2020, 12, 31);
            var initialPrice = 100.0;
            var finalPrice = initialPrice * 1.10;
            var nStocks = 90;

            // Arrange
            using (var db = CreateDbContext())
            {
                var stock = StockFactory.AddTest(db);

                stock.AddBuy(fromDate.AddMonths(6), nStocks, initialPrice);
                stock.AddValue(finalPrice, toDate.AddDays(-2), updateLastKnownValue: true);

                db.SaveChanges();
            }

            using (var db = CreateDbContext())
            {
                // Act
                var service = new StockPerformanceOverviewService(A.Fake<ILog>(), db, toDate);
                var stocks = service.GetStockList(PerformanceInterval.Year);

                // Assert
                var stock = stocks.Last();
                stock.Value.ShouldBe(nStocks * finalPrice, 0.01);
                stock.PerformanceFractionT0.ShouldBe(0.10, 0.001);
                stock.PerformanceFractionTMin1.ShouldBe(0);
            }
        }
    }
}
