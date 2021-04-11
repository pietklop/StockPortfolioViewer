using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Dashboard.Helpers;
using Messages.UI.Overview;
using Services.Ui;

namespace Dashboard
{
    public partial class frmOverview : Form
    {
        private readonly StockOverviewService stockOverviewService;

        public frmOverview(StockOverviewService stockOverviewService)
        {
            this.stockOverviewService = stockOverviewService;
            InitializeComponent();
        }

        private void frmOverview_Load(object sender, EventArgs e)
        {
            PopulateStockGrid();
        }

        private void PopulateStockGrid()
        {
            var stockList = stockOverviewService.GetStockList();
            dgvStockList.DataSource = stockList;

            // column configuration
            dgvStockList.ApplyColumnDisplayFormatAttributes();
            dgvStockList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var nameColumn = dgvStockList.GetColumn(nameof(StockViewModel.Name));
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvStockList.SetReadOnly();
            dgvStockList.SetVisualStyling();
        }

        private void dgvStockList_SelectionChanged(object sender, EventArgs e)
        {
            dgvStockList.ClearSelection();
        }

        private void dgvStockList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int[] redGreenColumnIndexes =
            {
                dgvStockList.GetColumn(nameof(StockViewModel.Profit)).Index,
                dgvStockList.GetColumn(nameof(StockViewModel.ProfitFraction)).Index,
            };
            
            if (redGreenColumnIndexes.Contains(e.ColumnIndex))
                e.CellStyle.ForeColor = (double) e.Value < 0 ? Color.Red : Color.LawnGreen;
        }
    }
}
