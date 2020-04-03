namespace Millarow.Arithmetic
{
    public interface IGenericArithmetic<T>
    {
        double ToDouble(T value);
        T FromDouble(double value);

        bool IsZero(T value);
        bool IsSmaller(T left, T right);
        bool IsSmallerOrEqual(T left, T right);
        bool IsEqual(T left, T right);
        bool IsGreater(T left, T right);
        bool IsGreaterOrEqual(T left, T right);

        T Abs(T v);
        T Add(T a, T b);
        T Divide(T value, T divider);
        T Max(T a, T b);
        T Min(T a, T b);
        T Multiply(T a, T b);
        T Negate(T value);
        T Subtract(T a, T b);
        T Lerp(T from, T to, double offset);
        T ClampMin(T value, T min);
        T ClampMax(T value, T max);
        T Clamp(T value, T min, T max);

        T MaxValue { get; }
        T MinValue { get; }
        T NegativeOne { get; }
        T Zero { get; }
        T One { get; }
        T Two { get; }
    }
}