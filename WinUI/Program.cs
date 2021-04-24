using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Dashboard.DI;
using log4net;
using log4net.Config;
using Services;
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

            DoStartupActions();

            var mainForm = CastleContainer.Resolve<frmMain>();
            Application.Run(mainForm);
        }

        private static void DoStartupActions()
        {
            try
            {
                var curUpdater = CastleContainer.Resolve<CurrencyUpdater>();
                curUpdater.Run();

                var stockUpdater = CastleContainer.Resolve<StockValueUpdater>();
                stockUpdater.Run();
            }
            catch (Exception ex)
            {
                log.Error($"Error during {nameof(DoStartupActions)}", ex);
            }
        }
    }
}
