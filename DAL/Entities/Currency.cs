using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities
{
    public class Currency
    {
        [StringLength(3)]
        public string Key { get; set; }
        public string Symbol { get; set; }
        /// <summary>
        /// Ratio => price of 1EUR
        /// This is only used to calculate actual values
        /// </summary>
        public double Ratio { get; set; }
        public DateTime LastUpdate { get; set; }

        public override string ToString() => Key;
    }
}