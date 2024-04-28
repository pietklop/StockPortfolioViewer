using System;
using System.ComponentModel;

namespace Messages.UI.Overview
{
    public class StockPerformanceOverviewModel
    {
        [ColumnCellsUnderline]
        public string Name { get; set; }
        [ColumnHide]
        public string Isin { get; set; }
        [Description("Total value of this currently owned stock")]
        [DisplayFormat("C0")]
        [ColumnHide]
        public double Value { get; set; }
        [DisplayFormat("P1")]
        [Description("(Weighed) Performance")]
        public double PerformanceFractionTMin3 { get; set; }
        [DisplayFormat("P1")]
        [Description("(Weighed) Performance")]
        public double PerformanceFractionTMin2 { get; set; }
        [DisplayFormat("P1")]
        [Description("(Weighed) Performance")]
        public double PerformanceFractionTMin1 { get; set; }
        [DisplayFormat("P1")]
        [Description("(Weighed) Performance")]
        public double PerformanceFractionT0 { get; set; }
    }

    public static class StockPerformanceOverviewModelHelper
    {
        public static void SetPerformance(this StockPerformanceOverviewModel model, int interval, double value)
        {
            switch (interval)
            {
                case 0:
                    model.PerformanceFractionT0 = value;
                    break;
                case 1:
                    model.PerformanceFractionTMin1 = value;
                    break;
                case 2:
                    model.PerformanceFractionTMin2 = value;
                    break;
                case 3:
                    model.PerformanceFractionTMin3 = value;
                    break;
                default:
                    throw new Exception($"Invalid interval {interval}");
            }
        }
    }
}