﻿using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Dashboard.DI
{
    public class FormInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<frmDataRetriever>().LifestyleSingleton());
            container.Register(Component.For<frmDataRetrievers>().LifestyleSingleton());
            container.Register(Component.For<frmDividends>().LifestyleTransient());
            container.Register(Component.For<frmMain>().LifestyleSingleton());
            container.Register(Component.For<frmOverview>().LifestyleTransient());
            container.Register(Component.For<frmStockDetail>().LifestyleTransient());
            container.Register(Component.For<frmStockRetrievers>().LifestyleTransient());
            container.Register(Component.For<frmTransactions>().LifestyleTransient());
        }
    }
}