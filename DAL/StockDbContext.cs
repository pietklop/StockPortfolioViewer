using DAL.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace DAL
{
    // Add-Migration Migration_Name -p DAL -s TestConsole
    // update-database -p DAL -s TestConsole
    public class StockDbContext : DbContext
    {
        public DbSet<Area> Areas { get; set; }
        public DbSet<AreaShare> AreaShares { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Dividend> Dividends { get; set; }
        public DbSet<PitStockValue> PitStockValues { get; set; }
        public DbSet<DataRetriever> DataRetrievers { get; set; }
        public DbSet<RetrieverLimitation> RetrieverLimitations { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<SectorShare> SectorShares { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public StockDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Currency>()
                .HasKey(ur => new { ur.Key });

            modelBuilder.Entity<StockRetrieverCompatibility>()
                .HasKey(ur => new { ur.StockId, ur.DataRetrieverId });
        }

        public static DbConnection Connection(string dbFileNamePath)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = dbFileNamePath };
            var connectionString = connectionStringBuilder.ToString();
            return new SqliteConnection(connectionString);
        }
    }
}