using System;

namespace Millarow.Geometry
{
    public static class SizeHelper
    {
        public static Size<int> Round(this Size<float> value)
        {
            var w = (int)Math.Round(value.Width);
            var h = (int)Math.Round(value.Height);

            return new Size<int>(w, h);
        }

        public static Size<int> Round(this Size<double> value)
        {
            var w = (int)Math.Round(value.Width);
            var h = (int)Math.Round(value.Height);

            return new Size<int>(w, h);
        }

        public static Size<int> Truncate(this Size<float> value)
        {
            var w = (int)value.Width;
            var h = (int)value.Height;

            return new Size<int>(w, h);
        }

        public static Size<int> Truncate(this Size<double> value)
        {
            var w = (int)value.Width;
            var h = (int)value.Height;

            return new Size<int>(w, h);
        }
    }
}