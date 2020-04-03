using Millarow.Arithmetic;
using System;

namespace Millarow.Geometry
{
    [Serializable]
    public struct Point<T> : IVector<T>, IEquatable<Point<T>>
        where T : struct, IFormattable
    {
        private static readonly IGenericArithmetic<T> _arithmetic = GenericArithmetic.Get<T>();

        [System.Diagnostics.DebuggerStepThrough]
        public Point(T x, T y)
        {
            X = x;
            Y = y;
        }

        public double GetDistance(Point<T> pt)
        {
            var dx = _arithmetic.ToDouble(_arithmetic.Subtract(pt.X, X));
            var dy = _arithmetic.ToDouble(_arithmetic.Subtract(pt.Y, Y));

            return Math.Sqrt(dx * dx + dy * dy);
        }

        public Point<T> Add(T dx, T dy)
        {
            return new Point<T>(_arithmetic.Add(X, dx), _arithmetic.Add(Y, dy));
        }

        public Point<T> Add(IVector<T> vector)
        {
            return Add(vector.X, vector.Y);
        }

        public Point<T> Multiply(T mx, T my)
        {
            return new Point<T>(_arithmetic.Multiply(X, mx), _arithmetic.Multiply(Y, my));
        }

        public Point<T> Rotate(Angle angle, T centerX, T centerY)
        {
            var dx = _arithmetic.ToDouble(_arithmetic.Subtract(X, centerX));
            var dy = _arithmetic.ToDouble(_arithmetic.Subtract(Y, centerY));

            var cos = Math.Cos(angle.Radians);
            var sin = Math.Sin(angle.Radians);

            var x = _arithmetic.FromDouble(cos * dx - sin * dy + _arithmetic.ToDouble(centerX));
            var y = _arithmetic.FromDouble(sin * dx + cos * dy + _arithmetic.ToDouble(centerY));

            return new Point<T>(x, y);
        }

        public Point<T> Rotate(Angle angle, Point<T> center)
        {
            return Rotate(angle, center.X, center.Y);
        }

        public Point<T> Lerp(Point<T> to, double pos)
        {
            return new Point<T>(_arithmetic.Lerp(X, to.X, pos), _arithmetic.Lerp(Y, to.Y, pos));
        }

        public Point<T> Negate()
        {
            return new Point<T>(_arithmetic.Negate(X), _arithmetic.Negate(Y));
        }

        public Point<T> Clamp(Point<T> min, Point<T> max)
        {
            var x = _arithmetic.Clamp(X, min.X, max.X);
            var y = _arithmetic.Clamp(Y, min.Y, max.Y);

            return new Point<T>(x, y);
        }

        public Point<double> ToDouble()
        {
            return new Point<double>(_arithmetic.ToDouble(X), _arithmetic.ToDouble(Y));
        }

        public Angle GetAngle(Point<T> pt)
        {
            var dx = _arithmetic.ToDouble(_arithmetic.Subtract(X, pt.X));
            var dy = _arithmetic.ToDouble(_arithmetic.Subtract(Y, pt.Y));

            return Angle.FromDegree((Math.Atan2(dy, dx)) * 180.0 / Math.PI);
        }

        public override string ToString()
        {
            return $"X={X} Y={Y}";
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return $"X={X.ToString(format, provider)} Y={Y.ToString(format, provider)}";
        }

        public bool Equals(Point<T> other)
        {
            return other.X.Equals(X) && other.Y.Equals(Y);
        }

        public override bool Equals(object obj)
        {
            return obj is Point<T> ? Equals((Point<T>)obj) : false;
        }

        public override int GetHashCode()
        {
            return HashCode.Oat.Get(X, Y);
        }

        public T X { get; }

        public T Y { get; }

        public bool IsZero => _arithmetic.IsZero(X) && _arithmetic.IsZero(Y);

        public static Point<T> Zero { get; } = new Point<T>(_arithmetic.Zero, _arithmetic.Zero);

        public static Point<T> Min { get; } = new Point<T>(_arithmetic.MinValue, _arithmetic.MinValue);

        public static Point<T> Max { get; } = new Point<T>(_arithmetic.MaxValue, _arithmetic.MaxValue);

        public static bool operator ==(Point<T> pt1, Point<T> pt2) => pt1.Equals(pt2);

        public static bool operator !=(Point<T> pt1, Point<T> pt2) => !pt1.Equals(pt2);

        public static Point<T> operator +(Point<T> pt, T m) => new Point<T>(_arithmetic.Add(pt.X, m), _arithmetic.Add(pt.Y, m));

        public static Point<T> operator +(Point<T> pt, IVector<T> m) => new Point<T>(_arithmetic.Add(pt.X, m.X), _arithmetic.Add(pt.Y, m.Y));

        public static Point<T> operator -(Point<T> pt) => new Point<T>(_arithmetic.Negate(pt.X), _arithmetic.Negate(pt.Y));

        public static Point<T> operator -(Point<T> pt, T m) => new Point<T>(_arithmetic.Subtract(pt.X, m), _arithmetic.Subtract(pt.Y, m));

        public static Point<T> operator -(Point<T> pt, IVector<T> m) => new Point<T>(_arithmetic.Subtract(pt.X, m.X), _arithmetic.Subtract(pt.Y, m.Y));

        public static Point<T> operator *(Point<T> pt, T m) => pt.Multiply(m, m);

        public static Point<T> operator *(Point<T> pt, IVector<T> m) => pt.Multiply(m.X, m.Y);

        public static Point<T> operator /(Point<T> pt, T m) => new Point<T>(_arithmetic.Divide(pt.X, m), _arithmetic.Divide(pt.Y, m));

        public static Point<T> operator /(Point<T> pt, IVector<T> m) => new Point<T>(_arithmetic.Divide(pt.X, m.X), _arithmetic.Divide(pt.Y, m.Y));
    }
}