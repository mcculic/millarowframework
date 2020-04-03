using Millarow.Rest.Metadata.Providers;
using System;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class QueryAttribute : RequestParameterAttribute, IStringFormatProvider, IOmitDefaultProvider, IIsRequiredProvider
    {
        public QueryAttribute()
        {
        }

        public QueryAttribute(string name)
        {
            Name = name;
        }

        public string StringFormat { get; set; }

        public bool OmitDefault { get; set; }

        public bool OmitEmpty { get; set; }

        public bool IsRequired { get; set; }

        public override RequestParameterKind ParameterKind => RequestParameterKind.Query;

        Maybe<bool> IIsRequiredProvider.IsRequired => IsRequired;

        Maybe<string> IStringFormatProvider.Format => StringFormat ?? Maybe.Nothing<string>();

        Maybe<bool> IOmitDefaultProvider.OmitDefault => OmitDefault;
    }
}