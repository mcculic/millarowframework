namespace Millarow.Arithmetic.Internal
{
    internal sealed class UInt8Arithmetic : Arithmetic<byte>
    {
        public override byte Add(byte a, byte b)
        {
            checked
            {
                return (byte)(a + b);
            }
        }

        public override byte Subtract(byte a, byte b)
        {
            checked
            {
                return (byte)(a - b);
            }
        }

        public override byte Divide(byte value, byte divider)
        {
            checked
            {
                return (byte)(value / divider);
            }
        }

        public override byte Multiply(byte a, byte b)
        {
            checked
            {
                return (byte)(a * b);
            }
        }

        public override byte Min(byte a, byte b)
        {
            return a < b ? a : b;
        }

        public override byte Max(byte a, byte b)
        {
            return a > b ? a : b;
        }
        
        public override double ToDouble(byte value)
        {
            return value;
        }

        public override byte FromDouble(double value)
        {
            checked
            {
                return (byte)value;
            }
        }

        private static byte ToByte(int value)
        {
            checked
            {
                return (byte)value;
            }
        }

        public override byte MinValue => byte.MinValue;

        public override byte MaxValue => byte.MaxValue;

        public override byte Zero => 0;

        public override byte One => 1;

        public override byte NegativeOne => throw CreatePropertyNotSupportedException(nameof(NegativeOne));
    }
}
