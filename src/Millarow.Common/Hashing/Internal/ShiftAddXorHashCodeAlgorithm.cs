namespace Millarow.Hashing.Internal
{
    internal sealed class ShiftAddXorHashCodeAlgorithm : HashCodeAlgorithm
    {
        private ShiftAddXorHashCodeAlgorithm()
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

                    hash ^= (hash << 5) + (hash >> 2) + valueHashByte;
                }
            }
        }

        public static ShiftAddXorHashCodeAlgorithm Instance { get; } = new ShiftAddXorHashCodeAlgorithm();
    }
}
