using System;
using JetBrains.Annotations;

namespace DAL.Entities
{
    public class Dividend : Entity
    {
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public DateTime Created { get; set; }
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// Value in native currency of stock
        /// </summary>
        [CanBeNull]
        public double? NativeValue { get; set; }
        /// <summary>
        /// Value in user defined base currency
        /// </summary>
        public double UserValue { get; set; }
        public double UserCosts { get; set; }
        public double UserTax { get; set; }
    }
}