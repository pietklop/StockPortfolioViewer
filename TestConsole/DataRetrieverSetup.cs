using System;
using DAL;
using DAL.Entities;
using StockDataApi.AlphaVantage;
using StockDataApi.IexCloud;

namespace TestConsole
{
    public static class DataRetrieverSetup
    {
        public static void AddRetrievers(StockDbContext db)
        {
            AddRetriever(db, AvDataRetriever.Name, typeof(AvDataRetriever), "https://www.alphavantage.co/", "???");
            AddRetriever(db, IexDataRetriever.Name, typeof(IexDataRetriever), "https://cloud.iexapis.com/v1/", "???");
        }

        private static void AddRetriever(StockDbContext db, string name, Type type, string baseUrl, string key)
        {
            db.DataRetrievers.Add(new DataRetriever
            {
                Name = name,
                Type = type.FullName,
                BaseUrl = baseUrl,
                Key = key,
            });
        }
    }
}