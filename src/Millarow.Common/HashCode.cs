using Millarow.Hashing;
using Millarow.Hashing.Internal;

namespace Millarow
{
    //http://eternallyconfuzzled.com/tuts/algorithms/jsw_tut_hashing.aspx#djb
    public static class HashCode
    {
        public static HashCodeAlgorithm BernsteinAdd => BernsteinAddHashCodeAlgorithm.Instance;

        public static HashCodeAlgorithm BernsteinXor => BernsteinXorHashCodeAlgorithm.Instance;

        public static HashCodeAlgorithm Oat => OatHashCodeAlgorithm.Instance;

        public static HashCodeAlgorithm Rotating => RotatingHashCodeAlgorithm.Instance;

        public static HashCodeAlgorithm Xoring => XoringHashCodeAlgorithm.Instance;

        public static HashCodeAlgorithm ShiftAddXor => ShiftAddXorHashCodeAlgorithm.Instance;
    }
}
