using System;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace Dashboard.Helpers
{
    public static class ChartHelper
    {
        public static void ConfigPieChart(Chart chart)
        {
            chart.Series[0].ChartType = SeriesChartType.Pie;
            chart.Series[0]["PieLabelStyle"] = "Disabled"; // hide the in-chart labels
        
            ConfigChart(chart);
        }

        public static void ConfigLineChart(Chart chart)
        {
            chart.Series[0].ChartType = SeriesChartType.Line;
            chart.ChartAreas[0].AxisX.LineColor = Color.Gainsboro;
            chart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Gainsboro;

            chart.ChartAreas[0].AxisY.LineColor = Color.Gainsboro;
            chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Gainsboro;

            ConfigChart(chart);
        }

        private static void ConfigChart(Chart chart)
        {
            chart.Legends[0].Enabled = true;
            chart.Legends[0].Font = new Font(chart.Legends[0].Font.FontFamily, 10f);
            chart.Legends[0].ForeColor = Color.Gainsboro;
            chart.Legends[0].BackColor = Color.Transparent;
            chart.BackColor = Color.Transparent;
            chart.ChartAreas[0].BackColor = Color.Transparent;
            chart.ChartAreas[0].Visible = true;
        }

        public static void PopulatePieChart(Chart chart, string[] labels, double[] values) =>
            chart.Series[0].Points.DataBindXY(labels, values);
        
        public static void PopulateLineChart(Chart chart, DateTime[] labels, double[] values, string dataLabel)
        {
            chart.Series[0].Points.DataBindXY(labels, values);
            chart.Series[0].Name = dataLabel;
        }
    }
}