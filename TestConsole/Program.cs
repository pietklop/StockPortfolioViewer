using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Castle.MicroKernel;
using DAL;
using DAL.Entities;
using Imports.DeGiro;
using log4net;
using log4net.Config;
using Messages.StockDataApi;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Services;
using Services.DI;
using Services.Helpers;
using StockDataApi.AlphaVantage;
using StockDataApi.General;
using StockDataApi.IexCloud;
using StockDataApi.MarketStack;
using StockDataApi.TwelveData;
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
        /// Apply ratio on all historic quantities and prices for given stock
        /// </summary>
        public static void ApplyStockSplit()
        {
            string symbol = "GOOGL";
            double ratio = 20;
            
            using var db = new StockDbContext();
            var stock = db.Stocks
                .Include(s => s.Transactions).ThenInclude(t => t.StockValue)
                .Include(s => s.StockValues)
                .Include(s => s.LastKnownStockValue.StockValue)
                .Single(s => s.Symbol == symbol);

            foreach (var sValue in stock.StockValues)
            {
                sValue.NativePrice /= ratio;
                sValue.UserPrice /= ratio;
            }

            foreach (var st in stock.Transactions)
                st.Quantity *= ratio;

            db.SaveChanges();
        }

        public static void RemovePositions()
        {
            float? removeLargerThan = ReadLine.ReadValueType<float?>("Remove positions larger than", 1000);
            if (removeLargerThan == null) return;
            log.Info($"Remove positions larger than: {removeLargerThan}");
            using (var db = new StockDbContext())
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
            using (var db = new StockDbContext())
            {
                //            var dr = CastleContainer.Resolve<IexDataRetriever>();

//                var pp = db.PitStockValues.ToList();
//                var pitGroups = pp.GroupBy(p => p.StockId).ToList();
//                foreach (var pits in pitGroups)
//                {
//                    var dd = DateTime.Now.AddDays(2).Date;
//                    foreach (var p in pits.OrderByDescending(p => p.TimeStamp))
//                    {
//                        if (p.TimeStamp.Date == dd)
//                            db.Remove(p);
//                            //log.Info($"Remove Pit: {p.Id}");
//                        else
//                            dd = p.TimeStamp.Date;
//                    }
//                }

                db.SaveChanges();

            }
        }

        public static void Migrate()
        {
            using (var db = new StockDbContext())
            {
                log.Info($"Migrate...");
                db.Database.Migrate();

                db.SaveChanges();
            }
        }

        public static void CreateInitialDb()
        {
            using (var db = new StockDbContext())
            {
                log.Info($"Migrate and setup");
                db.Database.Migrate();

                if (db.Areas.Any())
                    throw new Exception($"Seems that de db already contains data!");

                DataRetrieverSetup.AddRetrievers(db);
                AreaSetup.AddContinents(db);
                CurrencySetup.AddCurrencies(db);
                SectorSetup.AddSectors(db);

                db.SaveChanges();
            }

        }

    }
}



