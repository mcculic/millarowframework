namespace Millarow.Rest
{
    public sealed class ContentType
    {
        public ContentType(MimeType mediaType, string charSet)
        {
            mediaType.AssertNotNull(nameof(mediaType));
            charSet.AssertNotNull(nameof(charSet));

            MediaType = mediaType;
            CharSet = charSet;
        }

        public ContentType(MimeType mediaType)
            : this(mediaType, null)
        {
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(CharSet))
                return MediaType;
         
            return $"{MediaType}; {CharSet}";
        }

        public MimeType MediaType { get; }

        public string CharSet { get; }
    }
}
