namespace Millarow.Rest
{
    public class RequestBody : RequestContent
    {
        public RequestBody(RestValue content, ContentType contentType)
            : base(contentType)
        {
            content.AssertNotNull(nameof(content));

            Content = content;
        }

        public RestValue Content { get; }
    }
}
