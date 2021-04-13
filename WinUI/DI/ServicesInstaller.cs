using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Imports.DeGiro;
using Services;
using Services.Ui;

namespace Dashboard.DI
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<frmMain>().LifestyleSingleton());
            container.Register(Component.For<frmOverview>().LifestyleTransient());
            container.Register(Component.For<frmStockDetail>().LifestyleTransient());

            container.Register(Component.For<Importer>().LifestyleTransient());
            container.Register(Component.For<ImportProcessor>().LifestyleTransient());
            container.Register(Component.For<StockDetailService>().LifestyleTransient());
            container.Register(Component.For<StockService>().LifestyleTransient());
            container.Register(Component.For<StockOverviewService>().LifestyleTransient());
        }
    }
}