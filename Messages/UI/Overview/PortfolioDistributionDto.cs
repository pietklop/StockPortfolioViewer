using System.Linq;

namespace Messages.UI.Overview
{
    public class PortfolioDistributionDto
    {
        public string Name { get; }
        public string[] Labels { get; }
        public double[] Fractions { get; }

        public PortfolioDistributionDto(string name, string[] labels, double[] values)
        {
            Name = name;
            Labels = new string[labels.Length];
            var total = values.Sum();
            var fractions = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                fractions[i] = values[i] / total;
                Labels[i] += $"{labels[i]} ({fractions[i]:P0})";
            }

            Fractions = fractions.ToArray();
        }
    }
}