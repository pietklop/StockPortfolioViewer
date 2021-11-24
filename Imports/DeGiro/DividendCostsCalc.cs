using System;
using System.Collections.Generic;
using Core;

namespace Imports.DeGiro
{
    public static class DividendCostsCalc
    {
        public static void EnrichDividend(DividendDto dividend, List<CurrencyConvert> currConversions)
        {
            double divTaxRate = 0.15;
            var divWithoutTax = dividend.Value - dividend.Tax;
            double oneEuro = 1;
            if (dividend.Currency != Constants.UserCurrency)
            {
                dividend.CurrencyRatio = currConversions.GetClosestBy(dividend.Currency, dividend.TimeStamp).RatioPerUserCurr;
                oneEuro *= dividend.CurrencyRatio;
            }

            if (dividend.IsCapitalReturn) return;
            if (dividend.Tax == 0) dividend.Tax = divWithoutTax / (1 - divTaxRate) * divTaxRate;
            dividend.Costs = Math.Round(Math.Min(divWithoutTax * 0.10, oneEuro + divWithoutTax * 0.03), 2);
        }
    }
}