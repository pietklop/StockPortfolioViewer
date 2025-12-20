using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Core;
using DAL;
using DAL.Entities;
using Imports;
using log4net;
using log4net.Config;
using Messages.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Services;
using Services.DI;
using TestConsole.Infrastructure;

namespace TestConsole
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static async Task Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            XmlConfigurator.Configure(new FileInfo(@"log4net.config"));
            log.Info("Start");

            var menu = new Menu();

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            menu.Add("Setup", "Create initial DB", CreateInitialDb);
            menu.Add("Migrate", "Migrate", Migrate);
            menu.Add("Large", "Remove positions", RemovePositions);
            menu.Add("Split", "Apply stock-split", ApplyStockSplit);
            menu.Add("Test", "Test", Test);
            menu.Add("Exit", "Exit", () => cancellationTokenSource.Cancel());

            while (!cancellationTokenSource.Token.IsCancellationRequested)
                await menu.RunAsync();
        }

        /// <summary>
        /// EF uses this creating the migrations
        /// </summary>
        public class ClockDbContextFactory : IDesignTimeDbContextFactory<StockDbContext>
        {
            public StockDbContext CreateDbContext(string[] args)
            {
                var settings = SettingsHelper.GetSettings();

                var optionsBuilder = new DbContextOptionsBuilder<StockDbContext>();
                optionsBuilder.UseSqlite(StockDbContext.Connection(settings.DbFileNamePath));

                return new StockDbContext(optionsBuilder.Options);
            }
        }

        /// <summary>
        /// Apply ratio on all historic quantities and prices for given stock
        /// </summary>
        public static void ApplyStockSplit()
        {
            string symbol = "NVDA";
            double ratio = 10;
            string newIsin = "";

            using var db = StockDbContextHelper.CreateDbContext();
            var stock = db.Stocks
                .Include(s => s.Transactions).ThenInclude(t => t.StockValue)
                .Include(s => s.StockValues)
                .Include(s => s.LastKnownStockValue.StockValue)
                .Single(s => s.Symbol == symbol);

            if (newIsin.HasValue()) stock.Isin = newIsin;

            if (stock.AlarmUpperThreshold.HasValue)
                stock.AlarmUpperThreshold /= ratio;
            if (stock.AlarmLowerThreshold.HasValue)
                stock.AlarmLowerThreshold /= ratio;
            foreach (var sValue in stock.StockValues)
            {
                sValue.NativePrice /= ratio;
                sValue.UserPrice /= ratio;
            }

            foreach (var st in stock.Transactions)
                st.Quantity = Math.Round(st.Quantity * ratio);

            db.SaveChanges();
        }

        public static void RemovePositions()
        {
            float? removeLargerThan = ReadLine.ReadValueType<float?>("Remove positions larger than", 1000);
            if (removeLargerThan == null) return;
            log.Info($"Remove positions larger than: {removeLargerThan}");
            using (var db = StockDbContextHelper.CreateDbContext())
            {
                var stocks = db.Stocks
                    .Include(s => s.AreaShares)
                    .Include(s => s.LastKnownStockValue.StockValue)
                    .Include(s => s.SectorShares)
                    .Include(s => s.Transactions)
                    //.Where(s => s.Name == "MSFT")
                    .ToList();

                var largeStocks = new List<Stock>();
                foreach (var stock in stocks)
                {
                    if (Value(stock) > removeLargerThan)
                        largeStocks.Add(stock);
                }

                foreach (var stock in largeStocks)
                    stock.LastKnownStockValue = null;

                db.SaveChanges();

                foreach (var stock in largeStocks)
                    RemoveStock(stock, db);

                db.SaveChanges();
            }

            double Value(Stock stock)
            {
                if (stock.LastKnownStockValue == null) return 100_000;
                return stock.Transactions.Sum(t => t.Quantity) * stock.LastKnownUserPrice;
            }

            void RemoveStock(Stock stock, StockDbContext db)
            {
                log.Info($"Remove: {stock}");
                var divs = db.Dividends.Where(d => d.StockId == stock.Id).ToList();
                db.RemoveRange(divs);

                var trans = db.Transactions.Where(d => d.StockId == stock.Id).ToList();
                db.RemoveRange(trans);

                var pits = db.PitStockValues.Where(d => d.StockId == stock.Id).ToList();
                db.RemoveRange(pits);

                var comps = db.DataRetrievers.SelectMany(d => d.StockRetrieverCompatibilities).Where(d => d.StockId == stock.Id).ToList();
                db.RemoveRange(comps);

                db.RemoveRange(stock.AreaShares);
                db.RemoveRange(stock.SectorShares);

                db.Stocks.Remove(stock);
            }
        }

        public static void Test()
        {
            var container = CastleContainer.Instance;
            var installer = DependencyInstaller.CreateInstaller();
            container.AddFacilities().Install(installer);

            var tranList = new List<TransactionDto>();

            //tranList.AddRange(AddStockBuySell("Aurora", "CAD", new DateTime(2019,2, 12), new DateTime(2020, 6, 1),150,10.1,1.77, 1.51, 1.45));
            // tranList.AddRange(AddStockBuySell("Microsoft", "USD", new DateTime(2019,4, 9), new DateTime(2020, 2, 1),18,120,170, 1.11, 1.15));
            // tranList.AddRange(AddStockBuySell("Pepsico", "USD", new DateTime(2019,4, 9), new DateTime(2020, 2, 1),18,122,146.50, 1.11, 1.15));
            // tranList.AddRange(AddStockBuySell("Nissan", "EUR", new DateTime(2019,4, 9), new DateTime(2020, 2, 1),130,7.53,3.84, 1, 1));
            // tranList.AddRange(AddStockBuySell("Volker Wessels", "EUR", new DateTime(2019,4, 9), new DateTime(2020, 2, 1),53,18.50,22.13, 1, 1));
            //
            // tranList.AddRange(AddStockBuySell("AMD", "USD", new DateTime(2019,7, 23), new DateTime(2020, 4, 1),33,32.80,45.21, 1.11, 1.11));
            // tranList.AddRange(AddStockBuySell("Nvidia", "USD", new DateTime(2019,7, 23), new DateTime(2020, 4, 1),24,42.50,63.41, 1.11, 1.11));
            //
            // tranList.AddRange(AddStockBuySell("Think Global Real Estate", "EUR", new DateTime(2017,4, 13), new DateTime(2017, 10, 24),125,40.60,70.12, 1, 1));
            //
            // tranList.AddRange(AddStockBuySell("Arcadis", "EUR", new DateTime(2017,3,29), new DateTime(2017, 4,27), 140, 14.21,16.31, 1, 1));
            // tranList.AddRange(AddStockBuySell("KPN", "EUR", new DateTime(2017,5,4), new DateTime(2017, 5,24),760,2.66,2.99,1,1));
            //
            // tranList.AddRange(AddStockBuySell("", "EUR", new DateTime(2017,4, 13), new DateTime(2017, 10, 24),));


            var importProcessor = CastleContainer.Resolve<ImportProcessor>();
            importProcessor.Process(new TransactionImportDto(tranList, new List<DividendDto>
            {
                new DividendDto
                {
                    Isin = $"Think Global Real Estate_2017",
                    TimeStamp = new DateTime(2017,9,17),
                    Currency = "EUR",
                    Value = 72.12,
                    CurrencyRatio = 1,
                    Tax = 10,
                    Costs = 1,
                }
            }));
        }

        private static List<TransactionDto> AddStockBuySell(string name, string currency, DateTime dateBuy, DateTime dateSell, int quantity, double priceBuy, double priceSell, double currRatioBuy, double currRatioSell)
        {
            var isin = $"{name}_{dateBuy.Year}";
            return new List<TransactionDto>
            {
                new TransactionDto
                {
                    Name = name,
                    Isin = isin,
                    Currency = currency,
                    TimeStamp = dateBuy,
                    Quantity = quantity,
                    Price = priceBuy,
                    Costs = 15,
                    CurrencyRatio = currRatioBuy,
                    Guid = Guid.NewGuid().ToString(),
                },
                new TransactionDto
                {
                    Name = name,
                    Isin = isin,
                    Currency = currency,
                    TimeStamp = dateSell,
                    Quantity = -quantity,
                    Price = priceSell,
                    Costs = 15,
                    CurrencyRatio = currRatioSell,
                    Guid = Guid.NewGuid().ToString(),
                },
            };
        }

        public static void Migrate()
        {
            using (var db = StockDbContextHelper.CreateDbContext())
            {
                log.Info($"Migrate...");
                db.Database.Migrate();

                db.SaveChanges();
            }
        }

        public static void CreateInitialDb()
        {
            using (var db = StockDbContextHelper.CreateDbContext())
            {
                log.Info($"Migrate and setup");
                db.Database.Migrate();

                if (db.Areas.Any())
                    throw new Exception($"Seems that de db already contains data!");

                AreaSetup.AddContinents(db);
                CurrencySetup.AddCurrencies(db);
                SectorSetup.AddSectors(db);

                db.SaveChanges();
            }
        }
    }
}



