using System;

namespace Messages.Dtos
{
    public class StockValueDto
    {
        public string Name { get; set; }
        public string Isin { get; set; }
        public string Currency { get; set; }
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// [Stock native currency]
        /// </summary>
        public double ClosePrice { get; set; }
    }
}