using System;

namespace Messages.UI.Overview
{
    public class TransactionViewModel
    {
        public const string SumOf = "Sum of";
        public const string AnnualSumOf = "Annual sum of";
        public string Name { get; set; }
        public string Date { get; set; }
        public double Quantity { get; set; }
        public string Price { get; set; }
        public string Value { get; set; }
        public string Costs { get; set; }
        public string CurrRatio { get; set; }
    }
}