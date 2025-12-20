using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core;
using Imports;
using Services.Ui;

namespace Services.DI
{
    public class ServicesInstaller : IWindsorInstaller
    {
        private readonly Settings settings;

        public ServicesInstaller(Settings settings)
        {
            this.settings = settings;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<Settings>().Instance(settings));

            container.Register(Component.For<DividendOverviewService>().LifestyleTransient());
            container.Register(Component.For<Importer>().LifestyleTransient());
            container.Register(Component.For<ImportProcessor>().LifestyleTransient());
            container.Register(Component.For<PortfolioDistributionService>().LifestyleTransient());
            container.Register(Component.For<StockDetailService>().LifestyleTransient());
            container.Register(Component.For<StockHistoryOverviewService>().LifestyleTransient());
            container.Register(Component.For<StockPerformanceService>().LifestyleTransient());
            container.Register(Component.For<StockPerformanceOverviewService>().LifestyleTransient());
            container.Register(Component.For<StockService>().LifestyleTransient());
            container.Register(Component.For<StockOverviewService>().LifestyleTransient());
            container.Register(Component.For<StockRetrieverService>().LifestyleTransient());
            container.Register(Component.For<TransactionOverviewService>().LifestyleTransient());
        }
    }
}