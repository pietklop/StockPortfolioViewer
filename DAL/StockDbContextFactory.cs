using Microsoft.EntityFrameworkCore;

namespace DAL
{
    /// <summary>
    /// Creates short-lived <see cref="StockDbContext"/> instances, independent of the shared (singleton) context
    /// </summary>
    public class StockDbContextFactory : IDbContextFactory<StockDbContext>
    {
        private readonly DbContextOptions<StockDbContext> options;

        public StockDbContextFactory(DbContextOptions<StockDbContext> options)
        {
            this.options = options;
        }

        public StockDbContext CreateDbContext() => new StockDbContext(options);
    }
}
