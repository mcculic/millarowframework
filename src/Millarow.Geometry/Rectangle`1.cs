using Millarow.Arithmetic;
using System;
using System.Collections.Generic;

namespace Millarow.Geometry
{
    [Serializable]
    public struct Rectangle<T> : IEquatable<Rectangle<T>>
        where T : struct, IFormattable
    {
        private static readonly IGenericArithmetic<T> _arithmetic = GenericArithmetic.Get<T>();

        [System.Diagnostics.DebuggerStepThrough]
        public Rectangle(T left, T top, T width, T height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public Rectangle(Point<T> location, Size<T> size)
            : this(location.X, location.Y, size.Width, size.Height)
        {
        }

        public Rectangle<T> Offset(T dx, T dy)
        {
            return new Rectangle<T>(Location.Add(dx, dy), Size);
        }

        public Rectangle<T> Offset(IVector<T> vector)
        {
            return new Rectangle<T>(Location.Add(vector), Size);
        }

        public Rectangle<T> Inflate(T dx, T dy)
        {
            var l = _arithmetic.Subtract(Left, dx);
            var t = _arithmetic.Subtract(Top, dy);
            var w = _arithmetic.Add(Width, _arithmetic.Add(dx, dx));
            var h = _arithmetic.Add(Height, _arithmetic.Add(dy, dy));

            return new Rectangle<T>(l, t, w, h);
        }

        public Rectangle<T> Inflate(Size<T> size)
        {
            return Inflate(size.Width, size.Height);
        }

        public Rectangle<T> Inflate(Thickness<T> thickness)
        {
            var l = _arithmetic.Subtract(Left, thickness.Left);
            var t = _arithmetic.Subtract(Top, thickness.Top);
            var r = _arithmetic.Add(Right, thickness.Right);
            var b = _arithmetic.Add(Bottom, thickness.Bottom);

            return FromLTRB(l, t, r, b);
        }

        public Rectangle<T> Deflate(T dx, T dy)
        {
            var l = _arithmetic.Add(Left, dx);
            var t = _arithmetic.Add(Top, dy);
            var w = _arithmetic.Subtract(Width, _arithmetic.Add(dx, dx));
            var h = _arithmetic.Subtract(Height, _arithmetic.Add(dy, dy));

            return new Rectangle<T>(l, t, w, h);
        }

        public Rectangle<T> Deflate(Size<T> size)
        {
            return Deflate(size.Width, size.Height);
        }

        public Rectangle<T> Deflate(Thickness<T> thickness)
        {
            var l = _arithmetic.Add(Left, thickness.Left);
            var t = _arithmetic.Add(Top, thickness.Top);
            var r = _arithmetic.Subtract(Right, thickness.Right);
            var b = _arithmetic.Subtract(Bottom, thickness.Bottom);

            return FromLTRB(l, t, r, b);
        }

        public Rectangle<T> Union(Rectangle<T> rect)
        {
            var left = _arithmetic.Min(Left, rect.Left);
            var top = _arithmetic.Min(Top, rect.Top);
            var right = _arithmetic.Max(Right, rect.Right);
            var bottom = _arithmetic.Max(Bottom, rect.Bottom);

            return FromLTRB(left, top, right, bottom);
        }

        public Rectangle<T> Union(Point<T> location, Size<T> size)
        {
            return Union(new Rectangle<T>(location, size));
        }

        public Rectangle<T> Intersect(Rectangle<T> rect)
        {
            var left = _arithmetic.Max(Left, rect.Left);
            var top = _arithmetic.Max(Top, rect.Top);
            var right = _arithmetic.Min(Right, rect.Right);
            var bottom = _arithmetic.Min(Bottom, rect.Bottom);

            if (_arithmetic.IsGreaterOrEqual(right, left) && _arithmetic.IsGreaterOrEqual(bottom, top))
                return FromLTRB(left, top, right, bottom);
            else
                return Empty;
        }

        public Rectangle<T> Intersect(Point<T> location, Size<T> size)
        {
            return Intersect(new Rectangle<T>(location, size));
        }

        public bool IntersectsWith(Rectangle<T> rect)
        {
            return
                _arithmetic.IsSmaller(Left, rect.Right) &&
                _arithmetic.IsSmaller(Top, rect.Bottom) &&
                _arithmetic.IsGreater(Right, rect.Left) &&
                _arithmetic.IsGreater(Bottom, rect.Top);
        }

        public bool IntersectsWith(Point<T> location, Size<T> size)
        {
            return IntersectsWith(new Rectangle<T>(location, size));
        }

        public bool Contains(T x, T y)
        {
            return
                _arithmetic.IsSmallerOrEqual(Left, x) &&
                _arithmetic.IsSmallerOrEqual(Top, y) &&
                _arithmetic.IsGreater(Right, x) &&
                _arithmetic.IsGreater(Bottom, y);
        }

        public bool Contains(Point<T> pt)
        {
            return Contains(pt.X, pt.Y);
        }

        public bool Contains(Rectangle<T> rect)
        {
            return
                _arithmetic.IsSmallerOrEqual(Left, rect.Left) &&
                _arithmetic.IsSmallerOrEqual(Top, rect.Top) &&
                _arithmetic.IsGreaterOrEqual(Right, rect.Right) &&
                _arithmetic.IsGreaterOrEqual(Bottom, rect.Bottom);
        }

        public Rectangle<T> Normalize()
        {
            var l = _arithmetic.Min(Left, Right);
            var t = _arithmetic.Min(Top, Bottom);
            var r = _arithmetic.Max(Left, Right);
            var b = _arithmetic.Max(Top, Bottom);

            return FromLTRB(l, t, r, b);
        }

        public Rectangle<double> ToDouble()
        {
            return new Rectangle<double>(Location.ToDouble(), Size.ToDouble());
        }

        public IEnumerable<Line<T>> EnumerateSides()
        {
            yield return new Line<T>(Left, Top, Right, Top);
            yield return new Line<T>(Right, Top, Right, Bottom);
            yield return new Line<T>(Right, Bottom, Left, Bottom);
            yield return new Line<T>(Left, Bottom, Left, Top);
        }

        public override string ToString()
        {
            return $"Left={Left} Top={Top} Width={Width} Height={Height}";
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return $"Left={Left.ToString(format, provider)} Top={Top.ToString(format, provider)} Width={Width.ToString(format, provider)} Height={Height.ToString(format, provider)}";
        }

        public bool Equals(Rectangle<T> other)
        {
            return Left.Equals(other.Left) && Top.Equals(other.Top) && Width.Equals(other.Width) && Height.Equals(other.Height);
        }

        public override bool Equals(object obj)
        {
            return obj is Rectangle<T> ? Equals((Rectangle<T>)obj) : false;
        }

        public override int GetHashCode()
        {
            return HashCode.Oat.Get(Left, Top, Width, Height);
        }

        public static Rectangle<T> FromLTRB(T left, T top, T right, T bottom)
        {
            var w = _arithmetic.Subtract(right, left);
            var h = _arithmetic.Subtract(bottom, top);

            return new Rectangle<T>(left, top, w, h);
        }

        public static Rectangle<T> FromLTRB(Point<T> lt, Point<T> rb)
        {
            return FromLTRB(lt.X, lt.Y, rb.X, rb.Y);
        }
        
        public static Rectangle<T> Intersert(IEnumerable<Rectangle<T>> rects)
        {
            rects.AssertNotNull(nameof(rects));

            var enumerator = rects.GetEnumerator();
            if (enumerator.MoveNext())
            {
                var ret = enumerator.Current;

                while (enumerator.MoveNext())
                    ret = ret.Intersect(enumerator.Current);

                return ret;
            }

            return Empty;
        }

        public static Rectangle<T> Union(IEnumerable<Rectangle<T>> rects)
        {
            rects.AssertNotNull(nameof(rects));

            var enumerator = rects.GetEnumerator();
            if (enumerator.MoveNext())
            {
                var ret = enumerator.Current;

                while (enumerator.MoveNext())
                    ret = ret.Union(enumerator.Current);

                return ret;
            }

            return Empty;
        }

        public T Left { get; }

        public T Top { get; }

        public T Width { get; }

        public T Height { get; }

        public T Right => _arithmetic.Add(Left, Width);

        public T Bottom => _arithmetic.Add(Top, Height);

        public T Area => _arithmetic.Multiply(Width, Height);

        public bool IsEmpty => _arithmetic.IsZero(Width) || _arithmetic.IsZero(Height);

        public bool IsNormalized => _arithmetic.IsGreaterOrEqual(Right, Left) && _arithmetic.IsGreaterOrEqual(Bottom, Top);

        public Point<T> Location => new Point<T>(Left, Top);

        public Size<T> Size => new Size<T>(Width, Height);

        public Point<T> Center
        {
            get
            {
                var x = _arithmetic.Add(Left, _arithmetic.Divide(Width, _arithmetic.Two));
                var y = _arithmetic.Add(Top, _arithmetic.Divide(Height, _arithmetic.Two));

                return new Point<T>(x, y);
            }
        }

        public static Rectangle<T> Empty { get; } = new Rectangle<T>();

        public static bool operator ==(Rectangle<T> r1, Rectangle<T> r2) => r1.Equals(r2);

        public static bool operator !=(Rectangle<T> r1, Rectangle<T> r2) => !r1.Equals(r2);

        public static Rectangle<T> operator +(Rectangle<T> r, Point<T> pt) => new Rectangle<T>(r.Location + pt, r.Size);

        public static Rectangle<T> operator -(Rectangle<T> r, Point<T> pt) => new Rectangle<T>(r.Location - pt, r.Size);

        public static Rectangle<T> operator +(Rectangle<T> r, Size<T> s) => new Rectangle<T>(r.Location, r.Size + s);

        public static Rectangle<T> operator -(Rectangle<T> r, Size<T> s) => new Rectangle<T>(r.Location, r.Size - s);
    }
}