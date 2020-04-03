using System;

namespace Millarow.Geometry
{
    public static class ThicknessHelper
    {
        public static Thickness<int> Round(this Thickness<float> value)
        {
            var l = (int)Math.Round(value.Left);
            var t = (int)Math.Round(value.Top);
            var r = (int)Math.Round(value.Right + 0.5);
            var b = (int)Math.Round(value.Bottom + 0.5);

            return new Thickness<int>(l, t, r, b);
        }

        public static Thickness<int> Round(this Thickness<double> value)
        {
            var l = (int)Math.Round(value.Left);
            var t = (int)Math.Round(value.Top);
            var r = (int)Math.Round(value.Right);
            var b = (int)Math.Round(value.Bottom);

            return new Thickness<int>(l, t, r, b);
        }

        public static Thickness<int> Truncate(this Thickness<float> value)
        {
            var l = (int)value.Left;
            var t = (int)value.Top;
            var r = (int)value.Right;
            var b = (int)value.Bottom;

            return new Thickness<int>(l, t, r, b);
        }

        public static Thickness<int> Truncate(this Thickness<double> value)
        {
            var l = (int)value.Left;
            var t = (int)value.Top;
            var r = (int)value.Right;
            var b = (int)value.Bottom;

            return new Thickness<int>(l, t, r, b);
        }
    }
}