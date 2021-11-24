namespace DAL.Entities
{
    public static class DividendHelper
    {
        public static bool IsCapitalReturn(this Dividend dividend) => dividend.UserTax == 0;
    }
}