using System.ComponentModel;

namespace Messages.UI.Overview
{
    public class StockViewModel
    {
        [ColumnCellsUnderline]
        public string Name { get; set; }
        [ColumnHide]
        public string Isin { get; set; }
        [Description("Total value of this currently owned stock")]
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
        [DisplayName("Portf.")]
        [DisplayFormat("P1")]
        [Description("Percentage of the complete portfolio value")]
        public double PortFolioFraction { get; set; }
        [DisplayName("30d")]
        [DisplayFormat("P1")]
        [Description("Profit over last 30 days")]
        public double ProfitFractionLast30Days { get; set; }
        [DisplayName("7d")]
        [DisplayFormat("P1")]
        [Description("Profit over last 7 days")]
        public double ProfitFractionLast7Days { get; set; }
        [DisplayName("Last change")]
        [Description("Last price change (always behind when exchange is closed)")]
        public string LastPriceChange { get; set; }
        public string Remark { get; set; }
        // [DisplayName("Retrievers")]
        // [Description("Compatible data-retrievers")]
        // public string CompatibleDataRetrievers { get; set; }
    }
}