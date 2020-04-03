using Millarow.Arithmetic;
using System;

namespace Millarow.Geometry
{
    [Serializable]
    public struct Thickness<T> : IEquatable<Thickness<T>>
        where T : struct, IFormattable
    {
        private static readonly IGenericArithmetic<T> _arithmetic = GenericArithmetic.Get<T>();

        public Thickness(T left, T top, T right, T bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public Thickness(T uniform)
        {
            Left = uniform;
            Top = uniform;
            Right = uniform;
            Bottom = uniform;
        }

        public Thickness<T> Add(Thickness<T> value)
        {
            var l = _arithmetic.Add(Left, value.Left);
            var t = _arithmetic.Add(Top, value.Top);
            var r = _arithmetic.Add(Right, value.Right);
            var b = _arithmetic.Add(Bottom, value.Bottom);

            return new Thickness<T>(l, t, r, b);
        }

        public Thickness<T> Subtract(Thickness<T> value)
        {
            var l = _arithmetic.Subtract(Left, value.Left);
            var t = _arithmetic.Subtract(Top, value.Top);
            var r = _arithmetic.Subtract(Right, value.Right);
            var b = _arithmetic.Subtract(Bottom, value.Bottom);

            return new Thickness<T>(l, t, r, b);
        }

        public Thickness<T> Divide(T div)
        {
            var l = _arithmetic.Divide(Left, div);
            var t = _arithmetic.Divide(Top, div);
            var r = _arithmetic.Divide(Right, div);
            var b = _arithmetic.Divide(Bottom, div);

            return new Thickness<T>(l, t, r, b);
        }

        public Thickness<double> ToDouble()
        {
            var l = _arithmetic.ToDouble(Left);
            var t = _arithmetic.ToDouble(Top);
            var r = _arithmetic.ToDouble(Right);
            var b = _arithmetic.ToDouble(Bottom);

            return new Thickness<double>(l, t, r, b);
        }

        public override string ToString()
        {
            return $"Left={Left} Top={Top} Right={Right} Bottom={Bottom}";
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return $"Left={Left.ToString(format, provider)} Top={Top.ToString(format, provider)} Right={Right.ToString(format, provider)} Bottom={Bottom.ToString(format, provider)}";
        }

        public bool Equals(Thickness<T> other)
        {
            return other.Left.Equals(Left) 
                && other.Top.Equals(Top) 
                && other.Right.Equals(Right) 
                && other.Bottom.Equals(Bottom);
        }

        public override bool Equals(object obj)
        {
            return obj is Thickness<T> t ? Equals(t) : false;
        }

        public override int GetHashCode()
        {
            return HashCode.Oat.Get(Left, Top, Right, Bottom);
        }

        public T Left { get; }

        public T Top { get; }

        public T Right { get; }

        public T Bottom { get; }

        public T Width => _arithmetic.Add(Left, Right);

        public T Height => _arithmetic.Add(Top, Bottom);

        public Size<T> Size => new Size<T>(Width, Height);

        public Size<T> LeftTop => new Size<T>(Left, Top);

        public Size<T> RightBottom => new Size<T>(Right, Bottom);

        public bool IsZero => _arithmetic.IsZero(Left) && _arithmetic.IsZero(Top) && _arithmetic.IsZero(Right) && _arithmetic.IsZero(Bottom);

        public bool IsUniform => _arithmetic.IsEqual(Left, Top) && _arithmetic.IsEqual(Top, Right) && _arithmetic.IsEqual(Right, Bottom);

        public static Thickness<T> Zero { get; } = new Thickness<T>();

        public static Thickness<T> operator +(Thickness<T> t1, Thickness<T> t2) => t1.Add(t2);

        public static Thickness<T> operator -(Thickness<T> t1, Thickness<T> t2) => t1.Subtract(t2);

        public static Thickness<T> operator -(Thickness<T> t) => new Thickness<T>(_arithmetic.Negate(t.Left), _arithmetic.Negate(t.Top), _arithmetic.Negate(t.Right), _arithmetic.Negate(t.Bottom));

        public static Thickness<T> operator /(Thickness<T> t1, T div) => t1.Divide(div);

        public static implicit operator Thickness<T>(T uniform) => new Thickness<T>(uniform);
    }
}