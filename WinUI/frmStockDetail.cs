using System;
using System.Linq;
using System.Windows.Forms;
using Core;
using DAL;
using Dashboard.Helpers;
using Dashboard.Input;
using log4net;
using Messages.UI.StockDetails;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.DI;
using Services.Ui;
using StockDataApi.IexCloud;

namespace Dashboard
{
    public partial class frmStockDetail : Form
    {
        private readonly ILog log;
        private readonly StockDbContext db;
        private readonly StockService stockService;
        private readonly StockDetailService stockDetailService;
        private readonly string stockIsin;

        public frmStockDetail(ILog log, StockDbContext db, StockService stockService, StockDetailService stockDetailService, string stockIsin)
        {
            this.log = log;
            this.db = db;
            this.stockService = stockService;
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

        private void dgvStockDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            var nameColumnIndex = dgvStockDetails.GetColumn(nameof(StockPropertyViewModel.Name)).Index;
            var valueColumnIndex = dgvStockDetails.GetColumn(nameof(StockPropertyViewModel.Value)).Index;
            var propName = (string)dgvStockDetails[nameColumnIndex, e.RowIndex].Value;

            switch (propName)
            {
                case StockDetailProperties.Name:
                    ChangeName();
                    break;
                case StockDetailProperties.Symbol:
                    ChangeSymbol();
                    break;
                case StockDetailProperties.CurrentPrice:
                    ChangeCurrentPrice();
                    break;
                case StockDetailProperties.LastPriceUpdate:
                    TryAutoPriceUpdate();
                    break;
            }

            void ChangeName()
            {
                var newName = InputHelper.GetString("Enter name");
                if (newName == null) return;
                var stock = db.Stocks.Single(s => s.Isin == stockIsin);
                stock.Name = newName;
                SaveAndUpdate(newName);
            }

            void ChangeSymbol()
            {
                var input = InputHelper.GetString("Enter symbol");
                if (input == null) return;
                var stock = db.Stocks.Single(s => s.Isin == stockIsin);
                stock.Symbol = input;
                SaveAndUpdate(input);
            }

            void ChangeCurrentPrice()
            {
                var input = InputHelper.GetPositiveValue("Enter price");
                if (input == null) return;
                var stock = stockService.UpdateStockPrice(stockIsin, input.Value);
                SaveAndUpdate($"{stock.Currency.Symbol}{input:F2}");
            }

            void TryAutoPriceUpdate()
            {
                try
                {
                    var stock = db.Stocks.Single(s => s.Isin == stockIsin);
                    if (string.IsNullOrEmpty(stock.Symbol)) throw new Exception($"Symbol can not be empty");
                    if (!InputHelper.GetConfirmation($"Auto price update?")) return;
                    var dr = CastleContainer.Resolve<IexDataRetriever>();
                    var priceDto = dr.GetStockQuote(stock.Symbol);
                    stockService.UpdateStockPrice(stockIsin, priceDto.Price);
                    SaveAndUpdate($"{stock.Currency.Symbol}{priceDto.Price:F2}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Something went wrong. Msg: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    log.Error($"Error during StockPrice data request", ex);
                }
            }

            void SaveAndUpdate(object newValue)
            {
                db.SaveChanges();
                dgvStockDetails[valueColumnIndex, e.RowIndex].Value = newValue;
            }
        }
    }
}
