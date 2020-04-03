namespace Millarow.Arithmetic.Internal
{
    internal sealed class UInt16Arithmetic : Arithmetic<ushort>
    {
        public override ushort Add(ushort a, ushort b)
        {
            checked
            {
                return (ushort)(a + b);
            }
        }

        public override ushort Divide(ushort value, ushort divider)
        {
            checked
            {
                return (ushort)(value / divider);
            }
        }

        public override ushort Multiply(ushort a, ushort b)
        {
            checked
            {
                return (ushort)(a * b);
            }
        }

        public override ushort Subtract(ushort a, ushort b)
        {
            checked
            {
                return (ushort)(a - b);
            }
        }

        public override ushort Min(ushort a, ushort b)
        {
            return a < b ? a : b;
        }

        public override ushort Max(ushort a, ushort b)
        {
            return a > b ? a : b;
        }

        public override double ToDouble(ushort value)
        {
            return value;
        }

        public override ushort FromDouble(double value)
        {
            checked
            {
                return (ushort)value;
            }
        }

        public override ushort MinValue => ushort.MinValue;

        public override ushort MaxValue => ushort.MaxValue;

        public override ushort One => 1;

        public override ushort Zero => 0;

        public override ushort NegativeOne => throw CreatePropertyNotSupportedException(nameof(NegativeOne));
    }
}