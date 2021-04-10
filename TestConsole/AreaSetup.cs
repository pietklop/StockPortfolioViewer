using Core;
using DAL;
using DAL.Entities;

namespace TestConsole
{
    public static class AreaSetup
    {
        public static void AddContinents(StockDbContext db)
        {
            AddContinent(db, "Europe");
            AddContinent(db, "N-America");
            AddContinent(db, "S-America");
            AddContinent(db, "Africa");
            AddContinent(db, "Asia");
            AddContinent(db, "Oceania");
            AddContinent(db, Constants.Unknown);

            db.SaveChanges();
        }

        private static void AddContinent(StockDbContext dbContext, string name)
        {
            dbContext.Areas.Add(new Area {Name = name, IsContinent = true});
        }
    }
}