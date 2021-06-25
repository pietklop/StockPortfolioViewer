using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Core;
using Dashboard.Helpers;
using Services.Helpers;
using Services.Ui;

namespace Dashboard
{
    public partial class frmStockPerformance : Form
    {
        private readonly Settings settings;
        private readonly StockPerformanceService stockPerformanceService;
        private readonly string stockIsin;

        public frmStockPerformance(Settings settings, StockPerformanceService stockPerformanceService, string stockIsin = null)
        {
            this.settings = settings;
            this.stockPerformanceService = stockPerformanceService;
            this.stockIsin = stockIsin;
            InitializeComponent();
        }

        private void frmStockPerformance_Load(object sender, EventArgs e)
        {
            cmbPeriod.DataSource = Enum.GetValues(typeof(Period));
            cmbPeriod.SelectedItem = Period.AllTime;
            chart.ConfigXyChart();
            PopulateGraph();
        }

        private bool MultipleStocks() => stockIsin == null;

        private void PopulateGraph()
        {
            var period = (Period) cmbPeriod.SelectedItem;
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

            var points = stockPerformanceService.GetValues(stockIsin, from, to, out PerformanceInterval interval);
            var performance = points.Last().RelativeValue / points.First().RelativeValue-1;
            var firstDate = points.First().Date;
            var lastDate = points.Last().Date;
            var annualPerformance = GrowthHelper.AnnualPerformance(performance, firstDate, lastDate);
            var dataLabelRelativeValue = $"{performance:P1} ({annualPerformance:P0})";
            var dates = points.Select(p => p.Date).ToArray();
            if (MultipleStocks())
                chart.AddXySeries(SeriesChartType.Column, dates, points.Select(p => p.TotalValue).ToArray(), "Total value");
            else
                chart.AddXySeries(SeriesChartType.Column, dates, points.Select(p => p.Quantity).ToArray(), "Number of stocks");
            chart.AddXySeries(SeriesChartType.Line, dates, points.Select(p => p.RelativeValue).ToArray(), "RelativeValue", dataLabelRelativeValue);

            var baseReturnPoints = CreateBaseLine();
            var baseSeries = chart.AddXySeries(SeriesChartType.Line, dates, baseReturnPoints.Select(p => p.RelativeValue).ToArray(), "BasePerformance", $"Base ({settings.BaseAnnualPerformance:P0})");
            baseSeries.BorderWidth = 1;
            
            if (points.Any(p => p.Dividend > 0))
                chart.AddXySeries(SeriesChartType.Stock, dates, points.Select(p => p.Dividend).ToArray(), "Dividend");

            lblPeriod.Text = $"{PeriodText()}  ({interval} interval)";

            List<ValuePointDto> CreateBaseLine()
            {
                var basePoints = new List<ValuePointDto>(points.Count);
                basePoints.Add(new ValuePointDto(points[0].RelativeValue, points[0].Date));

                foreach (var point in points.Skip(1))
                    basePoints.Add(new ValuePointDto(points[0].RelativeValue * GrowthHelper.ExpectedPerformance(1+settings.BaseAnnualPerformance, points[0].Date, point.Date), point.Date));

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
        }

        private void cmbPeriod_SelectionChangeCommitted(object sender, EventArgs e)
        {
            PopulateGraph();
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
