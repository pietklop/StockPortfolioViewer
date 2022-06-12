using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Castle.MicroKernel;
using Core;
using Dashboard.DI;
using log4net;
using log4net.Config;
using Services.DataCollection;
using Services.DI;

namespace Dashboard
{
    static class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static double eurInUsd;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            XmlConfigurator.Configure(new FileInfo(@"log4net.config"));

            var container = CastleContainer.Instance;
            var installer = DependencyInstaller.CreateInstaller(new FormInstaller());
            container.AddFacilities().Install(installer);

            DoStartupActions();

            var mainForm = CastleContainer.Instance.Resolve<frmMain>(new Arguments { { "eurInUsd", eurInUsd } }); 
            Application.Run(mainForm);
        }

        private static void DoStartupActions()
        {
            try
            {
                var drMan = CastleContainer.Resolve<DataRetrieverManager>();
                drMan.TryUpdateCurrencies();
                eurInUsd = drMan.EuroPrice();
                var settings = CastleContainer.Resolve<Settings>();
                if (settings.RetrieveStockValuesAtStartup)
                    drMan.TryUpdateStocks();
            }
            catch (Exception ex)
            {
                log.Error($"Error during {nameof(DoStartupActions)}", ex);
            }
        }
    }
}
