using System;

namespace Millarow.Rest
{
    public sealed class RestValue
    {
        public RestValue(object value, Type type)
        {
            type.AssertNotNull(nameof(type));

            if (value == null)
            {
                if (type.IsValueType && Nullable.GetUnderlyingType(type) == null)
                    throw new ArgumentException(nameof(type)); //TODO msg
            }
            else if (!type.IsInstanceOfType(value))
                throw new ArgumentException(nameof(type)); //TODO msg

            Value = value;
            Type = type;
        }

        public object Value { get; }

        public Type Type { get; }

        public bool IsNull => Value == null;

        public static RestValue Create<T>(T value)
            => new RestValue(value, typeof(T));
    }
}
