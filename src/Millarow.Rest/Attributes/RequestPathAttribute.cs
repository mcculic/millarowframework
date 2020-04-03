using Millarow.Rest.Metadata.Providers;
using System;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class RequestPathAttribute : RequestParameterAttribute, IStringFormatProvider, IIsRequiredProvider
    {
        public RequestPathAttribute()
        {
        }

        public RequestPathAttribute(string name)
        {
            Name = name;
        }

        public string StringFormat { get; set; }
        
        public bool IsRequired { get; set; }

        public override RequestParameterKind ParameterKind => RequestParameterKind.Route;

        Maybe<string> IStringFormatProvider.Format => StringFormat == null ? Maybe.Nothing<string>() : StringFormat;

        Maybe<bool> IIsRequiredProvider.IsRequired => IsRequired;
    }
}
