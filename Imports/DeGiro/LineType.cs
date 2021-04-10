namespace Imports.DeGiro
{
    public enum LineType
    {
        None,
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