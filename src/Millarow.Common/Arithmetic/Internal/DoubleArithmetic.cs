namespace Millarow.Arithmetic.Internal
{
    internal sealed class DoubleArithmetic : Arithmetic<double>
    {
        public override double Add(double a, double b)
        {
            return a + b;
        }

        public override double Subtract(double a, double b)
        {
            return a - b;
        }

        public override double Divide(double value, double divider)
        {
            return value / divider;
        }

        public override double Multiply(double a, double b)
        {
            return a * b;
        }

        public override double Min(double a, double b)
        {
            return a < b ? a : b;
        }

        public override double Max(double a, double b)
        {
            return a > b ? a : b;
        }

        public override double ToDouble(double value)
        {
            return value;
        }

        public override double FromDouble(double value)
        {
            return value;
        }

        public override double MinValue => double.MinValue;

        public override double MaxValue => double.MaxValue;

        public override double Zero => 0.0;

        public override double One => 1.0;

        public override double NegativeOne => -1.0;
    }
}
