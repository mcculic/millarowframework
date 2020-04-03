using System.IO;

namespace Millarow.Rest
{
    public sealed class RequestBinaryContent : RequestContent
    {
        private readonly byte[] _data;

        public RequestBinaryContent(byte[] data, ContentType contentType) 
            : base(contentType)
        {
            data.AssertNotNull(nameof(data));

            _data = data;
        }

        public RequestBinaryContent(byte[] data, MimeType mediaType, string charSet)
            : this(data, new ContentType(mediaType, charSet))
        {
        }

        public Stream ReadAsStream()
        {
            return new MemoryStream(_data, false);
        }
    }
}
