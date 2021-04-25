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

        public frmTransactions(TransactionOverviewService transactionOverviewService)
        {
            this.transactionOverviewService = transactionOverviewService;
            InitializeComponent();
        }

        private void frmTransactions_Load(object sender, System.EventArgs e)
        {
            PopulateStockGrid();
        }

        private void PopulateStockGrid()
        {
            var stockList = transactionOverviewService.GetStockList();
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

            if (qColumnIndex == e.ColumnIndex && (double)e.Value < 0)
                dgvTransactions.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.LightSlateGray;

        }
    }
}
