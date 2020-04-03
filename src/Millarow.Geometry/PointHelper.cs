using System;

namespace Millarow.Geometry
{
    public static class PointHelper
    {
        public static Point<int> Round(this Point<float> value)
        {
            return new Point<int>((int)Math.Round(value.X), (int)Math.Round(value.Y));
        }

        public static Point<int> Round(this Point<double> value)
        {
            return new Point<int>((int)Math.Round(value.X), (int)Math.Round(value.Y));
        }

        public static Point<int> Truncate(this Point<double> value)
        {
            return new Point<int>((int)value.X, (int)value.Y);
        }

        public static Point<int> Truncate(this Point<float> value)
        {
            return new Point<int>((int)value.X, (int)value.Y);
        }
    }
}