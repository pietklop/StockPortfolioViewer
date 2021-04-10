using System;
using System.IO;
using System.Reflection;
using DAL;
using Imports.DeGiro;
using log4net;
using log4net.Config;
using Microsoft.EntityFrameworkCore;
using Services;

namespace TestConsole
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            XmlConfigurator.Configure(new FileInfo(@"log4net.config"));
            log.Info("Start");

            var importer = new Importer();
            var import = importer.Import(@"c:\Users\pieterk\Dropbox\Aandelen\Account (2).csv");

            //return;

            using (var db = new StockDbContext())
            {
                db.Database.Migrate();

                var stockService = new StockService(log, db);
                var importProcessor = new ImportProcessor(log, db, stockService);
                importProcessor.Process(import);

                //Setup(db);

                db.SaveChanges();
            }

            Console.WriteLine("Done");
            Console.ReadKey();
        }

        public static void Setup(StockDbContext db)
        {
            AreaSetup.AddContinents(db);
            CurrencySetup.AddCurrencies(db);
            SectorSetup.AddSectors(db);
        }


        //        private static IWindsorContainer BuildContainer()
        //        {
        //            var container = new WindsorContainer();
        //            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
        //
        //            return container.Install(new DependencyInstaller(new DbContextOptionsBuilder()
        //                    .UseSqlite("Filename=StockDatabase.db").Options));
        //        }

    }
}
