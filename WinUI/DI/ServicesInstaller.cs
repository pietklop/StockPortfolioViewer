using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Services.Ui;

namespace Dashboard.DI
{
    public class ServicesInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<frmMain>().LifestyleSingleton());
            container.Register(Component.For<frmOverview>().LifestyleTransient());

            container.Register(Component.For<StockOverviewService>().LifestyleTransient());
        }
    }
}