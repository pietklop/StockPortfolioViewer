using System;
using System.Collections.Generic;
using DAL;
using DAL.Entities;
using StockDataApi.AlphaVantage;
using StockDataApi.IexCloud;
using StockDataApi.MarketStack;

namespace TestConsole
{
    public static class DataRetrieverSetup
    {
        public static void AddRetrievers(StockDbContext db)
        {
            AddRetriever(db, AvDataRetriever.ConstName, 10, typeof(AvDataRetriever), "https://www.alphavantage.co/", "???")
                .AddAvLimitations();
            AddRetriever(db, IexDataRetriever.ConstName, 5, typeof(IexDataRetriever), "https://cloud.iexapis.com/v1/", "???")
                .AddIexLimitations();
            AddRetriever(db, MsDataRetriever.ConstName, 15, typeof(MsDataRetriever), "https://api.marketstack.com/v1/", "???")
                .AddMsLimitations();
        }

        private static DataRetriever AddRetriever(StockDbContext db, string name, int priority, Type type, string baseUrl, string key)
        {
            return db.DataRetrievers.Add(new DataRetriever
            {
                Name = name,
                Type = type.FullName,
                BaseUrl = baseUrl,
                Key = key,
                Priority = priority
            }).Entity;
        }

        internal static DataRetriever AddIexLimitations(this DataRetriever retriever)
        {
            retriever.AddLimitation(RetrieverLimitTimespanType.Month, 50_000);

            return retriever;
        }

        internal static DataRetriever AddAvLimitations(this DataRetriever retriever)
        {
            retriever.AddLimitation(RetrieverLimitTimespanType.Minute, 5);
            retriever.AddLimitation(RetrieverLimitTimespanType.Day, 500);

            return retriever;
        }

        internal static DataRetriever AddMsLimitations(this DataRetriever retriever)
        {
            retriever.AddLimitation(RetrieverLimitTimespanType.Month, 1_000);
            retriever.AddLimitation(RetrieverLimitTimespanType.Second, 5);

            return retriever;
        }

        internal static RetrieverLimitation AddLimitation(this DataRetriever retriever, RetrieverLimitTimespanType type, int limit)
        {
            var limitation = new RetrieverLimitation
            {
                TimespanType = type,
                Limit = limit,
            };

            retriever.RetrieverLimitations ??= new List<RetrieverLimitation>();
            retriever.RetrieverLimitations.Add(limitation);

            return limitation;
        }
    }
}