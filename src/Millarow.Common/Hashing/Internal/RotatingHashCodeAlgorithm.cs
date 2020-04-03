namespace Millarow.Hashing.Internal
{
    internal sealed class RotatingHashCodeAlgorithm : HashCodeAlgorithm
    {
        private RotatingHashCodeAlgorithm()
        {
        }

        protected override void CombineHash<T>(ref int hash, T value)
        {
            unchecked
            {
                var valueHash = value == null ? 0 : value.GetHashCode();

                for (int i = 0; i < 4; i++)
                {
                    var valueHashByte = valueHash >> (i << 3);

                    hash = (hash << 4) ^ (hash >> 28) ^ valueHashByte;
                }
            }
        }

        public static RotatingHashCodeAlgorithm Instance { get; } = new RotatingHashCodeAlgorithm();
    }
}