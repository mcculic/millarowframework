using System.Globalization;

namespace Millarow.Rest.Serialization
{
    public abstract class RequestRouteValueFormatter
    {
        public abstract bool CanSerialize(RestValue value);

        public abstract string Serialize(RestValue value, CultureInfo cultureInfo);
    }
}
