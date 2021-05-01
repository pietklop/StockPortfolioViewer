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
            chart.Legends[0].Enabled = true;
            chart.Legends[0].Font = new Font(chart.Legends[0].Font.FontFamily, 10f);
            chart.Legends[0].ForeColor = Color.Gainsboro;
            chart.Legends[0].BackColor = Color.Transparent;
            chart.BackColor = Color.Transparent;
            chart.ChartAreas[0].BackColor = Color.Transparent;
            chart.ChartAreas[0].Visible = true;
        }

        public static void PopulateChart(Chart chart, string[] labels, double[] values) =>
            chart.Series[0].Points.DataBindXY(labels, values);
    }
}