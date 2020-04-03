using Millarow.Rest.Metadata.Providers;
using System;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class RequestAttribute : Attribute, IRequestContentKindProvider
    {
        public RequestAttribute()
        {
        }

        public abstract RequestBodyType Kind { get; }

        Maybe<RequestBodyType> IRequestContentKindProvider.GetKind() => Kind;
    }
}
