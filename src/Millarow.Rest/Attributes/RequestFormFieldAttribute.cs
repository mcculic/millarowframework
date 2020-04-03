using Millarow.Rest.Metadata.Providers;
using System;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class RequestFormFieldAttribute : RequestParameterAttribute, IContentMediaTypeProvider
    {
        public RequestFormFieldAttribute(string name)
        {
            Name = name;
        }

        public RequestFormFieldAttribute()
        {
        }

        public string MediaType { get; set; }

        public override RequestParameterKind ParameterKind => RequestParameterKind.FormField;

        Maybe<MimeType> IContentMediaTypeProvider.MediaType => MediaType == null ? null : Maybe.Just<MimeType>(MediaType);
    }
}
