using System;
using System.Drawing;
using System.Windows.Forms;
using Core;
using Dashboard.Helpers;
using Messages.UI.Overview;
using Services.Ui;

namespace Dashboard
{
    public partial class frmDividends : Form
    {
        private readonly DividendOverviewService dividendOverviewService;
        private readonly string stockIsin;

        public frmDividends(DividendOverviewService dividendOverviewService, string stockIsin = null)
        {
            this.dividendOverviewService = dividendOverviewService;
            this.stockIsin = stockIsin;
            InitializeComponent();
        }

        private void frmDividends_Load(object sender, EventArgs e)
        {
            cmbViewMode.Visible = stockIsin == null;
            cmbViewMode.DataSource = Enum.GetValues(typeof(DividendViewMode));
            cmbViewMode.SelectedItem = DividendViewMode.LastTwelveMonths;

            PopulateStockGrid();
        }

        private void PopulateStockGrid()
        {
            var viewMode = (DividendViewMode)cmbViewMode.SelectedItem;
            var stockList = dividendOverviewService.GetStockList(viewMode, stockIsin);
            dgvDividends.DataSource = stockList;

            // column configuration
            dgvDividends.ApplyColumnDisplayFormatAttributes();
            dgvDividends.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var nameColumn = dgvDividends.GetColumn(nameof(DividendViewModel.Name));
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvDividends.SetReadOnly();
            dgvDividends.SetVisualStyling();
        }

        private void cmbViewMode_SelectionChangeCommitted(object sender, EventArgs e) => PopulateStockGrid();

        private void dgvDividends_SelectionChanged(object sender, EventArgs e) => dgvDividends.ClearSelection();

        private void dgvDividends_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dateColumnIndex = dgvDividends.GetColumn(nameof(DividendViewModel.Date)).Index;

            if (dateColumnIndex == e.ColumnIndex)
                dgvDividends[dateColumnIndex, e.RowIndex].Style.ForeColor = ((string)e.Value).EndsWith("*") ? Color.Red : Color.Gainsboro;

            var viewMode = (DividendViewMode)cmbViewMode.SelectedItem;

            var nameColumnIndex = dgvDividends.GetColumn(nameof(DividendViewModel.Name)).Index;
            if ((viewMode == DividendViewMode.LastTwelveMonths || stockIsin.HasValue()) && nameColumnIndex == e.ColumnIndex && ((string)e.Value).StartsWith(DividendViewModel.AnnualSumOf))
                dgvDividends.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DarkBlue;
        }
    }
}
