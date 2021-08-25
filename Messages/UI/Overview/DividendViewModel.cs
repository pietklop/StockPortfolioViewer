using System.ComponentModel;

namespace Messages.UI.Overview
{
    public class DividendViewModel
    {
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
    }
}