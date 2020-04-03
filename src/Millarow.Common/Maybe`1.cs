using System;

namespace Millarow
{
    public struct Maybe<T> : IEquatable<Maybe<T>>
    {
        private readonly bool _hasValue;
        private readonly T _value;

        public Maybe(bool hasValue, T value)
        {
            _hasValue = hasValue;
            _value = value;
        }

        public T GetValueOrDefault()
        {
            return _hasValue ? _value : default(T);
        }

        public Maybe<U> As<U>()
        {
            if (_hasValue && _value is U)
                return (U)(object)_value;

            return Maybe.Nothing<U>();
        }

        public Maybe<U> Cast<U>()
        {
            if (_hasValue)
                return (U)(object)_value;

            return Maybe.Nothing<U>();
        }

        public Maybe<U> Bind<U>(Func<T, U> f)
        {
            if (_hasValue)
                return Maybe.Just(f(_value));

            return Maybe.Nothing<U>();
        }

        public bool Equals(Maybe<T> other)
        {
            return other.HasValue == _hasValue && Equals(other._value, _value);
        }

        public override bool Equals(object obj)
        {
            return obj is Maybe<T> && Equals((Maybe<T>)obj);
        }

        public override int GetHashCode()
        {
            var hash = typeof(Maybe<T>).GetHashCode();
            if (_hasValue)
                hash ^= _value.GetHashCode();

            return hash;
        }

        public bool HasValue => _hasValue;

        public T Value
        {
            get
            {
                if (_hasValue)
                    return _value;

                throw new InvalidOperationException();
            }
        }

        public static bool operator ==(Maybe<T> a, Maybe<T> b) => a.Equals(b);

        public static bool operator !=(Maybe<T> a, Maybe<T> b) => !a.Equals(b);

        public static implicit operator Maybe<T>(T value) => new Maybe<T>(true, value);
    }
}
