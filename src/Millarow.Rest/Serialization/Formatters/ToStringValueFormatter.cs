using System;
using System.Globalization;

namespace Millarow.Rest.Serialization.Formatters
{
    public sealed class ToStringValueFormatter : IRestValueFormatter
    {
        private ToStringValueFormatter()
        {
        }

        public bool CanSerialize(RestValue value)
        {
            return true;
        }

        public string Serialize(RestValue value, CultureInfo cultureInfo)
        {
            var v = value.Value;

            if (v == null)
                return null;
            else if (v is string s)
                return s;
            else if (v is IFormattable formattable)
                return formattable.ToString(null, cultureInfo);

            return v.ToString();
        }

        public static ToStringValueFormatter Instance { get; } = new ToStringValueFormatter();
    }
}
