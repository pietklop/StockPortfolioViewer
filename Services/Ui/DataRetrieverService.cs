using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using log4net;
using Messages.UI;
using Messages.UI.Overview;
using Services.DI;

namespace Services.Ui
{
    public class DataRetrieverService
    {
        private readonly ILog log;
        private readonly StockDbContext db;

        public DataRetrieverService(ILog log, StockDbContext db)
        {
            this.log = log;
            this.db = db;
        }

        public List<RetrieverLimitationViewModel> GetRetrieverLimitations(string name)
        {
            return db.RetrieverLimitations.Where(r => r.DataRetriever.Name == name)
                .Select(r => new RetrieverLimitationViewModel
                {
                    TimespanType = r.TimespanType,
                    Limit = r.Limit,
                    RequestsDone = r.RequestsDone,
                }).ToList();
        }

        public List<PropertyViewModel> GetDetails(string name)
        {
            var retriever = db.DataRetrievers.SingleOrDefault(d => d.Name == name)
                            ?? throw new Exception($"Could not find data-retriever with name: '{name}'");

            var dr = CastleContainer.ResolveRetrieverService(retriever);

            return new List<PropertyViewModel>
            {
                new PropertyViewModel{Name = "Name", Value = retriever.Name},
                new PropertyViewModel{Name = "Url", Value = retriever.BaseUrl},
                new PropertyViewModel{Name = "Priority", Value = retriever.Priority},
                new PropertyViewModel{Name = "Last request", Value = retriever.LastRequest.ToShortDateString()},
                new PropertyViewModel{Name = "Area", Value = retriever.SupportedArea},
                new PropertyViewModel{Name = "Currency", Value = dr.CanRetrieveCurrencies},
                new PropertyViewModel{Name = "Day behind", Value = dr.DataIsDayBehind},
            };
        }
    }
}