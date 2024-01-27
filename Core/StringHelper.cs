namespace Core
{
    public static class StringExtensions
    {
        public static bool HasValue(this string txt) => !string.IsNullOrEmpty(txt);
    }
}