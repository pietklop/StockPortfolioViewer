using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Core;
using DAL;
using Microsoft.EntityFrameworkCore;

namespace Services.DI
{
    public class StockDbInstaller : IWindsorInstaller
    {
        private readonly Settings settings;

        public StockDbInstaller(Settings settings)
        {
            this.settings = settings;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<StockDbContext>()
                //.LifestyleTransient()
                .ImplementedBy<StockDbContext>()
            .UsingFactoryMethod(() =>
                    new StockDbContext(new DbContextOptionsBuilder<StockDbContext>().UseSqlite(StockDbContext.Connection(settings.DbFileNamePath)).Options)));
        }
    }
}