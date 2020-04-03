namespace Millarow.Hashing.Internal
{
    internal sealed class OatHashCodeAlgorithm : HashCodeAlgorithm
    {
        private OatHashCodeAlgorithm()
        {
        }
        
        protected override void CombineHash<T>(ref int hash, T value)
        {
            unchecked
            {
                if (value != null)
                    hash += value.GetHashCode();

                hash += (hash << 10);
                hash ^= (hash >> 6);
            }
        }

        protected override int FinalizeHash(int hash)
        {
            unchecked
            {
                hash += (hash << 3);
                hash ^= (hash >> 11);
                hash += (hash << 15);

                return hash;
            }
        }

        public static OatHashCodeAlgorithm Instance { get; } = new OatHashCodeAlgorithm();
    }
}