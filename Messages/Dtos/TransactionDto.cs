using System;
using JetBrains.Annotations;

namespace Messages.Dtos
{
    public class TransactionDto
    {
        [CanBeNull]
        public string Name { get; set; }
        public string Isin { get; set; }
        public string Currency { get; set; }
        public DateTime TimeStamp { get; set; }
        public double Quantity { get; set; }
        /// <summary>
        /// [Stock native currency]
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Costs of the transaction [user currency]
        /// </summary>
        public double Costs { get; set; }
        /// <summary>
        /// Costs of the transaction [stock native currency]
        /// </summary>
        public double NativeCosts { get; set; }
        /// <summary>
        /// ratio = stock native / user currency
        /// </summary>
        public double CurrencyRatio { get; set; }
        public string Guid { get; set; }

        public override string ToString() => $"{Name} Qt:{Quantity:F0} Price:{Price:F2}";
    }
}