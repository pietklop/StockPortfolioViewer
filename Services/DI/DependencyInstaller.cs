using System.IO;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Core;
using Microsoft.Extensions.Configuration;

namespace Services.DI
{
    public static class DependencyInstaller
    {
        public static IWindsorInstaller CreateInstaller(params IWindsorInstaller[] externalInstallers)
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var settings = new Settings();
            config.GetSection(nameof(Settings)).Bind(settings);

            var compositeInstaller = new CompositeInstaller();

            foreach (var externalInstaller in externalInstallers)
                compositeInstaller.Add(externalInstaller);

            compositeInstaller.Add(new StockDbInstaller());
            compositeInstaller.Add(new ServicesInstaller(settings));

            return compositeInstaller;
        }

        public static IWindsorContainer AddFacilities(this IWindsorContainer container)
        {
            return container.AddFacility<TypedFactoryFacility>()
                .AddFacility<LogFacility>();
        }

    }
}