namespace Imports.DeGiro
{
    public enum LineType
    {
        Unknown,
        Buy,
        Sell,
        CurrencyConversion,
        Dividend,
        DividendTax,
        /// <summary>
        /// Costs for eg dividend
        /// </summary>
        CorporateActionCosts,
        TransactionCosts,
        Na,
    }
}