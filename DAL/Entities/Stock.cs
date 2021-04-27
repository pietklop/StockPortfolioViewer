using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Stock : Entity
    {
        [Required] public string Name { get; set; }
        [Required] public string Isin { get; set; }
        public string Symbol { get; set; }
        [Required] public Currency Currency { get; set; }

        public LastKnownStockValue LastKnownStockValue { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Dividend> Dividends { get; set; }
        public ICollection<PitStockValue> StockValues { get; set; }
        public ICollection<SectorShare> SectorShares { get; set; }
        public ICollection<AreaShare> AreaShares { get; set; }
        public ICollection<StockRetrieverCompatibility> StockRetrieverCompatibilities { get; set; }

        public double LastKnownUserPrice => LastKnownStockValue.StockValue.UserPrice;
        public override string ToString() => $"'{Name}'";
    }

    public class LastKnownStockValue : Entity
    {
        public int StockValueId { get; set; }
        public PitStockValue StockValue { get; set; }
        /// <summary>
        /// This timestamp differs from <see cref="PitStockValue.TimeStamp"/> when the
        /// synchronization is done while the market was closed
        /// </summary>
        [Required] public DateTime LastUpdate { get; set; }
    }
}