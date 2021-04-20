using System.Collections.Generic;
using System.Linq;
using DAL;
using log4net;
using Messages.UI.Overview;
using Microsoft.EntityFrameworkCore;
using Services.Helpers;

namespace Services.Ui
{
    public class StockRetrieverService
    {
        private readonly ILog log;
        private readonly StockDbContext db;

        public StockRetrieverService(ILog log, StockDbContext db)
        {
            this.log = log;
            this.db = db;
        }

        public List<StockRetrieverViewModel> GetRetrievers(string isin, string symbol)
        {
            const int keyMaxLength = 7;
            var stockRetrievers = db.Stocks
                .Include(s => s.StockRetrieverCompatibilities).ThenInclude(sr => sr.DataRetriever)
                .Where(s => s.Isin == isin)
                .SelectMany(s => s.StockRetrieverCompatibilities)
                .Select(r => new StockRetrieverViewModel
                {
                    RetrieverName = r.DataRetriever.Name,
                    Compatible = r.IsCompatible ? "V" : "X",
                    StockRef = r.StockRef,
                    Key = r.DataRetriever.Key.MaxLength(keyMaxLength),
                }).ToList();

            AddMissingRetrievers();

            return stockRetrievers;

            void AddMissingRetrievers()
            {
                var retrievers = db.DataRetrievers.ToList();

                foreach (var dataRetriever in retrievers)
                {
                    if (stockRetrievers.Any(sr => sr.RetrieverName == dataRetriever.Name))
                        continue;

                    stockRetrievers.Add(new StockRetrieverViewModel
                    {
                        RetrieverName = dataRetriever.Name,
                        Compatible = "?",
                        StockRef = symbol,
                        Key = dataRetriever.Key.MaxLength(keyMaxLength),
                    });
                }
            }
        }
    }
}