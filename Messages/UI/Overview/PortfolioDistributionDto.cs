using System;
using System.Collections.Generic;
using System.Linq;

namespace Messages.UI.Overview
{
    public class PortfolioDistributionDto
    {
        public string Name { get; }
        public string[] Labels { get; }
        public double[] Fractions { get; }

        public PortfolioDistributionDto(string name, string[] labels, double[] values, bool removeBelowOnePercent = false)
        {
            Name = name;
            var labelList = new List<string>();
            var total = values.Sum();
            var fractions = new List<double>();
            for (int i = 0; i < values.Length; i++)
            {
                var fraction = values[i] / total;
                if (!removeBelowOnePercent || fraction > 0.005)
                {
                    labelList.Add($"{labels[i]} ({fraction:P0})");
                    fractions.Add(fraction);
                }
            }

            Labels = labelList.ToArray();
            Fractions = fractions.ToArray();
        }
    }
}