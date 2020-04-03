using System;

namespace Millarow
{
    public static class Maybe
    {
        public static Maybe<T> Just<T>(T value)
        {
            return new Maybe<T>(true, value);
        }

        public static Maybe<T> If<T>(bool condition, Func<T> valueFactory)
        {
            if (condition)
                return valueFactory();

            return Nothing<T>();
        }

        public static Maybe<T> Nothing<T>()
        {
            return new Maybe<T>();
        }

        public static T? ToNullable<T>(this Maybe<T> value) where T : struct
        {
            return value.HasValue ? value.Value : new T?();
        }

        public static Maybe<T> ToMaybe<T>(this T? value) where T : struct
        {
            return value.HasValue ? Just(value.Value) : Nothing<T>();
        }
    }
}