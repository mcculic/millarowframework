using Millarow.Rest.Metadata.Providers;
using System;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class RequestTimeoutAttribute : Attribute, ITimeoutProvider
    {
        public RequestTimeoutAttribute(double milliseconds)
        {
            Milliseconds = milliseconds;
        }

        public double Milliseconds { get; }

        Maybe<TimeSpan> ITimeoutProvider.Timeout => TimeSpan.FromMilliseconds(Milliseconds);
    }
}