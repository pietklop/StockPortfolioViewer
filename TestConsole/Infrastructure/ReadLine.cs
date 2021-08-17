using System;
using System.ComponentModel;

namespace TestConsole.Infrastructure
{
    public static class ReadLine
    {
        public static string ReadString(string prompt, string defaultValue = null)
        {
            Console.WriteLine(prompt);
            if (defaultValue != null) Console.WriteLine($"Default value [{defaultValue}]");
            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) && defaultValue != null) return defaultValue;
            return input;
        }

        public static T ReadValueType<T>(string prompt, object defaultValInput = null, bool persistent = false)
        {
            var type = typeof(T);

            if (Nullable.GetUnderlyingType(type) == null || !type.IsValueType)
                throw new Exception("Only nullable value types are supported");

            Console.WriteLine(prompt);

            var defValReturn = default(T);
            if (defaultValInput != null)
            {
                defValReturn = StringToValueType<T>(defaultValInput.ToString());
                Console.WriteLine($"Default value is [{defValReturn}]");
            }

            var input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) && defaultValInput != null) return defValReturn;

            var returnVal = StringToValueType<T>(input);
            if (returnVal == null && persistent)
                return ReadValueType<T>(prompt, defaultValInput, true);
            return returnVal;
        }

        private static T StringToValueType<T>(string value)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)converter.ConvertFromString(value);
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}