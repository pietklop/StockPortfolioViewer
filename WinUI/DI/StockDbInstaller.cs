using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DAL;

namespace Dashboard.DI
{
    public class StockDbInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<StockDbContext>()
                .ImplementedBy<StockDbContext>()
                .UsingFactoryMethod(() =>
                    new StockDbContext()));
        }
    }
}