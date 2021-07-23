using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace Dashboard.Helpers
{
    public static class ChartHelper
    {
        public static void ConfigPieChart(this Chart chart)
        {
            chart.Series[0].ChartType = SeriesChartType.Pie;
            chart.Series[0]["PieLabelStyle"] = "Disabled"; // hide the in-chart labels
        
            ConfigChart(chart);
        }

        public static void ConfigXyChart(this Chart chart)
        {
            chart.ChartAreas[0].AxisX.LineColor = Color.Gainsboro;
            chart.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.Gainsboro;

            chart.ChartAreas[0].AxisY.LineColor = Color.Gainsboro;
            chart.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.Gainsboro;

            ConfigChart(chart);
        }

        private static void ConfigChart(this Chart chart)
        {
            chart.Legends[0].Enabled = true;
            chart.Legends[0].Font = new Font(chart.Legends[0].Font.FontFamily, 10f);
            chart.Legends[0].ForeColor = Color.Gainsboro;
            chart.Legends[0].BackColor = Color.Transparent;
            chart.BackColor = Color.Transparent;
            chart.ChartAreas[0].BackColor = Color.Transparent;
            chart.ChartAreas[0].Visible = true;
        }

        public static void PopulatePieChart(this Chart chart, string[] labels, double[] values) =>
            chart.Series[0].Points.DataBindXY(labels, values);

        public static Series AddXySeries(this Chart chart, SeriesChartType chartType, DateTime[] labels, double[] values, string name, string legendText = null)
        {
            Series series;
            if (chart.Series[0].Name == "Series1")
                series = chart.Series[0];
            else if (chart.Series.Any(s => s.Name == name))
                series = chart.Series.Single(s => s.Name == name);
            else
            {
                series = new Series(name);
                chart.Series.Add(series);
            }
            series.ChartType = chartType;
            series.BorderWidth = 3; // Width of the plotted line
            series.Points.DataBindXY(labels, values);
            series.Name = name;
            series.LegendText = legendText ?? name;

            return series;
        }

        public static void RemoveSeries(this Chart chart, string name) => chart.Series.Remove(chart.Series.SingleOrDefault(s => s.Name == name));
    }
}