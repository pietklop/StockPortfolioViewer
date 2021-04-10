using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace Dashboard.DI
{
    public static class DependencyInstaller
    {
        public static IWindsorInstaller CreateInstaller()
        {
            var compositeInstaller = new CompositeInstaller();

            compositeInstaller.Add(new StockDbInstaller());
            compositeInstaller.Add(new ServicesInstaller());

            return compositeInstaller;
        }

        public static IWindsorContainer AddFacilities(this IWindsorContainer container)
        {
            return container.AddFacility<TypedFactoryFacility>()
                .AddFacility<LogFacility>();
        }

    }
}