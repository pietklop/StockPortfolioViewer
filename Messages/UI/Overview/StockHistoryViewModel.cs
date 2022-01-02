using System.ComponentModel;

namespace Messages.UI.Overview
{
    public class StockHistoryViewModel
    {
        [ColumnCellsUnderline]
        public string Name { get; set; }
        [ColumnHide]
        public string Isin { get; set; }
        [Description("Total value at last sell")]
        [DisplayFormat("C0")]
        [ColumnCellsUnderline]
        public double Value { get; set; }
        [DisplayFormat("C0")]
        [Description("Including nett dividend")]
        public double Profit { get; set; }
        [DisplayName("Profit")]
        [DisplayFormat("P1")]
        [Description("Including nett dividend")]
        public double ProfitFraction { get; set; }
        [DisplayFormat("P1")]
        [DisplayName("First buy")]
        public string FirstBuy { get; set; }
        [DisplayFormat("P1")]
        [DisplayName("Last sell")]
        public string LastSell { get; set; }
    }
}