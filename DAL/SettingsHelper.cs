using System.IO;
using Core;
using Microsoft.Extensions.Configuration;

namespace DAL
{
    public static class SettingsHelper
    {
        public static Settings GetSettings()
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var settings = new Settings();
            config.GetSection(nameof(Settings)).Bind(settings);

            return settings;
        }

    }
}
