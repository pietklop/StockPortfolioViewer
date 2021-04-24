using System.ComponentModel;

namespace Messages.UI.Overview
{
    public class StockViewModel
    {
        public string Name { get; set; }
        public string Isin { get; set; }
        [Description("Total value of this currently owned stock")]
        [DisplayFormat("C0")] 
        public double Value { get; set; }
        [DisplayFormat("C0")]
        public double Profit { get; set; }
        [DisplayName("Profit")]
        [DisplayFormat("P1")]
        public double ProfitFraction { get; set; }
        [DisplayName("Portf. weight")]
        [DisplayFormat("P1")]
        [Description("Percentage of the complete portfolio value")]
        public double PortFolioFraction { get; set; }
        [DisplayName("Last update")]
        [Description("Last update of the stock price")]
        public string LastPriceUpdate { get; set; }
    }
}