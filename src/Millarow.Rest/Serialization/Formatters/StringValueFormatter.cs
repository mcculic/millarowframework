using System.Globalization;

namespace Millarow.Rest.Serialization.Formatters
{
    public sealed class StringValueFormatter : TypedValueFormatter<string>
    {
        public StringValueFormatter()
        {
        }

        public override string Serialize(string value, CultureInfo cultureInfo)
        {
            return value;
        }

        public static StringValueFormatter Instance { get; } = new StringValueFormatter();
    }
}
