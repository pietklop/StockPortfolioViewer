using System.ComponentModel;

namespace Messages.UI.Overview
{
    public class RetrieverViewModel
    {
        [ColumnCellsUnderline]
        public string Name { get; set; }
        [DisplayName("Area")]
        public string SupportedArea { get; set; }
        [DisplayName("Last request")]
        public string LastRequest { get; set; }
    }
}