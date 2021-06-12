using System.Drawing;
using System.Windows.Forms;
using Dashboard.Helpers;
using Messages.UI.Overview;
using Services.Ui;

namespace Dashboard
{
    public partial class frmTransactions : Form
    {
        private readonly TransactionOverviewService transactionOverviewService;
        private readonly string stockIsin;

        public frmTransactions(TransactionOverviewService transactionOverviewService, string stockIsin = null)
        {
            this.transactionOverviewService = transactionOverviewService;
            this.stockIsin = stockIsin;
            InitializeComponent();
        }

        private void frmTransactions_Load(object sender, System.EventArgs e)
        {
            PopulateStockGrid();
        }

        private void PopulateStockGrid()
        {
            var stockList = transactionOverviewService.GetStockList(stockIsin);
            dgvTransactions.DataSource = stockList;

            // column configuration
            dgvTransactions.ApplyColumnDisplayFormatAttributes();
            dgvTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var nameColumn = dgvTransactions.GetColumn(nameof(TransactionViewModel.Name));
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvTransactions.SetReadOnly();
            dgvTransactions.SetVisualStyling();
        }

        private void dgvTransactions_SelectionChanged(object sender, System.EventArgs e) => dgvTransactions.ClearSelection();

        private void dgvTransactions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var qColumnIndex = dgvTransactions.GetColumn(nameof(TransactionViewModel.Quantity)).Index;

            if (qColumnIndex == e.ColumnIndex)
                dgvTransactions.Rows[e.RowIndex].DefaultCellStyle.ForeColor = (double) e.Value > 0 ? Color.Gainsboro : Color.LightSlateGray;

            var nameColumnIndex = dgvTransactions.GetColumn(nameof(TransactionViewModel.Name)).Index;
            if (nameColumnIndex == e.ColumnIndex && ((string) e.Value).StartsWith(TransactionViewModel.SumOf))
                dgvTransactions.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DarkBlue;
        }
    }
}
