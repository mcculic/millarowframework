using Millarow.Rest.Metadata.Providers;
using System;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class RequestBodyAttribute : RequestParameterAttribute, IContentMediaTypeProvider, IContentCharSetProvider, IIsRequiredProvider
    {
        public override RequestParameterKind ParameterKind => RequestParameterKind.Body;

        public string MediaType { get; set; }

        public string CharSet { get; set; }
        
        public bool IsRequired { get; set; }

        Maybe<MimeType> IContentMediaTypeProvider.MediaType => MediaType == null ? null : new MimeType(MediaType);

        Maybe<string> IContentCharSetProvider.CharSet => CharSet == null ? Maybe.Nothing<string>() : CharSet;

        Maybe<bool> IIsRequiredProvider.IsRequired => IsRequired;
    }
}