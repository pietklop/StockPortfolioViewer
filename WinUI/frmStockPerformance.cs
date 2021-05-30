﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Dashboard.Helpers;
using Services.Helpers;
using Services.Ui;

namespace Dashboard
{
    public partial class frmStockPerformance : Form
    {
        private readonly StockPerformanceService stockPerformanceService;
        private readonly string stockIsin;

        public frmStockPerformance(StockPerformanceService stockPerformanceService, string stockIsin = null)
        {
            this.stockPerformanceService = stockPerformanceService;
            this.stockIsin = stockIsin;
            InitializeComponent();
        }

        private void frmStockPerformance_Load(object sender, EventArgs e)
        {
            chart.ConfigXyChart();
            PopulateGraph(stockPerformanceService.GetValues(stockIsin, PerformanceInterval.Month));
        }

        private void PopulateGraph(List<ValuePointDto> points)
        {
            var performance = points.Last().RelativeValue / points.First().RelativeValue-1;
            var annualPerformance = GrowthHelper.AnnualPerformance(performance, points.First().Date, points.Last().Date);
            var dataLabelRelativeValue = $"{performance:P1} ({annualPerformance:P0})";
            chart.AddXySeries(SeriesChartType.Column, points.Select(p => p.Date).ToArray(), points.Select(p => p.Quantity).ToArray(), "Number of stocks");
            chart.AddXySeries(SeriesChartType.Line, points.Select(p => p.Date).ToArray(), points.Select(p => p.RelativeValue).ToArray(), dataLabelRelativeValue);
        }
    }
}
