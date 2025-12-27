using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Core;
using Dashboard.Helpers;
using Messages.UI.Overview;
using Services.DI;
using Services.Helpers;
using Services.Ui;

namespace Dashboard
{
    public partial class frmStockPerformance : Form
    {
        private readonly Settings settings;
        private readonly StockPerformanceService stockPerformanceService;
        private readonly StockPerformanceOverviewService stockPerformanceOverviewService;
        private List<string> stockIsins;
        private List<string> stockNames = null;

        public frmStockPerformance(Settings settings, StockPerformanceService stockPerformanceService, StockPerformanceOverviewService stockPerformanceOverviewService) : this(settings, stockPerformanceService, stockPerformanceOverviewService, (List<string>)null)
        {
        }

        public frmStockPerformance(Settings settings, StockPerformanceService stockPerformanceService, StockPerformanceOverviewService stockPerformanceOverviewService, List<string> stockIsins = null)
        {
            this.settings = settings;
            this.stockPerformanceService = stockPerformanceService;
            this.stockPerformanceOverviewService = stockPerformanceOverviewService;
            this.stockIsins = stockIsins;
            InitializeComponent();
        }

        private void frmStockPerformance_Load(object sender, EventArgs e)
        {
            PopulateStockGrid();
            if (!MultipleStocks())
                btnSelect.Visible = false;
            cmbPeriod.DataSource = Enum.GetValues(typeof(Period));
            cmbPeriod.SelectedItem = Period.AllTime;
            chart.ConfigXyChart();
            PopulateGraph();
        }

        private void PopulateStockGrid()
        {
            // if (stockNames == null)
            // {
            //     dgvStocks.Visible = false;
            //     return;
            // }
            var performanceInterval = PerformanceInterval.Year; // TEMP
            var stockList = stockPerformanceOverviewService.GetStockList(stockIsins, performanceInterval);
            dgvStocks.DataSource = ShowCurrentOnly(stockList);

            // column configuration
            dgvStocks.ApplyColumnDisplayFormatAttributes();
            dgvStocks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            var nameColumn = dgvStocks.GetColumn(nameof(StockPerformanceOverviewModel.Name));
            nameColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            var t0Column = dgvStocks.GetColumn(nameof(StockPerformanceOverviewModel.PerformanceFractionT0));
            var tMin1Column = dgvStocks.GetColumn(nameof(StockPerformanceOverviewModel.PerformanceFractionTMin1));
            var tMin2Column = dgvStocks.GetColumn(nameof(StockPerformanceOverviewModel.PerformanceFractionTMin2));
            var tMin3Column = dgvStocks.GetColumn(nameof(StockPerformanceOverviewModel.PerformanceFractionTMin3));
            var today = DateTime.Today;
            switch (performanceInterval) // todo, gebruik zelfde logica als in stockPerformanceOverviewService
            {
                case PerformanceInterval.Month:
                    // if (today.Day <= 10) today = today.AddDays(-10);
                    t0Column.HeaderText = today.GetMonthShort();
                    tMin1Column.HeaderText = today.AddMonths(-1).GetMonthShort();
                    tMin2Column.HeaderText = today.AddMonths(-2).GetMonthShort();
                    tMin3Column.HeaderText = today.AddMonths(-3).GetMonthShort();
                    break;
                case PerformanceInterval.Quarter:
                    // if (today.Month == 1) today = today.AddMonths(-1);
                    t0Column.HeaderText = today.GetQuarterShort();
                    tMin1Column.HeaderText = today.AddMonths(-3).GetQuarterShort();
                    tMin2Column.HeaderText = today.AddMonths(-6).GetQuarterShort();
                    tMin3Column.HeaderText = today.AddMonths(-9).GetQuarterShort();
                    break;
                case PerformanceInterval.Year:
                    if (today.Month <= 3) today = today.AddMonths(-3);
                    t0Column.HeaderText = today.Year.ToString();
                    tMin1Column.HeaderText = today.AddYears(-1).Year.ToString();
                    tMin2Column.HeaderText = today.AddYears(-2).Year.ToString();
                    tMin3Column.HeaderText = today.AddYears(-3).Year.ToString();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            dgvStocks.SetReadOnly();
            dgvStocks.SetVisualStyling();
            dgvStocks.Visible = true;

            List<StockPerformanceOverviewModel> ShowCurrentOnly(List<StockPerformanceOverviewModel> list) => list.Where(s => !double.IsNaN(s.PerformanceFractionT0)).ToList();
        }

        private bool MultipleStocks() => stockIsins?.Count != 1;

        private void PopulateGraph()
        {
            var period = (Period)cmbPeriod.SelectedItem;
            DateTime from;
            DateTime to;
            switch (period)
            {
                case Period.AllTime:
                    from = DateTime.MinValue;
                    to = DateTime.Today;
                    break;
                case Period.TTM:
                    to = DateTime.Today;
                    from = to.AddDays(-365);
                    break;
                case Period.T3M:
                    to = DateTime.Today;
                    from = to.AddDays(-91);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Unsupported {nameof(Period)} {period}");
            }

            var performanceDto = stockPerformanceService.GetValues(stockIsins, from, to, out PerformanceInterval interval);
            var points = performanceDto.Points;
            if (points.Count == 0)
                return;
            var performance = points.Last().RelativeValue / points.First().RelativeValue - 1;
            var firstDate = points.First().Date;
            var lastDate = points.Last().Date;
            var annualPerformance = GrowthHelper.AnnualPerformance(performance+1, firstDate, lastDate);
            var dataLabelRelativeValue = $"{performance.ToPercentage()} ({(annualPerformance-1).ToPercentage()})";
            var dates = points.Select(p => p.Date).ToArray();
            if (MultipleStocks())
            {
                chart.AddXySeries(SeriesChartType.Column, dates, points.Select(p => p.TotalValue).ToArray(), "Total value");
                chart.RemoveSeries("Number of stocks");
            }
            else
            {
                var qts = TryScaleDownStockQuantity(points.Select(p => p.Quantity).ToList());
                chart.AddXySeries(SeriesChartType.Column, dates, qts, "Number of stocks");
                chart.RemoveSeries("Total value");
            }
            chart.AddXySeries(SeriesChartType.Line, dates, points.Select(p => p.RelativeValue).ToArray(), "RelativeValue", dataLabelRelativeValue);

            var baseReturnPoints = CreateBaseLine();
            var baseSeries = chart.AddXySeries(SeriesChartType.Line, dates, baseReturnPoints.Select(p => p.RelativeValue).ToArray(), "BasePerformance", $"Base ({settings.BaseAnnualPerformance:P0})");
            baseSeries.BorderWidth = 1;

            if (points.Any(p => p.Dividend > 0))
                chart.AddXySeries(SeriesChartType.Stock, dates, points.Select(p => p.Dividend).ToArray(), "Dividend");
            else
                chart.RemoveSeries("Dividend");

            string percOfPortfolio = performanceDto.FractionOfTotalPortfolio == 1 ? "" : $"{performanceDto.FractionOfTotalPortfolio.ToPercentage()} of (selected) portfolio";
            lblPeriod.Text = $"{PeriodText()}  ({interval} interval)  {percOfPortfolio}";

            List<ValuePointDto> CreateBaseLine()
            {
                var basePoints = new List<ValuePointDto>(points.Count);
                basePoints.Add(new ValuePointDto(points[0].RelativeValue, points[0].Date));

                foreach (var point in points.Skip(1))
                    basePoints.Add(new ValuePointDto(points[0].RelativeValue * GrowthHelper.ExpectedPerformance(1 + settings.BaseAnnualPerformance, points[0].Date, point.Date), point.Date));

                return basePoints;
            }

            string PeriodText()
            {
                var nDays = (int)(lastDate - firstDate).TotalDays + 1;
                if (nDays <= 31) return $"Period: {nDays} days";
                var nWeeks = Math.Round(nDays / 7d);
                if (nWeeks < 20) return $"Period: {nWeeks} weeks";
                var nMonths = (int)Math.Round(nDays / 30d);
                if (nMonths <= 12) return $"Period: {nMonths} months";
                return $"Period: {nMonths / 12} years and {nMonths % 12} months";
            }

            // scale down quantities when they are more than 100% to get a better ratio with performance graph
            double[] TryScaleDownStockQuantity(List<double> qts)
            {
                var max = qts.Max();
                double ratio;
                if (max > 10_000) ratio = 200;
                else if (max > 3000) ratio = 50;
                else if (max > 1000) ratio = 20;
                else if (max > 200) ratio = 10;
                else return qts.ToArray();

                for (int i = 0; i < qts.Count; i++)
                    qts[i] /= ratio;

                return qts.ToArray();
            }
        }

        private void cmbPeriod_SelectionChangeCommitted(object sender, EventArgs e) => PopulateGraph();

        private void btnSelect_Click(object sender, EventArgs e)
        {
            using var form = CastleContainer.Resolve<Input.frmStockSelection>();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                stockIsins = form.Stocks.Any() ? form.Stocks.Select(s => s.Isin).ToList() : null;
                stockNames = form.Stocks.Any() ? form.Stocks.Select(s => s.Name).ToList() : null;
                PopulateGraph();
                PopulateStockGrid();
            }
        }

        private void dgvStocks_SelectionChanged(object sender, EventArgs e) => dgvStocks.ClearSelection();

        private void dgvStocks_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var currencyRowIndexes = new []{1, 2, 3, 4, 5};
            if (e.ColumnIndex >= 3 && e.ColumnIndex <= 6 && currencyRowIndexes.Contains(e.RowIndex))
                dgvStocks[e.ColumnIndex, e.RowIndex].Style.Format = "C0";
            if (dgvStocks.Columns[e.ColumnIndex].Name == nameof(StockPerformanceOverviewModel.PerformanceFractionT0))
                dgvStocks[e.ColumnIndex, e.RowIndex].ShowRedAtNegativeValue();
            if (dgvStocks.Columns[e.ColumnIndex].Name == nameof(StockPerformanceOverviewModel.PerformanceFractionTMin1))
                dgvStocks[e.ColumnIndex, e.RowIndex].ShowRedAtNegativeValue();
            if (dgvStocks.Columns[e.ColumnIndex].Name == nameof(StockPerformanceOverviewModel.PerformanceFractionTMin2))
                dgvStocks[e.ColumnIndex, e.RowIndex].ShowRedAtNegativeValue();
            if (dgvStocks.Columns[e.ColumnIndex].Name == nameof(StockPerformanceOverviewModel.PerformanceFractionTMin3))
                dgvStocks[e.ColumnIndex, e.RowIndex].ShowRedAtNegativeValue();
        }
    }

    enum Period
    {
        AllTime,
        /// <summary>
        /// Trailing twelve months
        /// </summary>
        TTM,
        /// <summary>
        /// Trailing three months
        /// </summary>
        T3M,
    }
}
