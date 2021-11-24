namespace Imports.DeGiro
{
    public enum LineType
    {
        Unknown,
        Buy,
        Sell,
        /// <summary>
        /// Sort of dividend without tax
        /// </summary>
        CapitalReturn,
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