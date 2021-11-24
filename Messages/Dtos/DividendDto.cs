using System;

namespace Imports
{
    /// <summary>
    /// All values in <see cref="Currency"/>
    /// </summary>
    public class DividendDto
    {
        public string Isin { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Currency { get; set; }
        public double Value { get; set; }
        /// <summary>
        /// ratio = stock native / user currency
        /// </summary>
        public double CurrencyRatio { get; set; }
        public double Tax { get; set; }
        public double Costs { get; set; }
        public bool IsCapitalReturn { get; set; }
    }
}