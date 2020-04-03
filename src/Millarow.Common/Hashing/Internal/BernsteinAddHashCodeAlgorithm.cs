namespace Millarow.Hashing.Internal
{
    internal sealed class BernsteinAddHashCodeAlgorithm : HashCodeAlgorithm
    {
        private const int HashSeed = 29;
        private const int HashMultiplier = 33;

        private BernsteinAddHashCodeAlgorithm()
        {
        }
        
        protected override int SeedHash()
        {
            return HashSeed;
        }

        protected override void CombineHash<T>(ref int hash, T value)
        {
            unchecked
            {
                hash *= HashMultiplier;

                if (value != null)
                    hash += value.GetHashCode();
            }
        }

        public static BernsteinAddHashCodeAlgorithm Instance { get; } = new BernsteinAddHashCodeAlgorithm();
    }
}