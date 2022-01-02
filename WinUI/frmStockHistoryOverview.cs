using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Dashboard.Helpers;
using Messages.UI.Overview;
using Services.Ui;

namespace Dashboard
{
    public partial class frmStockHistoryOverview : Form
    {
        private readonly frmMain frmMain;
        private readonly StockHistoryOverviewService stockHistoryOverviewService;

        public frmStockHistoryOverview(frmMain frmMain, StockHistoryOverviewService stockHistoryOverviewService)
        {
            this.frmMain = frmMain;
            this.stockHistoryOverviewService = stockHistoryOverviewService;
            InitializeComponent();
        }

        private void frmStockHistoryOverview_Load(object sender, EventArgs e)
        {
            PopulateStockGrid();
        }

        private void PopulateStockGrid()
        {
            var stockList = stockHistoryOverviewService.GetStockList();
            dgvStockList.DataSource = stockList;

            // column configuration
            dgvStockList.ApplyColumnDisplayFormatAttributes();
            dgvStockList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var nameColumn = dgvStockList.GetColumn(nameof(StockHistoryViewModel.Name));
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvStockList.SetReadOnly();
            dgvStockList.SetVisualStyling();
        }

        private void dgvStockList_SelectionChanged(object sender, EventArgs e) => dgvStockList.ClearSelection();

        private void dgvStockList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int[] redGreenColumnIndexes =
            {
                dgvStockList.GetColumn(nameof(StockHistoryViewModel.Profit)).Index,
                dgvStockList.GetColumn(nameof(StockHistoryViewModel.ProfitFraction)).Index,
            };

            if (redGreenColumnIndexes.Contains(e.ColumnIndex))
            {
                var value = (double)e.Value;
                if (value > 0)
                    e.CellStyle.ForeColor = Color.LawnGreen;
                else if (value < 0)
                    e.CellStyle.ForeColor = Color.Red;
            }
        }

        private void dgvStockList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var nameColumn = dgvStockList.GetColumn(nameof(StockHistoryViewModel.Name));
            var name = dgvStockList[nameColumn.Index, e.RowIndex].Value.ToString();
            var isinColumn = dgvStockList.GetColumn(nameof(StockHistoryViewModel.Isin));
            var isin = dgvStockList[isinColumn.Index, e.RowIndex].Value.ToString();
            var valueColumnIndex = dgvStockList.GetColumn(nameof(StockHistoryViewModel.Value)).Index;

            Close();

            if (e.ColumnIndex == valueColumnIndex)
                frmMain.ShowStockPerformance(name, isin);
            else
                frmMain.ShowStockDetails(name, isin);
        }

        private void dgvStockList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var stockList = stockHistoryOverviewService.GetStockList();
            dgvStockList.DoColumnOrdering(stockList, e.ColumnIndex);
        }
    }
}
