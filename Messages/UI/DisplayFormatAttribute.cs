using System;

namespace Messages.UI
{
    public class DisplayFormatAttribute : Attribute
    {
        public DisplayFormatAttribute(string format)
        {
            Format = format;
        }
        public string Format { get; set; }
    }
}