using System;

namespace Millarow.Geometry
{
    public static class EllipseHelper
    {
        public static Ellipse<int> Round(this Ellipse<float> value)
        {
            return new Ellipse<int>((int)Math.Round(value.CenterX), (int)Math.Round(value.CenterY), (int)Math.Round(value.RadiusX), (int)Math.Round(value.RadiusY));
        }

        public static Ellipse<int> Round(this Ellipse<double> value)
        {
            return new Ellipse<int>((int)Math.Round(value.CenterX), (int)Math.Round(value.CenterY), (int)Math.Round(value.RadiusX), (int)Math.Round(value.RadiusY));
        }

        public static Ellipse<int> Truncate(this Ellipse<float> value)
        {
            return new Ellipse<int>((int)value.CenterX, (int)value.CenterY, (int)value.RadiusX, (int)value.RadiusY);
        }

        public static Ellipse<int> Truncate(this Ellipse<double> value)
        {
            return new Ellipse<int>((int)value.CenterX, (int)value.CenterY, (int)value.RadiusX, (int)value.RadiusY);
        }
    }
}
