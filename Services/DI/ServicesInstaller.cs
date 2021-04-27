using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core;
using Imports.DeGiro;
using Services.DataCollection;
using Services.Ui;
using StockDataApi.AlphaVantage;
using StockDataApi.IexCloud;
using StockDataApi.MarketStack;

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

            RegisterDataRetrievers(container);

            container.Register(Component.For<CurrencyUpdater>().LifestyleTransient());
            container.Register(Component.For<DataRetrieverManager>().LifestyleTransient());
            container.Register(Component.For<DataRetrieverService>().LifestyleTransient());
            container.Register(Component.For<Importer>().LifestyleTransient());
            container.Register(Component.For<ImportProcessor>().LifestyleTransient());
            container.Register(Component.For<StockDetailService>().LifestyleTransient());
            container.Register(Component.For<StockService>().LifestyleTransient());
            container.Register(Component.For<StockOverviewService>().LifestyleTransient());
            container.Register(Component.For<StockRetrieverService>().LifestyleTransient());
            container.Register(Component.For<TransactionOverviewService>().LifestyleTransient());
        }

        private static void RegisterDataRetrievers(IWindsorContainer container)
        {
            container.Register(Component.For<AvDataRetriever>().LifestyleTransient());
            container.Register(Component.For<IexDataRetriever>().LifestyleTransient());
            container.Register(Component.For<MsDataRetriever>().LifestyleTransient());
        }
    }
}