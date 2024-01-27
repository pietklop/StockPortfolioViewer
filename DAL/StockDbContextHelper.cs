using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public static class StockDbContextHelper
    {
        public static StockDbContext CreateDbContext()
        {
            var settings = SettingsHelper.GetSettings();
            return new StockDbContext(Options());

            DbContextOptions<StockDbContext> Options() => new DbContextOptionsBuilder<StockDbContext>()
                .EnableSensitiveDataLogging()
                .UseSqlite(StockDbContext.Connection(settings.DbFileNamePath)).Options;
        }
    }
}