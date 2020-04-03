using System;

namespace Millarow.Geometry
{
    [Serializable]
    public struct Angle
    {
        private Angle(double radians)
        {
            Radians = radians;
        }

        public static Angle FromRadians(double radian)
        {
            return new Angle(radian);
        }

        public static Angle FromDegree(double degree)
        {
            return new Angle(degree * DegreeToRadian);
        }

        public static Angle operator *(Angle value, double m)
        {
            return new Angle(value.Radians * m);
        }

        public override string ToString()
        {
            return Radians.ToString();
        }

        public string ToString(string format, IFormatProvider provider)
        {
            return Radians.ToString(format, provider);
        }

        public double Radians { get; }

        public double Degree => Radians / DegreeToRadian;

        private const double DegreeToRadian = Math.PI / 180.0;
    }
}