using System;

namespace Millarow.Arithmetic.Internal
{
    internal abstract class Arithmetic<T> : IGenericArithmetic<T>
        where T : IEquatable<T>, IComparable<T>
    {
        public bool IsZero(T value) => value.CompareTo(Zero) == 0;

        public bool IsSmaller(T left, T right) => left.CompareTo(right) < 0;

        public bool IsSmallerOrEqual(T left, T right) => left.CompareTo(right) <= 0;

        public bool IsEqual(T left, T right) => left.CompareTo(right) == 0;

        public bool IsGreater(T left, T right) => left.CompareTo(right) > 0;

        public bool IsGreaterOrEqual(T left, T right) => left.CompareTo(right) >= 0;

        public T Negate(T value) => Multiply(value, NegativeOne);

        public T Abs(T v) => v.CompareTo(Zero) < 0 ? Negate(v) : v;

        public T Lerp(T from, T to, double offset)
        {
            var fromD = ToDouble(from);
            var toD = ToDouble(to);

            return FromDouble(fromD + offset * (toD - fromD));
        }

        public T ClampMin(T value, T min)
        {
            if (IsSmaller(value, min))
                return min;

            return value;
        }

        public T ClampMax(T value, T max)
        {
            if (IsGreater(value, max))
                return max;

            return value;
        }

        public T Clamp(T value, T min, T max)
        {
            if (IsSmaller(value, min))
                return min;

            if (IsGreater(value, max))
                return max;

            return value;
        }

        protected Exception CreatePropertyNotSupportedException(string propertyName)
        {
            return new NotSupportedException($"{GetType().Name} does not support property '{propertyName}'");
        }

        public abstract double ToDouble(T value);

        public abstract T Add(T a, T b);

        public abstract T Multiply(T a, T b);

        public abstract T Subtract(T a, T b);

        public abstract T Divide(T value, T divider);

        public abstract T Min(T a, T b);

        public abstract T Max(T a, T b);

        public abstract T FromDouble(double value);

        public abstract T MinValue { get; }

        public abstract T MaxValue { get; }

        public abstract T Zero { get; }

        public abstract T One { get; }

        public abstract T NegativeOne { get; }

        public T Two => Add(One, One);
    }
}