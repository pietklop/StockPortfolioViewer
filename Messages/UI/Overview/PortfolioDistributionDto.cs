using System.Collections.Generic;
using System.Linq;

namespace Messages.UI.Overview
{
    public class PortfolioDistributionDto
    {
        public string Name { get; }
        /// <summary>
        /// Shows the label with the fraction in brackets, e.g. "USA (50%)"
        /// </summary>
        public string[] LabelsWithFractions { get; }
        public string[] Labels { get; set; }
        public double[] Fractions { get; }

        public PortfolioDistributionDto(string name, string[] labels, double[] values, bool removeBelowOnePercent = false)
        {
            Name = name;
            var labelWithFractionList = new List<string>();
            var labelList = new List<string>();
            var total = values.Sum();
            var fractions = new List<double>();
            for (int i = 0; i < values.Length; i++)
            {
                var fraction = values[i] / total;
                if (!removeBelowOnePercent || fraction > 0.005)
                {
                    var label = labels[i];
                    labelList.Add(label);
                    var labelWithFraction = $"{label} ({fraction:P0})";
                    labelWithFractionList.Add(labelWithFraction);
                    fractions.Add(fraction);
                }
            }

            Labels = labelList.ToArray();
            LabelsWithFractions = labelWithFractionList.ToArray();
            Fractions = fractions.ToArray();
        }
    }
}