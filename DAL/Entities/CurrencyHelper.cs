using System;
using System.Linq;
using Core;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities
{
    public static class CurrencyHelper
    {
        public static Currency GetUser(this DbSet<Currency> currencies)
            => currencies.Get(Constants.UserCurrency);

        public static Currency Get(this DbSet<Currency> currencies, string key)
            => currencies.Single(c => c.Key == key);

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