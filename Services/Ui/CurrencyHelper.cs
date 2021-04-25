using Core;

namespace Services.Ui
{
    public static class CurrencyHelper
    {
        public static string FormatUserCurrency(this double value, bool rounded = true) =>
            value.FormatCurrency(Constants.UserCurrencySymbol, rounded);

        public static string FormatCurrency(this double value, string currencySymbol, bool rounded = true)
        {
            if (rounded)
                return $"{currencySymbol}{value:#,###}";
            return $"{currencySymbol}{value:#,###.##}";
        }
    }
}