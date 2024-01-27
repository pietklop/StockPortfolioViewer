using DAL;
using DAL.Entities;

namespace Services.Tests.Factories
{
    public static class AreaFactory
    {
        public static Area AddTest(StockDbContext db, string name = null, bool isContinent = false)
        {
            return db.Areas.Add(new Area
            {
                Name = name ?? "Area",
                IsContinent = isContinent,
            }).Entity;
        }
    }
}