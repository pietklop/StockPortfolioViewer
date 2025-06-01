namespace Services.Helpers;

public static class PercentageHelper
{
    public static string ToPercentage(this double value)
    {
        if (value >= 0.2)
            return $"{value*100:F0}%";
        return $"{value*100:F1}%";
    }
}