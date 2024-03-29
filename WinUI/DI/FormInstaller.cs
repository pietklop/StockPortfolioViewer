﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Dashboard.Input;

namespace Dashboard.DI
{
    public class FormInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<frmMain>().LifestyleSingleton());

            container.Register(Component.For<frmDataRetriever>().LifestyleTransient());
            container.Register(Component.For<frmDataRetrievers>().LifestyleTransient());
            container.Register(Component.For<frmDividends>().LifestyleTransient());
            container.Register(Component.For<frmOverview>().LifestyleTransient());
            container.Register(Component.For<frmStockDetail>().LifestyleTransient());
            container.Register(Component.For<frmStockHistoryOverview>().LifestyleTransient());
            container.Register(Component.For<frmStockPerformance>().LifestyleTransient());
            container.Register(Component.For<frmStockRetrievers>().LifestyleTransient());
            container.Register(Component.For<frmStockSelection>().LifestyleTransient());
            container.Register(Component.For<frmTransactions>().LifestyleTransient());
        }
    }
}