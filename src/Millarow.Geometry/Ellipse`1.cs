using Millarow.Arithmetic;
using System;

namespace Millarow.Geometry
{
    [Serializable]
    public struct Ellipse<T> : IEquatable<Ellipse<T>>
        where T : struct, IEquatable<T>, IComparable<T>, IFormattable
    {
        private static readonly IGenericArithmetic<T> _arithmetic = GenericArithmetic.Get<T>();

        [System.Diagnostics.DebuggerStepThrough]
        public Ellipse(T centerX, T centerY, T radiusX, T radiusY)
        {
            CenterX = centerX;
            CenterY = centerY;
            RadiusX = radiusX;
            RadiusY = radiusY;
        }

        [System.Diagnostics.DebuggerStepThrough]
        public Ellipse(Point<T> center, Size<T> radius)
            : this(center.X, center.Y, radius.Width, radius.Height)
        {
        }

        public Ellipse<double> ToDouble()
        {
            return new Ellipse<double>(Center.ToDouble(), Radius.ToDouble());
        }

        public override string ToString()
        {
            return $"CenterX={CenterX} CenterY={CenterY} RadiusX={RadiusX} RadiusY={RadiusY}";
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return $"CenterX={CenterX.ToString(format, provider)} CenterY={CenterY.ToString(format, provider)} RadiusX={RadiusX.ToString(format, provider)} RadiusY={RadiusY.ToString(format, provider)}";
        }

        public bool Equals(Ellipse<T> other)
        {
            return other.CenterX.Equals(CenterX) && other.CenterY.Equals(CenterY) && other.RadiusX.Equals(RadiusX) && other.RadiusY.Equals(RadiusY);
        }

        public override bool Equals(object obj)
        {
            return obj is Ellipse<T> ? Equals((Ellipse<T>)obj) : false;
        }

        public override int GetHashCode()
        {
            return HashCode.Oat.Get(CenterX, CenterY, RadiusX, RadiusY);
        }

        public T CenterX { get; }

        public T CenterY { get; }

        public T RadiusX { get; }

        public T RadiusY { get; }

        public Point<T> Center => new Point<T>(CenterX, CenterY);

        public Size<T> Radius => new Size<T>(RadiusX, RadiusY);

        public Rectangle<T> Bounds
        {
            get
            {
                var left = _arithmetic.Subtract(CenterX, RadiusX);
                var top = _arithmetic.Subtract(CenterY, RadiusY);
                var width = _arithmetic.Add(RadiusX, RadiusX);
                var height = _arithmetic.Add(RadiusY, RadiusY);

                return new Rectangle<T>(left, top, width, height);
            }
        }
    }
}