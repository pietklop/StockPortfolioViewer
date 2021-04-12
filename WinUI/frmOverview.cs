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
        private readonly frmMain frmMain;
        private readonly StockOverviewService stockOverviewService;

        public frmOverview(frmMain frmMain, StockOverviewService stockOverviewService)
        {
            this.frmMain = frmMain;
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
            var isinColumn = dgvStockList.GetColumn(nameof(StockViewModel.Isin));
            isinColumn.Visible = false;

            dgvStockList.SetReadOnly();
            dgvStockList.SetVisualStyling();
        }

        private void dgvStockList_SelectionChanged(object sender, EventArgs e) => dgvStockList.ClearSelection();

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

        private void dgvStockList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var isinColumn = dgvStockList.GetColumn(nameof(StockViewModel.Isin));
            var isin = dgvStockList[isinColumn.Index, e.RowIndex].Value.ToString();
            var nameColumn = dgvStockList.GetColumn(nameof(StockViewModel.Name));
            var name = dgvStockList[nameColumn.Index, e.RowIndex].Value.ToString();

            Close();

            frmMain.ShowStockDetails(name, isin);
        }
    }
}
