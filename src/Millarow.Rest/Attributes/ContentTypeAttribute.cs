using Millarow.Rest.Metadata.Providers;
using System;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
    public sealed class ContentTypeAttribute : Attribute, IContentCharSetProvider, IContentMediaTypeProvider
    {
        public ContentTypeAttribute(string mediaType)
        {
            mediaType.AssertNotNull(nameof(mediaType));

            MediaType = mediaType;
        }

        public string MediaType { get; }

        public string CharSet { get; set; }

        Maybe<string> IContentCharSetProvider.CharSet => CharSet == null ? Maybe.Nothing<string>() : CharSet;

        Maybe<MimeType> IContentMediaTypeProvider.MediaType => MediaType == null ? Maybe.Nothing<MimeType>() : new MimeType(MediaType);
    }
}
