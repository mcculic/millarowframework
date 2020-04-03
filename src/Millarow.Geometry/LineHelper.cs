using System;

namespace Millarow.Geometry
{
    public static class LineHelper
    {
        public static Line<int> Round(this Line<float> value)
        {
            return new Line<int>((int)Math.Round(value.X1), (int)Math.Round(value.Y1), (int)Math.Round(value.X2), (int)Math.Round(value.Y2));
        }

        public static Line<int> Round(this Line<double> value)
        {
            return new Line<int>((int)Math.Round(value.X1), (int)Math.Round(value.Y1), (int)Math.Round(value.X2), (int)Math.Round(value.Y2));
        }

        public static Line<int> Truncate(this Line<float> value)
        {
            return new Line<int>((int)value.X1, (int)value.Y1, (int)value.X2, (int)value.Y2);
        }

        public static Line<int> Truncate(this Line<double> value)
        {
            return new Line<int>((int)value.X1, (int)value.Y1, (int)value.X2, (int)value.Y2);
        }
    }
}