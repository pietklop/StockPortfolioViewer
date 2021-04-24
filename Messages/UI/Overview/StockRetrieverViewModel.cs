using System.ComponentModel;

namespace Messages.UI.Overview
{
    public class StockRetrieverViewModel
    {
        [DisplayName("Retriever name")]
        public string RetrieverName { get; set; }
        [ColumnCellsUnderline]
        [Description("Symbol ref specific for this data-retriever")]
        public string StockRef { get; set; }
        [ColumnCellsUnderline]
        [Description("Click to retrieve price")]
        public string Compatible { get; set; }
        [ColumnCellsUnderline]
        [Description("Personal key to provide access to retriever")]
        public string Key { get; set; }
    }
}