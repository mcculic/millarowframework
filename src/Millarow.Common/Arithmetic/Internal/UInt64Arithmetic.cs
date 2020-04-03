namespace Millarow.Arithmetic.Internal
{
    internal sealed class UInt64Arithmetic : Arithmetic<ulong>
    {
        public override ulong Add(ulong a, ulong b)
        {
            checked
            {
                return a + b;
            }
        }

        public override ulong Divide(ulong value, ulong divider)
        {
            checked
            {
                return value / divider;
            }
        }

        public override ulong Multiply(ulong a, ulong b)
        {
            checked
            {
                return a * b;
            }
        }

        public override ulong Subtract(ulong a, ulong b)
        {
            checked
            {
                return a - b;
            }
        }

        public override ulong Min(ulong a, ulong b)
        {
            return a < b ? a : b;
        }

        public override ulong Max(ulong a, ulong b)
        {
            return a > b ? a : b;
        }

        public override double ToDouble(ulong value)
        {
            return value;
        }

        public override ulong FromDouble(double value)
        {
            checked
            {
                return (ulong)value;
            }
        }

        public override ulong MinValue => ulong.MinValue;

        public override ulong MaxValue => ulong.MaxValue;

        public override ulong One => 1;

        public override ulong Zero => 0;

        public override ulong NegativeOne
        {
            get { throw CreatePropertyNotSupportedException(nameof(NegativeOne)); }
        }
    }
}
