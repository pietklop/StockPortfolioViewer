using DAL;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace Services.Tests.Infra
{
    [SuppressMessage("ReSharper", "ConsiderUsingConfigureAwait")]
    public abstract class DbTestBase
    {
        public DbContextOptions<StockDbContext> Options = new DbContextOptionsBuilder<StockDbContext>()
            .EnableSensitiveDataLogging()
            .UseSqlite(CreateInMemoryDatabase())
            .Options;

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();

            return connection;
        }

        protected StockDbContext CreateDbContext()
        {
            var dbContext = new StockDbContext(Options);
            dbContext.Database.OpenConnection();
            dbContext.Database.EnsureCreated();

            return dbContext;
        }
    }
}