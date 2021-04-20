namespace Services.Helpers
{
    public static class StringHelper
    {
        public static string MaxLength(this string data, int maxLength) =>
            data.Length <= maxLength ? data : $"{data.Substring(0, maxLength - 2)}..";
    }
}