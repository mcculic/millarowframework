using System;
using System.Globalization;

namespace Millarow.Rest.Serialization.Formatters
{
    public class FormattableValueFormatter<T> : TypedValueFormatter<T>
        where T : IFormattable
    {
        public FormattableValueFormatter(string format)
        {
            Format = format;
        }

        public override string Serialize(T value, CultureInfo cultureInfo)
        {
            return value?.ToString(Format, cultureInfo);
        }

        public string Format { get; }
    }
}
