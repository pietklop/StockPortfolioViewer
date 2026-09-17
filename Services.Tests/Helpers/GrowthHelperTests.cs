using Services.Helpers;
using Shouldly;
using Xunit;

namespace Services.Tests.Helpers
{
    public class GrowthHelperTests
    {
        [Theory]
        // bought and sold within the period, the invested amount is what was bought
        [InlineData(0, 200, 1000, 1200, 0.2)]      // sold with a profit
        [InlineData(0, -200, 1000, 800, -0.2)]     // sold with a loss
        [InlineData(0, 0, 1000, 1000, 0)]          // sold at cost
        // bought during the period and still owned at the end
        [InlineData(0, 150, 1000, 0, 0.15)]
        // no position at all during the period
        [InlineData(0, 0, 0, 0, 0)]
        // owned at the start of the period
        [InlineData(1000, 200, 0, 1200, 0.2)]      // sold during the period
        [InlineData(1000, 100, 0, 0, 0.1)]         // kept during the period
        [InlineData(1000, 220, 1000, 0, 0.1466666666666667)] // bought extra, counts for half the period
        public void GlobalAnnualPerformance(double startValue, double gainedInclDiv, double bought, double sold, double expected)
        {
            var performance = GrowthHelper.GlobalAnnualPerformance(startValue, gainedInclDiv, bought, sold);

            performance.ShouldBe(expected, 1e-9);
        }
    }
}
