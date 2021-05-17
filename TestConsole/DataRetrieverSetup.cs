using System;
using System.Collections.Generic;
using Core;
using DAL;
using DAL.Entities;
using StockDataApi.AlphaVantage;
using StockDataApi.IexCloud;
using StockDataApi.MarketStack;
using StockDataApi.TwelveData;

namespace TestConsole
{
    public static class DataRetrieverSetup
    {
        public static void AddRetrievers(StockDbContext db)
        {
            AddRetriever(db, AvDataRetriever.ConstName, 10, typeof(AvDataRetriever), "https://www.alphavantage.co/", "???", "World")
                .AddAvLimitations()
                .Description = @"You can use this site to search for symbols: https://www.buyupside.com/alphavantagelive/searchforsymboluser.php" + "\r\nEuropean stocks should be suffixed with the exchange like for example BMW.FRK or AIR.PAR";
            AddRetriever(db, IexDataRetriever.ConstName, 5, typeof(IexDataRetriever), "https://cloud.iexapis.com/v1/", "???", "Usa")
                .AddIexLimitations()
                .Description = "Preferred data-provider because of the high limit and realtime data. Covers usa only";
            AddRetriever(db, MsDataRetriever.ConstName, 15, typeof(MsDataRetriever), "http://api.marketstack.com/v1/", "???", "World")
                .AddMsLimitations()
                .Description = @"You can search for symbols on the site: https://marketstack.com/search";
            AddRetriever(db, TdDataRetriever.ConstName, 7, typeof(TdDataRetriever), "https://api.twelvedata.com/", "???", "World")
                .AddTdLimitations();
        }

        private static DataRetriever AddRetriever(StockDbContext db, string name, int priority, Type type, string baseUrl, string key, string area)
        {
            return db.DataRetrievers.Add(new DataRetriever
            {
                Name = name,
                Type = type.FullName,
                BaseUrl = baseUrl,
                Key = key,
                Priority = priority,
                SupportedArea = area,
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

        internal static DataRetriever AddTdLimitations(this DataRetriever retriever)
        {
            retriever.AddLimitation(RetrieverLimitTimespanType.Day, 800);
            retriever.AddLimitation(RetrieverLimitTimespanType.Minute, 12);

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