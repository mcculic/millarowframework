namespace Millarow.Arithmetic.Internal
{
    internal sealed class Int32Arithmetic : Arithmetic<int>
    {
        public override int Add(int a, int b)
        {
            checked
            {
                return a + b;
            }
        }

        public override int Divide(int value, int divider)
        {
            checked
            {
                return value / divider;
            }
        }

        public override int Multiply(int a, int b)
        {
            checked
            {
                return a * b;
            }
        }

        public override int Subtract(int a, int b)
        {
            checked
            {
                return a - b;
            }
        }

        public override int Min(int a, int b)
        {
            return a < b ? a : b;
        }

        public override int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        public override double ToDouble(int value)
        {
            return value;
        }

        public override int FromDouble(double value)
        {
            checked
            {
                return (int)value;
            }
        }

        public override int One => 1;

        public override int Zero => 0;

        public override int MinValue => int.MinValue;

        public override int MaxValue => int.MaxValue;

        public override int NegativeOne => -1;
    }
}
