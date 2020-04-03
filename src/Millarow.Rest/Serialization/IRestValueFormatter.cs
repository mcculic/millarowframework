using System.Globalization;

namespace Millarow.Rest.Serialization
{
    public interface IRestValueFormatter
    {
        bool CanSerialize(RestValue value);

        string Serialize(RestValue value, CultureInfo cultureInfo);
    }
}
