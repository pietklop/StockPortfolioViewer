using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Stock : Entity
    {
        [Required] public string Name { get; set; }
        [Required] public string Isin { get; set; }
        [Required] public Currency Currency { get; set; }

        public LastKnownStockValue LastKnownStockValue { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
        public ICollection<Dividend> Dividends { get; set; }
        public ICollection<PitStockValue> StockValues { get; set; }
        public ICollection<SectorShare> SectorShares { get; set; }
        public ICollection<AreaShare> AreaShares { get; set; }

        public double LastKnownUserPrice => LastKnownStockValue.StockValue.UserPrice;
        public override string ToString() => Name;
    }

    public class LastKnownStockValue : Entity
    {
        public PitStockValue StockValue { get; set; }
    }
}