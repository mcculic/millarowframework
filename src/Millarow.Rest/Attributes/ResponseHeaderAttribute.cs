using System;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ResponseHeaderAttribute : ResponseParameterAttribute
    {
        public override RequestParameterKind ParameterKind => RequestParameterKind.Header;
    }
}
