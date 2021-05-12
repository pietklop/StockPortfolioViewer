using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Castle.MicroKernel;
using Core;
using DAL;
using DAL.Entities;
using Dashboard.Helpers;
using Dashboard.Input;
using log4net;
using Messages.Dtos;
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
        private readonly frmMain frmMain;
        private readonly StockService stockService;
        private readonly StockDetailService stockDetailService;
        private readonly PortfolioDistributionService portfolioDistributionService;
        private readonly string stockIsin;

        public frmStockDetail(ILog log, StockDbContext db, frmMain frmMain, StockService stockService, StockDetailService stockDetailService, PortfolioDistributionService portfolioDistributionService, string stockIsin)
        {
            this.log = log;
            this.db = db;
            this.frmMain = frmMain;
            this.stockService = stockService;
            this.stockDetailService = stockDetailService;
            this.portfolioDistributionService = portfolioDistributionService;
            this.stockIsin = stockIsin;
            InitializeComponent();
        }

        private void frmStockDetail_Load(object sender, EventArgs e)
        {
            PopulateStockGrid();
        }

        private void frmStockDetail_Shown(object sender, EventArgs e)
        {
            dgvStockDetails.ClearSelection();

            //custom row config, does not work in form_load_call
            var underlineColumn = dgvStockDetails.GetColumn(nameof(StockPropertyViewModel.UnderlineRow));
            foreach (DataGridViewRow row in dgvStockDetails.Rows)
            {
                if ((bool)dgvStockDetails[underlineColumn.Index, row.Index].Value)
                    row.DefaultCellStyle.Font = new Font(dgvStockDetails.Font, FontStyle.Underline);
            }
        }

        private void PopulateStockGrid()
        {
            var stockList = stockDetailService.GetDetails(stockIsin);
            dgvStockDetails.DataSource = stockList;

            // column configuration
            dgvStockDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var nameColumn = dgvStockDetails.GetColumn(nameof(StockPropertyViewModel.Name));
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            var underlineColumn = dgvStockDetails.GetColumn(nameof(StockPropertyViewModel.UnderlineRow));
            underlineColumn.Visible = false;

            dgvStockDetails.SetReadOnly();
            dgvStockDetails.SetVisualStyling();
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
                case StockDetailProperties.Area:
                    ChangeArea();
                    break;
                case StockDetailProperties.Sector:
                    ChangeSector();
                    break;
                case StockDetailProperties.Bought:
                case StockDetailProperties.Sold:
                case StockDetailProperties.TransactionCosts:
                    frmMain.ShowStockTransactions(stockIsin);
                    break;
            }

            void ChangeName()
            {
                var newName = InputHelper.GetString(this, "Enter name");
                if (newName == null) return;
                var stock = db.Stocks.Single(s => s.Isin == stockIsin);
                stock.Name = newName;
                SaveAndUpdate(newName);
            }

            void ChangeSymbol()
            {
                var input = InputHelper.GetString(this, "Enter symbol");
                if (input == null) return;
                var stock = db.Stocks.Single(s => s.Isin == stockIsin);
                stock.Symbol = input;
                SaveAndUpdate(input);
            }

            void ChangeCurrentPrice()
            {
                var input = InputHelper.GetPositiveValue(this, "Enter price");
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

                    var frmDr = CastleContainer.Instance.Resolve<frmStockRetriever>(new Arguments { { "Stock", new StockDto{ Name = stock.Name, Isin = stock.Isin, Symbol = stock.Symbol} } });
                    frmDr.Show(this);
                    return;
                    
                    if (!InputHelper.GetConfirmation(this, $"Auto price update?")) return;
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

            void ChangeArea()
            {
                using var distributionForm = new frmDistribution(portfolioDistributionService.GetAreaDistribution(stockIsin));
                distributionForm.ShowDialog(this);
                if (!distributionForm.ResetRequest) return;

                var areas = db.Areas.Include(a => a.Continent).ToList();
                var input = DistributionInputHelper.GetDistribution(this, "Enter area share", areas.Where(a => a.IsContinent).Select(a => a.Name).ToList(), areas.Where(a => !a.IsContinent).Select(a => new AreaCountryInputDto(a.Continent.Name, a.Name)).ToList());
                if (input == null) return;
                var stock = db.Stocks.Include(s => s.AreaShares).Single(s => s.Isin == stockIsin);
                db.AreaShares.RemoveRange(stock.AreaShares);

                foreach (var newCountry in input.NewCountries)
                {
                    if (areas.Any(a => a.Name == newCountry.Country)) continue;
                    areas.Add(new Area {Continent = areas.Single(a => a.Name == newCountry.Continent), Name = newCountry.Country});
                }

                for (int i = 0; i < input.Keys.Length; i++)
                    stock.AreaShares.Add(new AreaShare {Area = areas.Single(a => a.Name == input.Keys[i]), Fraction = input.Fractions[i]});

                SaveAndUpdate(input.Keys.Length == 1 ? input.Keys[0] : "(Multiple)");
                if (stock.AreaShares.Count > 1) // user probably wants to see/check te result
                    ChangeArea();
            }

            void ChangeSector()
            {
                using var distributionForm = new frmDistribution(portfolioDistributionService.GetSectorDistribution(stockIsin));
                distributionForm.ShowDialog(this);
                if (!distributionForm.ResetRequest) return;
                
                var sectors = db.Sectors.ToList();
                var input = DistributionInputHelper.GetDistribution(this, "Enter sector share", sectors.Select(a => a.Name).ToList());
                if (input == null) return;
                var stock = db.Stocks.Include(s => s.SectorShares).Single(s => s.Isin == stockIsin);
                db.SectorShares.RemoveRange(stock.SectorShares);
                for (int i = 0; i < input.Keys.Length; i++)
                    stock.SectorShares.Add(new SectorShare {Sector = sectors.Single(a => a.Name == input.Keys[i]), Fraction = input.Fractions[i]});

                SaveAndUpdate(input.Keys.Length == 1 ? input.Keys[0] : "(Multiple)");
                if (stock.SectorShares.Count > 1) // user probably wants to see/check te result
                    ChangeSector();
            }

            void SaveAndUpdate(object newValue)
            {
                db.SaveChanges();
                dgvStockDetails[valueColumnIndex, e.RowIndex].Value = newValue;
            }
        }
    }
}
