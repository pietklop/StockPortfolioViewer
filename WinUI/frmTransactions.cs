using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Dashboard.Helpers;
using Messages.UI.Overview;
using Services;
using Services.Ui;

namespace Dashboard
{
    public partial class frmTransactions : Form
    {
        private readonly TransactionOverviewService transactionOverviewService;
        private readonly StockService stockService;
        private readonly string stockIsin;
        private double currentPrice;

        public frmTransactions(TransactionOverviewService transactionOverviewService, StockService stockService, string stockIsin = null)
        {
            this.transactionOverviewService = transactionOverviewService;
            this.stockService = stockService;
            this.stockIsin = stockIsin;
            InitializeComponent();
        }

        private void frmTransactions_Load(object sender, System.EventArgs e)
        {
            PopulateStockGrid();
            ShowCurrentPrice();
        }

        private void ShowCurrentPrice()
        {
            if (stockIsin == null) return;

            var stock = stockService.GetStockOrThrow(stockIsin);
            currentPrice = stock.LastKnownStockValue.StockValue.NativePrice;
            lblCurrentPrice.Text = currentPrice.FormatCurrency(stock.Currency.Symbol, false);
            lblCurrentPriceT.Visible = true;
            lblCurrentPrice.Visible = true;

            if (stock.Transactions.Count == 1) return;
            lblAvgBuy.Text = AvgBuy().FormatCurrency(stock.Currency.Symbol, false);
            lblAvgBuyT.Visible = true;
            lblAvgBuy.Visible = true;
            lblTotalValue.Text = TotalValue().FormatUserCurrency();
            lblTotalValue.Visible = true;
            lblTotalValueT.Visible = true;

            double AvgBuy()
            {
                var buys = stock.Transactions.Where(t => t.Quantity > 0).ToList();
                return buys.Sum(t => t.Quantity * t.StockValue.NativePrice) / buys.Sum(b => b.Quantity);
            }

            double TotalValue() => stock.LastKnownUserPrice * stock.Transactions.Sum(b => b.Quantity);
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
            if (dgvTransactions.GetColumn(nameof(TransactionViewModel.NativeValue)).Index == e.ColumnIndex)
            {
                var negative = ((string)e.Value).IndexOf('-') >= 0;
                dgvTransactions.Rows[e.RowIndex].DefaultCellStyle.ForeColor = negative ? Color.LightSlateGray : Color.Gainsboro;
            }

            if (dgvTransactions.GetColumn(nameof(TransactionViewModel.CurrentPrice)).Index == e.ColumnIndex)
            {
                var negative = ((string)e.Value)?.IndexOf('-') >= 0;
                var quantityColIndex = dgvTransactions.GetColumn(nameof(TransactionViewModel.Quantity)).Index;
                var sell = int.Parse(dgvTransactions[quantityColIndex, e.RowIndex].Value.ToString()) < 0;
                if (sell)
                    e.CellStyle.ForeColor = negative ? Color.DarkGreen : Color.Orange;
                else
                    e.CellStyle.ForeColor = negative ? Color.Red : Color.LawnGreen;
            }

            if (stockIsin != null)
            {
                var priceColumnIndex = dgvTransactions.GetColumn(nameof(TransactionViewModel.Price)).Index;
                if (priceColumnIndex == e.ColumnIndex)
                {
                    var quantityColumnIndex = dgvTransactions.GetColumn(nameof(TransactionViewModel.Quantity)).Index;
                    var bought = (double)dgvTransactions[quantityColumnIndex, e.RowIndex].Value > 0;

                    if (bought)
                    {
                        var hiddenPriceColumnIndex = dgvTransactions.GetColumn(nameof(TransactionViewModel.HiddenPrice)).Index;

                        var value = (double)dgvTransactions[hiddenPriceColumnIndex, e.RowIndex].Value;
                        if (value > currentPrice)
                            e.CellStyle.ForeColor = Color.Red;
                        else if (value < currentPrice)
                            e.CellStyle.ForeColor = Color.LawnGreen;
                    }
                }
            }

            var nameColumnIndex = dgvTransactions.GetColumn(nameof(TransactionViewModel.Name)).Index;
            if (nameColumnIndex == e.ColumnIndex && ((string) e.Value).StartsWith(TransactionViewModel.SumOf))
                dgvTransactions.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DarkBlue;
            else if (nameColumnIndex == e.ColumnIndex && ((string) e.Value).StartsWith(TransactionViewModel.AnnualSumOf))
                dgvTransactions.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.DarkRed;
        }
    }
}
