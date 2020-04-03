using System;
using System.Globalization;

namespace Millarow.Rest.Serialization.Formatters
{
    public abstract class TypedValueFormatter<T> : IRestValueFormatter
    {
        public abstract string Serialize(T value, CultureInfo cultureInfo);

        public virtual bool CanSerialize(T value)
            => true;

        bool IRestValueFormatter.CanSerialize(RestValue value)
            => value.Type == typeof(T) && CanSerialize((T)value.Value);

        string IRestValueFormatter.Serialize(RestValue value, CultureInfo cultureInfo)
        {
            var valueType = value.Type;

            if (!typeof(T).IsAssignableFrom(valueType) || valueType.IsInstanceOfType(value))
                throw new ArgumentException("Invalid value type"); //TODO msg

            return Serialize((T)value.Value, cultureInfo);
        }
    }
}
