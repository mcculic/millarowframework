using Millarow.Arithmetic;
using System;

namespace Millarow.Geometry
{
    [Serializable]
    public struct Line<T> : IEquatable<Line<T>>
        where T : struct, IFormattable
    {
        private static readonly IGenericArithmetic<T> _arithmetic = GenericArithmetic.Get<T>();

        public Line(T x1, T y1, T x2, T y2)
        {
            X1 = x1;
            Y1 = y1;
            X2 = x2;
            Y2 = y2;
        }

        public Line(Point<T> start, Point<T> end)
            : this(start.X, start.Y, end.X, end.Y)
        {
        }

        public Line<T> Offset(T dx, T dy)
        {
            return new Line<T>(_arithmetic.Add(X1, dx), _arithmetic.Add(Y1, dy), _arithmetic.Add(X2, dx), _arithmetic.Add(Y2, dy));
        }

        public Line<T> Offset(IVector<T> v)
        {
            return new Line<T>(_arithmetic.Add(X1, v.X), _arithmetic.Add(Y1, v.Y), _arithmetic.Add(X2, v.X), _arithmetic.Add(Y2, v.Y));
        }

        public Line<T> Normalize()
        {
            var x1 = _arithmetic.Min(X1, X2);
            var y1 = _arithmetic.Min(Y1, Y2);
            var x2 = _arithmetic.Max(X1, X2);
            var y2 = _arithmetic.Max(Y1, Y2);

            return new Line<T>(x1, y1, x2, y2);
        }

        public Line<T> Rotate(Angle angle, Point<T> center)
        {
            var start = Start.Rotate(angle, center);
            var end = End.Rotate(angle, center);

            return new Line<T>(start, end);
        }

        public Line<T> Rotate(Angle angle)
        {
            return Rotate(angle, Center);
        }

        public Line<double> ToDouble()
        {
            return new Line<double>(Start.ToDouble(), End.ToDouble());
        }

        public override string ToString()
        {
            return $"X1={X1} Y1={Y1} X2={X2} Y2={Y2}";
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return $"X1={X1.ToString(format, provider)} Y1={Y1.ToString(format, provider)} X2={X2.ToString(format, provider)} Y2={Y2.ToString(format, provider)}";
        }

        public bool Equals(Line<T> other)
        {
            return other.X1.Equals(X1) && other.Y1.Equals(Y1) && other.X2.Equals(X1) && other.Y2.Equals(Y2);
        }

        public override bool Equals(object obj)
        {
            return obj is Line<T> ? Equals((Line<T>)obj) : false;
        }

        public override int GetHashCode()
        {
            return HashCode.Oat.Get(X1, Y1, X2, Y2);
        }

        public T X1 { get; }

        public T Y1 { get; }

        public T X2 { get; }

        public T Y2 { get; }

        public T Width => _arithmetic.Subtract(X2, X1);

        public T Height => _arithmetic.Subtract(Y2, Y1);

        public bool IsNormalized => _arithmetic.IsSmallerOrEqual(X1, X2) && _arithmetic.IsSmallerOrEqual(Y1, Y2);

        public Point<T> Start => new Point<T>(X1, Y1);

        public Point<T> End => new Point<T>(X2, Y2);

        public Size<T> Size => new Size<T>(Width, Height);

        public Rectangle<T> Bounds => new Rectangle<T>(Start, Size);

        public Point<T> Center
        {
            get
            {
                var x = _arithmetic.Add(X1, _arithmetic.Divide(Width, _arithmetic.FromDouble(2)));
                var y = _arithmetic.Add(Y1, _arithmetic.Divide(Height, _arithmetic.FromDouble(2)));

                return new Point<T>(x, y);
            }
        }

        public Angle Angle => Start.GetAngle(End);
    }
}