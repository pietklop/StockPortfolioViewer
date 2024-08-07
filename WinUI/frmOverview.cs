﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Core;
using Dashboard.Helpers;
using Messages.UI.Overview;
using Services;
using Services.DI;
using Services.Ui;

namespace Dashboard
{
    public partial class frmOverview : Form
    {
        private readonly frmMain frmMain;
        private readonly StockOverviewService stockOverviewService;
        private readonly StockService stockService;
        private readonly PortfolioDistributionService portfolioDistributionService;
        private List<Button> distributionButtons = new List<Button>();
        private List<string> selectedIsins;
        private static int? orderedColumnIndex = null;
        private static SortOrder sortOrder = SortOrder.Ascending;

        public frmOverview(frmMain frmMain, StockOverviewService stockOverviewService, StockService stockService, PortfolioDistributionService portfolioDistributionService)
        {
            this.frmMain = frmMain;
            this.stockOverviewService = stockOverviewService;
            this.stockService = stockService;
            this.portfolioDistributionService = portfolioDistributionService;
            InitializeComponent();

            distributionButtons.Add(btnDistributionArea);
            distributionButtons.Add(btnDistributionContinent);
            distributionButtons.Add(btnDistributionCurrency);
            distributionButtons.Add(btnDistributionSector);
        }

        private void frmOverview_Load(object sender, EventArgs e)
        {
            PopulateStockGrid();
            chart.ConfigPieChart();
            btnDistributionArea_Click(btnDistributionArea, EventArgs.Empty);
            if (frmMain.nTotalStocks > 10)
                txtFilter.BringToFront(); // to not hide behind graph control
            else
                txtFilter.Visible = false; // no use to show with little number of stocks
        }

        private void PopulatePieChart(PortfolioDistributionDto dto) =>
            chart.PopulatePieChart(dto.Labels, dto.Fractions);

        private void PopulateStockGrid(bool reload = false, List<string> isins = null)
        {
            var stockList = stockOverviewService.GetStockList(reload, isins);
            if (frmMain.nTotalStocks == 0) frmMain.nTotalStocks = stockList.Count;
            dgvStockList.DataSource = stockList;

            // column configuration
            dgvStockList.ApplyColumnDisplayFormatAttributes();
            dgvStockList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var nameColumn = dgvStockList.GetColumn(nameof(StockViewModel.Name));
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvStockList.SetReadOnly();
            dgvStockList.SetVisualStyling();

            if (orderedColumnIndex.HasValue)
                dgvStockList.DoColumnOrdering(stockList, orderedColumnIndex.Value, sortOrder);
        }

        private void dgvStockList_SelectionChanged(object sender, EventArgs e) => dgvStockList.ClearSelection();

        private void dgvStockList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int[] redGreenColumnIndexes =
            {
                dgvStockList.GetColumn(nameof(StockViewModel.Profit)).Index,
                dgvStockList.GetColumn(nameof(StockViewModel.ProfitFraction)).Index,
                dgvStockList.GetColumn(nameof(StockViewModel.ProfitFractionLast30Days)).Index,
                dgvStockList.GetColumn(nameof(StockViewModel.ProfitFractionLast7Days)).Index,
            };

            if (redGreenColumnIndexes.Contains(e.ColumnIndex))
            {
                var value = (double) e.Value;
                if (value > 0)
                    e.CellStyle.ForeColor = Color.LawnGreen;
                else if (value < 0)
                    e.CellStyle.ForeColor = Color.Red;
            }

            var nameColIndex = dgvStockList.GetColumn(nameof(StockViewModel.Name)).Index;
            if (e.ColumnIndex == nameColIndex && e.Value != null)
            {
                if (((string)e.Value).EndsWith("-"))
                    e.CellStyle.ForeColor = Color.Red;
                else if (((string)e.Value).EndsWith("+"))
                    e.CellStyle.ForeColor = Color.LawnGreen;
            }

            var lastUpdateColIndex = dgvStockList.GetColumn(nameof(StockViewModel.LastPriceChange)).Index;
            if (e.ColumnIndex == lastUpdateColIndex && e.Value != null && ((string)e.Value).Contains("days"))
                e.CellStyle.ForeColor = Color.Red;
        }

        private void dgvStockList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var nameColumn = dgvStockList.GetColumn(nameof(StockViewModel.Name));
            var name = dgvStockList[nameColumn.Index, e.RowIndex].Value.ToString();
            if (name == Constants.Total)
            {
                MessageBox.Show("Computer says no");
                return;
            }
            var isinColumn = dgvStockList.GetColumn(nameof(StockViewModel.Isin));
            var isin = dgvStockList[isinColumn.Index, e.RowIndex].Value.ToString();
            var valueColumnIndex = dgvStockList.GetColumn(nameof(StockViewModel.Value)).Index;

            Close();

            if (e.ColumnIndex == valueColumnIndex)
                frmMain.ShowStockTransactions(name, isin);
            else
                frmMain.ShowStockDetails(name, isin);
        }

        private void dgvStockList_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (orderedColumnIndex.HasValue && orderedColumnIndex == e.ColumnIndex) // same column => swap order
                sortOrder = sortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            else
                sortOrder = SortOrder.Ascending;

            orderedColumnIndex = e.ColumnIndex;
            PopulateStockGrid();
        }

        private void SetDistributionButtons(Button activeButton)
        {
            distributionButtons.ForEach(b => b.FlatAppearance.BorderSize = 0);
            activeButton.FlatAppearance.BorderSize = 2;
        }

        private void btnDistributionArea_Click(object sender, EventArgs e)
        {
            PopulatePieChart(portfolioDistributionService.GetAreaDistribution(selectedIsins));
            var btn = (Button) sender;
            SetDistributionButtons(btn);
        }

        private void btnDistributionContinent_Click(object sender, EventArgs e)
        {
            PopulatePieChart(portfolioDistributionService.GetAreaDistributionByContinent(selectedIsins));
            var btn = (Button)sender;
            SetDistributionButtons(btn);
        }

        private void btnDistributionCurrency_Click(object sender, EventArgs e)
        {
            PopulatePieChart(portfolioDistributionService.GetCurrencyDistribution(selectedIsins));
            var btn = (Button)sender;
            SetDistributionButtons(btn);
        }

        private void btnDistributionSector_Click(object sender, EventArgs e)
        {
            PopulatePieChart(portfolioDistributionService.GetSectorDistribution(selectedIsins));
            var btn = (Button)sender;
            SetDistributionButtons(btn);
        }

        private void btnReloadGrid_Click(object sender, EventArgs e)
        {
            PopulateStockGrid(true);
            selectedIsins = null;
        }

        public void Reload() => btnReloadGrid_Click(this, EventArgs.Empty);

        private void btnSelect_Click(object sender, EventArgs e)
        {
            using var form = CastleContainer.Resolve<Input.frmStockSelection>();
            if (form.ShowDialog(this) == DialogResult.OK && form.Stocks.Any())
            {
                selectedIsins = form.Stocks.Select(s => s.Isin).ToList();
                PopulateStockGrid(true, selectedIsins);
                btnDistributionArea_Click(btnDistributionArea, EventArgs.Empty);
            }
        }

        private void txtFilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter || txtFilter.TextLength < 2) return;
            var stocks = stockService.GetStocks(txtFilter.Text);
            PopulateStockGrid(true, stocks.Select(s => s.Isin).ToList());
        }
    }
}
