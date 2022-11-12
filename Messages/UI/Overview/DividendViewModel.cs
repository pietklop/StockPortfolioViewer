using System.ComponentModel;

namespace Messages.UI.Overview
{
    public class DividendViewModel
    {
        public const string SumOf = "Sum of";
        public const string AnnualSumOf = "Annual sum of";
        public string Name { get; set; }
        public string Date { get; set; }
        [DisplayName("Nett")]
        public string NettValue { get; set; }
        public string Tax { get; set; }
        [Description("Broker costs")]
        public string Costs { get; set; }
        [Description("Nett percentage")]
        public string Percentage { get; set; }
        public string PayoutInterval { get; set; }
        [ColumnHide]
        public double DNetValue { get; set; }
        [ColumnHide]
        public double DTax { get; set; }
        [ColumnHide]
        public double DCosts { get; set; }
    }
}