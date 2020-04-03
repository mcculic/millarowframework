using System;

namespace Millarow.Presentation
{
    internal static class Exceptions
    {
        public static Exception ReentrancyCheckFailed(Exception innerException = null)
        {
            return new InvalidOperationException("Reentrancy or cross thread operation detected", innerException);
        }
    }
}
