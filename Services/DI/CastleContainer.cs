using System;
using System.Linq;
using Castle.MicroKernel;
using Castle.Windsor;
using DAL;
using DAL.Entities;
using Services.DataCollection;
using StockDataApi.General;

namespace Services.DI
{
    public static class CastleContainer
    {
        private static IWindsorContainer container;

        public static IWindsorContainer Instance
        {
            get { return container ??= new WindsorContainer(); }
            // exposing a setter alleviates some common component testing problems
            set => container = value;
        }

        // shortcut to make your life easier :)
        public static T Resolve<T>() => Instance.Resolve<T>();

        [Obsolete("Request should be done using DataRetrieverManager instead")]
        public static DataRetrieverBase ResolveRetriever(StockDbContext db, string retrieverName)
        {
            var entity = db.DataRetrievers.Single(d => d.Name == retrieverName)
                         ?? throw new Exception($"Could not find retriever '{retrieverName}'");
            return ResolveRetriever(entity);
        }

        private static DataRetrieverBase ResolveRetriever(DataRetriever retriever) =>
            Instance.Resolve<DataRetrieverBase>(retriever.Type, new Arguments { { "baseUrl", retriever.BaseUrl }, { "apiKey", retriever.Key }, { "priority", retriever.Priority } });
        
        public static DataRetrieverService ResolveRetrieverService(DataRetriever retrieverDb)
        {
            var drBase = ResolveRetriever(retrieverDb);
            return Instance.Resolve<DataRetrieverService>(new Arguments { { "dataRetriever", drBase } });
        }

        public static void Dispose()
        {
            container?.Dispose();
            container = null;
        }
    }
}