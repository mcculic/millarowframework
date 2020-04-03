using System;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
    public sealed class ResponseBodyAttribute : ResponseParameterAttribute
    {
        public override RequestParameterKind ParameterKind => RequestParameterKind.Body;
    }
}
