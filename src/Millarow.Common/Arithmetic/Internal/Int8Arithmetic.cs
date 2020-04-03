namespace Millarow.Arithmetic.Internal
{
    internal sealed class Int8Arithmetic : Arithmetic<sbyte>
    {
        public override sbyte Add(sbyte a, sbyte b)
        {
            checked
            {
                return (sbyte)(a + b);
            }
        }

        public override sbyte Subtract(sbyte a, sbyte b)
        {
            checked
            {
                return (sbyte)(a - b);
            }
        }

        public override sbyte Divide(sbyte value, sbyte divider)
        {
            checked
            {
                return (sbyte)(value / divider);
            }
        }

        public override sbyte Multiply(sbyte a, sbyte b)
        {
            checked
            {
                return (sbyte)(a * b);
            }
        }

        public override sbyte Min(sbyte a, sbyte b)
        {
            return a < b ? a : b;
        }

        public override sbyte Max(sbyte a, sbyte b)
        {
            return a > b ? a : b;
        }
        
        public override double ToDouble(sbyte value)
        {
            return value;
        }

        public override sbyte FromDouble(double value)
        {
            checked
            {
                return (sbyte)value;
            }
        }

        public override sbyte MinValue => sbyte.MinValue;

        public override sbyte MaxValue => sbyte.MaxValue;

        public override sbyte Zero => 0;

        public override sbyte One => 1;

        public override sbyte NegativeOne => -1;
    }
}
