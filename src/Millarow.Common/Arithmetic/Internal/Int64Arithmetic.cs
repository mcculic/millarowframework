namespace Millarow.Arithmetic.Internal
{
    internal sealed class Int64Arithmetic : Arithmetic<long>
    {
        public override long Add(long a, long b)
        {
            checked
            {
                return a + b;
            }
        }

        public override long Divide(long value, long divider)
        {
            checked
            {
                return value / divider;
            }
        }

        public override long Multiply(long a, long b)
        {
            checked
            {
                return a * b;
            }
        }

        public override long Subtract(long a, long b)
        {
            checked
            {
                return a - b;
            }
        }

        public override long Min(long a, long b)
        {
            return a < b ? a : b;
        }

        public override long Max(long a, long b)
        {
            return a > b ? a : b;
        }

        public override double ToDouble(long value)
        {
            return value;
        }

        public override long FromDouble(double value)
        {
            checked
            {
                return (long)value;
            }
        }

        public override long One => 1;

        public override long Zero => 0;

        public override long MinValue => long.MinValue;

        public override long MaxValue => long.MaxValue;

        public override long NegativeOne => -1;
    }
}
