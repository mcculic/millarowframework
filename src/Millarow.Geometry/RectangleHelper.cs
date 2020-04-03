using System;

namespace Millarow.Geometry
{
    public static class RectangleHelper
    {
        public static Rectangle<int> Round(this Rectangle<float> value)
        {
            var l = (int)Math.Round(value.Left);
            var t = (int)Math.Round(value.Top);
            var w = (int)Math.Round(value.Width);
            var h = (int)Math.Round(value.Height);

            return new Rectangle<int>(l, t, w, h);
        }

        public static Rectangle<int> Round(this Rectangle<double> value)
        {
            var l = (int)Math.Round(value.Left);
            var t = (int)Math.Round(value.Top);
            var w = (int)Math.Round(value.Width);
            var h = (int)Math.Round(value.Height);

            return new Rectangle<int>(l, t, w, h);
        }

        public static Rectangle<int> Truncate(this Rectangle<float> value)
        {
            var l = (int)value.Left;
            var t = (int)value.Top;
            var w = (int)value.Width;
            var h = (int)value.Height;

            return new Rectangle<int>(l, t, w, h);
        }

        public static Rectangle<int> Truncate(this Rectangle<double> value)
        {
            var l = (int)value.Left;
            var t = (int)value.Top;
            var w = (int)value.Width;
            var h = (int)value.Height;

            return new Rectangle<int>(l, t, w, h);
        }
    }
}
