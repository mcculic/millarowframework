namespace Millarow.Arithmetic.Internal
{
    internal sealed class FloatArithmetic : Arithmetic<float>
    {
        public override float Add(float a, float b)
        {
            return a + b;
        }

        public override float Subtract(float a, float b)
        {
            return a - b;
        }

        public override float Divide(float value, float divider)
        {
            return value / divider;
        }

        public override float Multiply(float a, float b)
        {
            return a * b;
        }

        public override float Min(float a, float b)
        {
            return a < b ? a : b;
        }

        public override float Max(float a, float b)
        {
            return a > b ? a : b;
        }

        public override double ToDouble(float value)
        {
            return value;
        }

        public override float FromDouble(double value)
        {
            if (value < float.MinValue)
                return float.MinValue;

            if (value > float.MaxValue)
                return float.MaxValue;

            return (float)value;
        }

        public override float MinValue => float.MinValue;

        public override float MaxValue => float.MaxValue;

        public override float One => 1.0F;

        public override float Zero => 0.0F;

        public override float NegativeOne => -1.0F;
    }
}
