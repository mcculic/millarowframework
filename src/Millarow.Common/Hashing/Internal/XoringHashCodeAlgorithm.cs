namespace Millarow.Hashing.Internal
{
    internal sealed class XoringHashCodeAlgorithm : HashCodeAlgorithm
    {
        private XoringHashCodeAlgorithm()
        {
        }

        protected override void CombineHash<T>(ref int hash, T value)
        {
            if (value != null)
                hash = hash ^ value.GetHashCode();
        }

        public static XoringHashCodeAlgorithm Instance { get; } = new XoringHashCodeAlgorithm();
    }
}