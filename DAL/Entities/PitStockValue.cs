﻿using System;
using Core;

namespace DAL.Entities
{
    /// <summary>
    /// Point In Time stock value
    /// </summary>
    public class PitStockValue : Entity
    {
        public int StockId { get; set; }
        public Stock Stock { get; set; }
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// Price in native currency of stock
        /// </summary>
        public double NativePrice { get; set; }
        /// <summary>
        /// Price in user defined base currency
        /// </summary>
        public double UserPrice { get; set; }
        /// <summary>
        /// Daily growth factor from previous PitValue till this one
        /// </summary>
        [Obsolete ("Value is calculated on the fly")]
        public double DailyGrowth { get; set; }

        public override string ToString() => $"{Constants.UserCurrencySymbol}{UserPrice:F2} {TimeStamp.ToShortDateString()}";
    }
}