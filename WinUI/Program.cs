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
        private static double eurInUsd;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            XmlConfigurator.Configure(new FileInfo(@"log4net.config"));

            var container = CastleContainer.Instance;
            var installer = DependencyInstaller.CreateInstaller(new FormInstaller());
            container.AddFacilities().Install(installer);

            //DoStartupActions();

            // load all stock data in memory once, so the views do not need to query the database
            CastleContainer.Resolve<StockCacheService>().Reload();

            var mainForm = CastleContainer.Instance.Resolve<frmMain>();
            Application.Run(mainForm);
        }
    }
}
