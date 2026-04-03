using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Core;
using DAL;
using DAL.Entities;
using Dashboard.Helpers;
using Dashboard.Input;
using log4net;
using Messages.UI;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Ui;

namespace Dashboard.StockDetails;

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

    private void frmStockDetail_Load(object sender, EventArgs e) => PopulateStockGrid();

    private void frmStockDetail_Shown(object sender, EventArgs e)
    {
        dgvStockDetails.ClearSelection();

        //custom row config, does not work in form_load_call
        var underlineColumn = dgvStockDetails.GetColumn(nameof(PropertyViewModel.UnderlineRow));
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
        var nameColumn = dgvStockDetails.GetColumn(nameof(PropertyViewModel.Name));
        nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        var underlineColumn = dgvStockDetails.GetColumn(nameof(PropertyViewModel.UnderlineRow));
        underlineColumn.Visible = false;

        dgvStockDetails.SetReadOnly();
        dgvStockDetails.SetVisualStyling();
    }

    private void dgvStockDetails_CellClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
        var nameColumnIndex = dgvStockDetails.GetColumn(nameof(PropertyViewModel.Name)).Index;
        var valueColumnIndex = dgvStockDetails.GetColumn(nameof(PropertyViewModel.Value)).Index;
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
                var name = db.Stocks.Single(s => s.Isin == stockIsin).Name;
                frmMain.ShowStockTransactions(name, stockIsin);
                break;
            case StockDetailProperties.Dividend:
                frmMain.ShowDividends(stockIsin);
                break;
            case StockDetailProperties.DividendPayout:
                ChangeDividendPayout();
                break;
            case StockDetailProperties.ExpenseRatio:
                ChangeExpenseRatio();
                break;
            case StockDetailProperties.AlarmCondition:
            case StockDetailProperties.Remarks:
                ChangeAlarmCondition();
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

        void ChangeArea()
        {
            var currentDistribution = portfolioDistributionService.GetAreaDistribution(stockIsin);
            using var distributionForm = new frmDistribution(currentDistribution);
            distributionForm.ShowDialog(this);
            if (!distributionForm.EditRequest) return;

            var areas = db.Areas.Include(a => a.Continent).ToList();
            using var distributionEditForm = new frmDistributionEdit(currentDistribution, areas.Where(a => !a.IsContinent).Select(a => new AreaCountryInputDto(a.Continent!.Name, a.Name)).ToList());
            distributionEditForm.ShowDialog(this);
            if (!distributionEditForm.SaveRequest) return;

            var stock = db.Stocks.Include(s => s.AreaShares).Single(s => s.Isin == stockIsin);
            db.AreaShares.RemoveRange(stock.AreaShares);

            foreach (var newCountry in distributionEditForm.Countries)
            {
                if (areas.Any(a => a.Name == newCountry.Country)) continue;
                areas.Add(new Area {Continent = areas.Single(a => a.Name == newCountry.Continent), Name = newCountry.Country});
            }

            var newDistribution = distributionEditForm.Distribution;

            foreach (var d in newDistribution)
                stock.AreaShares.Add(new AreaShare {Area = areas.Single(a => a.Name == d.Key), Fraction = d.Fraction});

            stock.LastSectorUpdate = DateTime.Now;

            SaveAndUpdate(newDistribution.Count == 1 ? newDistribution[0].Key : "(Multiple)");
            if (stock.AreaShares.Count > 1) // user probably wants to see/check te result
                ChangeArea();
        }

        void ChangeSector()
        {
            var currentDistribution = portfolioDistributionService.GetSectorDistribution(stockIsin);
            using var distributionForm = new frmDistribution(currentDistribution);
            distributionForm.ShowDialog(this);
            if (!distributionForm.EditRequest) return;

            var sectors = db.Sectors.ToList();
            using var distributionEditForm = new frmDistributionEdit(currentDistribution, sectors.Select(a => a.Name).ToList());
            distributionEditForm.ShowDialog(this);
            if (!distributionEditForm.SaveRequest) return;

            var newDistribution = distributionEditForm.Distribution;

            var stock = db.Stocks.Include(s => s.SectorShares).Single(s => s.Isin == stockIsin);
            db.SectorShares.RemoveRange(stock.SectorShares);
            for (int i = 0; i < newDistribution.Count; i++)
                stock.SectorShares.Add(new SectorShare {Sector = sectors.Single(a => a.Name == newDistribution[i].Key), Fraction = newDistribution[i].Fraction});

            stock.LastSectorUpdate = DateTime.Now;

            SaveAndUpdate(newDistribution.Count == 1 ? newDistribution[0].Key : "(Multiple)");
            if (stock.SectorShares.Count > 1) // user probably wants to see/check te result
                ChangeSector();
        }

        void ChangeDividendPayout()
        {
            var input = InputHelper.GetListIndex(this, "Select div payout", Enum.GetNames(typeof(DividendPayoutInterval)).Skip(1).ToList());
            if (input == -1) return;
            var divPayout = (DividendPayoutInterval) input + 1;
            db.Stocks.Single(s => s.Isin == stockIsin).DividendPayoutInterval = divPayout;
            SaveAndUpdate($"{divPayout}");
        }

        void ChangeExpenseRatio()
        {
            var input = InputHelper.GetPositiveValue(this, "Enter ratio [%]");
            if (input == null) return;
            input /= 100;
            if (input >= 0.01)
                MessageBox.Show($"{input:P0}!? Thats expensive, i would take another one", "Wow", MessageBoxButtons.OK, MessageBoxIcon.Question);
            db.Stocks.Single(s => s.Isin == stockIsin).ExpenseRatio = input.Value;
            SaveAndUpdate($"{input:P2}");
        }

        void ChangeAlarmCondition()
        {
            var stock = db.Stocks
                .Include(s => s.LastKnownStockValue.StockValue)
                .Single(s => s.Isin == stockIsin);

            using var alarmConditionInputForm = new frmAlarmConditionInput(stock.AlarmLowerThreshold, stock.AlarmUpperThreshold, stock.Remarks, stock.LastKnownStockValue.StockValue.NativePrice);
            alarmConditionInputForm.ShowDialog(this);
            if (alarmConditionInputForm.DialogResult != DialogResult.OK) return;

            stock.AlarmLowerThreshold = alarmConditionInputForm.LowerThreshold;
            stock.AlarmUpperThreshold = alarmConditionInputForm.UpperThreshold;
            stock.Remarks = alarmConditionInputForm.Remarks;
            db.SaveChanges();
        }

        void SaveAndUpdate(object newValue)
        {
            db.SaveChanges();
            dgvStockDetails[valueColumnIndex, e.RowIndex].Value = newValue;
        }
    }
}