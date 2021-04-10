using System;
using System.Linq;
using Core;

namespace DAL.Entities
{
    public static class CurrencyHelper
    {
        public static Currency GetUserCurrency(this StockDbContext db)
            => db.Currencies.Single(c => c.Key == Constants.UserCurrency);

        public static double ToUserCurrency(this double value, double ratio, string otherCurrency)
        {
            if (otherCurrency == Constants.UserCurrency) return value;
            return ToUserCurrency(value, ratio);
        }

        public static double ToUserCurrency(this double value, double ratio)
        {
            if (ratio <= 0) throw new Exception($"Ratio should be larger than 0");
            return Math.Round(value / ratio, 2);
        }

        public static double ToOtherCurrency(this double value, double ratio, string otherCurrency)
        {
            if (otherCurrency == Constants.UserCurrency) return value;
            return ToOtherCurrency(value, ratio);
        }

        public static double ToOtherCurrency(this double value, double ratio)
        {
            if (ratio <= 0) throw new Exception($"Ratio should be larger than 0");
            return Math.Round(value * ratio);
        }
    }
}