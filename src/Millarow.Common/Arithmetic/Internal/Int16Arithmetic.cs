namespace Millarow.Arithmetic.Internal
{
    internal sealed class Int16Arithmetic : Arithmetic<short>
    {
        public override short Add(short a, short b)
        {
            checked
            {
                return (short)(a + b);
            }
        }

        public override short Divide(short value, short divider)
        {
            checked
            {
                return (short)(value / divider);
            }
        }

        public override short Multiply(short a, short b)
        {
            checked
            {
                return (short)(a * b);
            }
        }

        public override short Subtract(short a, short b)
        {
            checked
            {
                return (short)(a - b);
            }
        }

        public override short Min(short a, short b)
        {
            return a < b ? a : b;
        }

        public override short Max(short a, short b)
        {
            return a > b ? a : b;
        }

        public override double ToDouble(short value)
        {
            return value;
        }

        public override short FromDouble(double value)
        {
            checked
            {
                return (short)value;
            }
        }
        
        public override short MinValue => short.MinValue;

        public override short MaxValue => short.MaxValue;

        public override short One => 1;

        public override short Zero => 0;

        public override short NegativeOne => -1;
    }
}