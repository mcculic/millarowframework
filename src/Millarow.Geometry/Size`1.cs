using Millarow.Arithmetic;
using System;

namespace Millarow.Geometry
{
    [Serializable]
    public struct Size<T> : IVector<T>, IEquatable<Size<T>>
        where T : struct, IFormattable
    {
        private static readonly IGenericArithmetic<T> _arithmetic = GenericArithmetic.Get<T>();

        [System.Diagnostics.DebuggerStepThrough]
        public Size(T width, T height)
        {
            Width = width;
            Height = height;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public Size<double> ToDouble()
        {
            return new Size<double>(_arithmetic.ToDouble(Width), _arithmetic.ToDouble(Height));
        }

        public Size<T> Add(T v)
        {
            return new Size<T>(_arithmetic.Add(Width, v), _arithmetic.Add(Height, v));
        }

        public Size<T> Add(T dw, T dh)
        {
            return new Size<T>(_arithmetic.Add(Width, dw), _arithmetic.Add(Height, dh));
        }

        public Size<T> Add(IVector<T> v)
        {
            return Add(v.X, v.Y);
        }

        public Size<T> Subtract(T v)
        {
            return new Size<T>(_arithmetic.Subtract(Width, v), _arithmetic.Subtract(Height, v));
        }

        public Size<T> Subtract(T dw, T dh)
        {
            return new Size<T>(_arithmetic.Subtract(Width, dw), _arithmetic.Subtract(Height, dh));
        }

        public Size<T> Subtract(Size<T> v)
        {
            return Subtract(v.Width, v.Height);
        }

        public Size<T> Multiply(T v)
        {
            return new Size<T>(_arithmetic.Multiply(Width, v), _arithmetic.Multiply(Height, v));
        }

        public Size<T> Multiply(Size<T> v)
        {
            return new Size<T>(_arithmetic.Multiply(Width, v.Width), _arithmetic.Multiply(Height, v.Height));
        }

        public Size<T> Divide(T v)
        {
            return new Size<T>(_arithmetic.Divide(Width, v), _arithmetic.Divide(Height, v));
        }

        public Size<T> Divide(Size<T> v)
        {
            return new Size<T>(_arithmetic.Divide(Width, v.Width), _arithmetic.Divide(Height, v.Height));
        }

        public Size<T> Clamp(Size<T> min, Size<T> max)
        {
            var w = _arithmetic.Clamp(Width, min.Width, max.Width);
            var h = _arithmetic.Clamp(Height, min.Height, max.Height);

            return new Size<T>(w, h);
        }

        public Size<T> Inflate(IVector<T> size)
        {
            var w = _arithmetic.Add(Width, size.X);
            var h = _arithmetic.Add(Height, size.Y);

            return new Size<T>(w, h);
        }

        public Size<T> Deflate(IVector<T> size)
        {
            var w = _arithmetic.Subtract(Width, size.X);
            var h = _arithmetic.Subtract(Height, size.Y);

            return new Size<T>(w, h);
        }

        public Size<T> WithWidth(T width)
        {
            return new Size<T>(width, Height);
        }

        public Size<T> WithHeight(T height)
        {
            return new Size<T>(Width, height);
        }

        public override string ToString()
        {
            return $"Width={Width} Height={Height}";
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return $"Width={Width.ToString(format, provider)} Height={Height.ToString(format, provider)}";
        }

        public bool Equals(Size<T> other)
        {
            return other.Width.Equals(Width) && other.Height.Equals(Height);
        }

        public override bool Equals(object obj)
        {
            return obj is Size<T> ? Equals((Size<T>)obj) : false;
        }

        public override int GetHashCode()
        {
            return HashCode.Oat.Get(Width, Height);
        }

        public T Width { get; }

        public T Height { get; }

        public bool IsZero => _arithmetic.IsZero(Width) && _arithmetic.IsZero(Height);

        public bool IsEmpty => _arithmetic.IsZero(Width) || _arithmetic.IsZero(Height);

        T IVector<T>.X => Width;

        T IVector<T>.Y => Height;

        public static Size<T> Zero { get; } = new Size<T>();
        
        public static Size<T> operator +(Size<T> s, T x) => s.Add(x);

        public static Size<T> operator +(Size<T> s1, IVector<T> s2) => s1.Add(s2);

        public static Size<T> operator -(Size<T> s1, T x) => s1.Subtract(x);

        public static Size<T> operator -(Size<T> s1, Size<T> s2) => s1.Subtract(s2);

        public static Size<T> operator -(Size<T> s) => new Size<T>(_arithmetic.Negate(s.Width), _arithmetic.Negate(s.Height));

        public static Size<T> operator *(Size<T> s, T x) => s.Multiply(x);

        public static Size<T> operator *(Size<T> s1, Size<T> s2) => s1.Multiply(s2);

        public static Size<T> operator /(Size<T> s, T x) => s.Divide(x);

        public static Size<T> operator /(Size<T> s1, Size<T> s2) => s1.Divide(s2);

        public static bool operator ==(Size<T> s1, Size<T> s2) => s1.Equals(s2);

        public static bool operator !=(Size<T> s1, Size<T> s2) => !s1.Equals(s2);
    }
}