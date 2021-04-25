using System;
using System.Linq;
using System.Windows.Forms;
using DAL;
using DAL.Entities;
using Dashboard.Helpers;
using Dashboard.Input;
using log4net;
using Messages.Dtos;
using Messages.UI.Overview;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.DI;
using Services.Ui;
using StockDataApi.General;

namespace Dashboard
{
    public partial class frmStockRetriever : Form
    {
        private readonly ILog log;
        private readonly StockDbContext db;
        private readonly StockDto stock;
        private readonly StockService stockService;
        private readonly StockRetrieverService stockRetrieverService;

        public frmStockRetriever(ILog log, StockDbContext db, StockDto stock, StockService stockService, StockRetrieverService stockRetrieverService)
        {
            this.log = log;
            this.db = db;
            this.stock = stock;
            this.stockService = stockService;
            this.stockRetrieverService = stockRetrieverService;
            InitializeComponent();
        }

        private void frmStockRetriever_Load(object sender, System.EventArgs e)
        {
            PopulateRetrieverGrid();
            lblStockName.Text = stock.Name;
        }

        private void PopulateRetrieverGrid()
        {
            var srList = stockRetrieverService.GetRetrievers(stock.Isin, stock.Symbol);
            dgvRetrieverList.DataSource = srList;

            // column configuration
            dgvRetrieverList.ApplyColumnDisplayFormatAttributes();
            dgvRetrieverList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var retrieverColumn = dgvRetrieverList.GetColumn(nameof(StockRetrieverViewModel.RetrieverName));
            retrieverColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvRetrieverList.SetReadOnly();
            dgvRetrieverList.SetVisualStyling();
        }

        private void dgvRetrieverList_SelectionChanged(object sender, System.EventArgs e)
        {
            dgvRetrieverList.ClearSelection();
        }

        private void dgvRetrieverList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            var retrieverColumn = dgvRetrieverList.GetColumn(nameof(StockRetrieverViewModel.RetrieverName)).Index;
            var retrieverName = dgvRetrieverList[retrieverColumn, e.RowIndex].Value.ToString();

            var propName = (string)dgvRetrieverList[retrieverColumn, e.RowIndex].Value;

            switch (dgvRetrieverList.Columns[e.ColumnIndex].Name)
            {
                case nameof(StockRetrieverViewModel.StockRef):
                    ChangeStockRef();
                    break;
                case nameof(StockRetrieverViewModel.Compatible):
                    TryRetrieveValue();
                    break;
                case nameof(StockRetrieverViewModel.Key):
                    ChangeKey();
                    break;
            }

            void TryRetrieveValue()
            {
                var stk = db.Stocks.Include(s => s.StockRetrieverCompatibilities).Include(s => s.Currency).Single(s => s.Isin == stock.Isin);
                var sr = GetOrCreateRetriever(stk, stock.Symbol);
                try
                {
                    var dre = db.DataRetrievers.SingleOrDefault(d => d.Name == retrieverName) ?? throw new Exception($"Could not find retriever '{retrieverName}'");
                    var dr = CastleContainer.Instance.Resolve<DataRetrieverBase>(dre.Type);
                    var priceDto = dr.GetStockQuote(sr.StockRef);
                    if (priceDto == null) throw new Exception($"Failed to retrieve stockValue of {stk} using {dre}");
                    stockService.UpdateStockPrice(stock.Isin, priceDto.Price, priceDto.LastPriceUpdate);
                    sr.Compatibility = RetrieverCompatibility.True;
                    db.SaveChanges();
                    MessageBox.Show($"{stk.Currency.Symbol}{priceDto.Price:F2} of {priceDto.LastPriceUpdate.ToShortDateString()} {priceDto.LastPriceUpdate.ToShortTimeString()}");
                }
                catch (Exception ex)
                {
                    log.Error($"Error during StockPrice data request", ex);
                    sr.Compatibility = RetrieverCompatibility.False;
                    db.SaveChanges();
                    MessageBox.Show($"Something went wrong. Msg: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            void ChangeStockRef()
            {
                var newName = InputHelper.GetString("Enter ref", stock.Symbol);
                if (newName == null) return;
                var stk = db.Stocks.Include(s => s.StockRetrieverCompatibilities).Single(s => s.Isin == stock.Isin);
                GetOrCreateRetriever(stk, null, newName);
            }

            void ChangeKey()
            {
                var newKey = InputHelper.GetString("Enter key");
                if (newKey == null) return;
                var dataRetriever = db.DataRetrievers.Single(s => s.Name == retrieverName);
                dataRetriever.Key = newKey;
                db.SaveChanges();
                PopulateRetrieverGrid();
            }

            StockRetrieverCompatibility GetOrCreateRetriever(Stock stk, string defaultStockRef, string newStockRef = null)
            {
                var sr = stk.StockRetrieverCompatibilities.FirstOrDefault(s => s.DataRetriever.Name == retrieverName);
                if (sr == null)
                {
                    var dr = db.DataRetrievers.SingleOrDefault(d => d.Name == retrieverName) ?? throw new Exception($"Could not find retriever '{retrieverName}'");
                    sr = new StockRetrieverCompatibility {DataRetriever = dr, Stock = stk, StockRef = newStockRef ?? defaultStockRef};
                    stk.StockRetrieverCompatibilities.Add(sr);
                }
                else if (newStockRef != null)
                {
                    sr.StockRef = newStockRef;
                    sr.Compatibility = RetrieverCompatibility.Unknown;
                }
                db.SaveChanges();
                PopulateRetrieverGrid();

                return sr;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
