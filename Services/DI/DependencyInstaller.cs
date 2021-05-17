using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using DAL;

namespace Services.DI
{
    public static class DependencyInstaller
    {
        public static IWindsorInstaller CreateInstaller(params IWindsorInstaller[] externalInstallers)
        {
            var compositeInstaller = new CompositeInstaller();

            foreach (var externalInstaller in externalInstallers)
                compositeInstaller.Add(externalInstaller);

            compositeInstaller.Add(new StockDbInstaller());
            compositeInstaller.Add(new ServicesInstaller(SettingsHelper.GetSettings()));

            return compositeInstaller;
        }

        public static IWindsorContainer AddFacilities(this IWindsorContainer container)
        {
            return container.AddFacility<TypedFactoryFacility>()
                .AddFacility<LogFacility>();
        }
    }
}