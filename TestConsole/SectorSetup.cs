using Core;
using DAL;
using DAL.Entities;

namespace TestConsole
{
    public static class SectorSetup
    {
        public static void AddSectors(StockDbContext db)
        {
            AddSector(db, "Cons goods luxury");
            AddSector(db, "Cons goods staples");
            AddSector(db, "Energy");
            AddSector(db, "Finance");
            AddSector(db, "Healthcare");
            AddSector(db, "Industry");
            AddSector(db, "Information tech");
            AddSector(db, "Comm tech");
            AddSector(db, "Utilities");
            AddSector(db, "Real estate");
            AddSector(db, "Materials");
            AddSector(db, Constants.Unknown);

            db.SaveChanges();
        }

        private static void AddSector(StockDbContext db, string name)
        {
            db.Sectors.Add(new Sector { Name = name });
        }
    }
}