namespace Millarow.Arithmetic.Internal
{
    internal sealed class DecimalArithmetic : Arithmetic<decimal>
    {
        public override decimal Add(decimal a, decimal b) => a + b;

        public override decimal Subtract(decimal a, decimal b) => a - b;

        public override decimal Divide(decimal value, decimal divider) => value / divider;

        public override decimal Multiply(decimal a, decimal b) => a * b;

        public override decimal Min(decimal a, decimal b) => a < b ? a : b;

        public override decimal Max(decimal a, decimal b) => a > b ? a : b;

        public override double ToDouble(decimal value) => (double)value;

        public override decimal FromDouble(double value) => (decimal)value;

        public override decimal MinValue => decimal.MinValue;

        public override decimal MaxValue => decimal.MaxValue;

        public override decimal Zero => 0;

        public override decimal One => 1;

        public override decimal NegativeOne => -1.0M;
    }
}
