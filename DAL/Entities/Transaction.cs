using System;

namespace DAL.Entities
{
    public class Transaction : Entity
    {
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        /// <summary>
        /// Be aware this is NOT the transaction date, see <see cref="PitStockValue.TimeStamp"/> for that
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Positive will represent a buy, negative a sell
        /// </summary>
        public double Quantity { get; set; }
        public PitStockValue StockValue { get; set; }
        /// <summary>
        /// Costs of the transaction in user defined base currency
        /// </summary>
        public double UserCosts { get; set; }
        /// <summary>
        /// Can be used for example as ref to import
        /// </summary>
        public string ExtRef { get; set; }

        public override string ToString()
        {
            var txt = Stock == null ? $"{ExtRef} " : $"{Stock.Name} ";
            txt += $"n={Quantity} ";
            if (StockValue != null) txt += $"{StockValue.NativePrice} {StockValue.TimeStamp.Date}";

            return txt;
        }
    }
}