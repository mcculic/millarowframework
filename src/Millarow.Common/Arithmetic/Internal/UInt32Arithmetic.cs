namespace Millarow.Arithmetic.Internal
{
    internal sealed class UInt32Arithmetic : Arithmetic<uint>
    {
        public override uint Add(uint a, uint b)
        {
            checked
            {
                return a + b;
            }
        }

        public override uint Divide(uint value, uint divider)
        {
            checked
            {
                return value / divider;
            }
        }

        public override uint Multiply(uint a, uint b)
        {
            checked
            {
                return a * b;
            }
        }

        public override uint Subtract(uint a, uint b)
        {
            checked
            {
                return a - b;
            }
        }

        public override uint Min(uint a, uint b)
        {
            return a < b ? a : b;
        }

        public override uint Max(uint a, uint b)
        {
            return a > b ? a : b;
        }

        public override double ToDouble(uint value)
        {
            return value;
        }

        public override uint FromDouble(double value)
        {
            checked
            {
                return (uint)value;
            }
        }

        public override uint MinValue => uint.MinValue;

        public override uint MaxValue => uint.MaxValue;

        public override uint One => 1;

        public override uint Zero => 0;

        public override uint NegativeOne => throw CreatePropertyNotSupportedException(nameof(NegativeOne));
    }
}
