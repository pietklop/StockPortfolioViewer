using System;
using System.Windows.Forms;
using Dashboard.Helpers;
using Messages.UI.StockDetails;
using Services.Ui;

namespace Dashboard
{
    public partial class frmStockDetail : Form
    {
        private readonly StockDetailService stockDetailService;
        private readonly string stockIsin;

        public frmStockDetail(StockDetailService stockDetailService, string stockIsin)
        {
            this.stockDetailService = stockDetailService;
            this.stockIsin = stockIsin;
            InitializeComponent();
        }

        private void frmStockDetail_Load(object sender, EventArgs e)
        {
            PopulateStockGrid();
        }

        private void PopulateStockGrid()
        {
            var stockList = stockDetailService.GetDetails(stockIsin);
            dgvStockDetails.DataSource = stockList;

            // column configuration
            dgvStockDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var nameColumn = dgvStockDetails.GetColumn(nameof(StockPropertyViewModel.Name));
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvStockDetails.SetReadOnly();
            dgvStockDetails.SetVisualStyling();
        }

        private void dgvStockDetails_SelectionChanged(object sender, EventArgs e)
        {
            dgvStockDetails.ClearSelection();
        }
    }
}
