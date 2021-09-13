using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DAL;

namespace Services.DI
{
    public class StockDbInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<StockDbContext>()
                .LifestyleTransient()
                .ImplementedBy<StockDbContext>()
                .UsingFactoryMethod(() =>
                    new StockDbContext()));
        }
    }
}