using Millarow.Rest.Metadata.Providers;

namespace Millarow.Rest.Attributes
{
    public sealed class RequestHeaderAttribute : RequestParameterAttribute, IStringFormatProvider
    {
        public string StringFormat { get; set; }

        public override RequestParameterKind ParameterKind => RequestParameterKind.Header;

        Maybe<string> IStringFormatProvider.Format => StringFormat == null ? Maybe.Nothing<string>() : StringFormat;
    }
}
