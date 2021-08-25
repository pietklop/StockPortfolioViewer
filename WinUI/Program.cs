using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Core;
using Dashboard.DI;
using log4net;
using log4net.Config;
using Services;
using Services.DataCollection;
using Services.DI;

namespace Dashboard
{
    static class Program
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

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

            var settings = CastleContainer.Resolve<Settings>();
            if (settings.RetrieveStockValuesAtStartup)
                DoStartupActions();

            var mainForm = CastleContainer.Resolve<frmMain>();
            Application.Run(mainForm);
        }

        private static void DoStartupActions()
        {
            try
            {
                var drMan = CastleContainer.Resolve<DataRetrieverManager>();
                drMan.TryUpdateCurrencies();
                drMan.TryUpdateStocks();
            }
            catch (Exception ex)
            {
                log.Error($"Error during {nameof(DoStartupActions)}", ex);
            }
        }
    }
}
