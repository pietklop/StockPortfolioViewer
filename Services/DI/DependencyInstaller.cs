using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.EntityFrameworkCore;

namespace Services.DI
{
    public class DependencyInstaller : IWindsorInstaller
    {
        private readonly DbContextOptions dbContextOptions;

        public DependencyInstaller(DbContextOptions dbContextOptions)
        {
            this.dbContextOptions = dbContextOptions;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {

        }
    }
}