namespace Millarow.Rest
{
    public class RequestFormField
    {
        public RequestFormField(string name, RestValue content, MimeType mediaType)
        {
            content.AssertNotNull(nameof(content));
            name.AssertNotNullOrEmpty(nameof(name));
            mediaType.AssertNotNull(nameof(mediaType));

            Name = name;
            Content = content;
            MediaType = mediaType;
        }

        public string Name { get; }

        public RestValue Content { get; }

        public MimeType MediaType { get; }
    }
}
