using System.Collections.Generic;
using System.Globalization;

namespace Imports.DeGiro
{
    public static class StringHelper
    {
        public static string[] SmartSplit(this string text, char separator)
        {
            var fields = new List<string>();

            int start = 0;
            bool inQuote = false;
            for (int pos = 0; pos < text.Length; pos++)
            {
                if (!inQuote && text[pos] == separator)
                {
                    fields.Add(text.Substring(start, pos - start).RemoveQuotes());
                    start = pos+1;
                }
                else if (text[pos] == '"')
                    inQuote = !inQuote;
            }
            fields.Add(text.Substring(start, text.Length - start).RemoveQuotes());

            return fields.ToArray();
        }

        public static double ToDouble(this string text) => 
            double.Parse(text.Replace(",", "."), CultureInfo.InvariantCulture);

        private static string RemoveQuotes(this string text)
        {
            if (!text.StartsWith("\"")) return text;

            return text.Substring(1, text.Length - 2);
        }
    }

}