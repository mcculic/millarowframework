using System.Collections.Generic;

namespace Millarow.Rest.Serialization.Formatters
{
    public class ByteArrayContentFormatter : TypedContentFormatter<byte[]>
    {
        public ByteArrayContentFormatter(IEnumerable<MimeType> supportedMediaTypes) 
            : base(supportedMediaTypes)
        {
        }

        public override byte[] Deserialize(byte[] contentData, ContentType contentType)
            => contentData;

        public override byte[] Serialize(ContentType contentType, byte[] value)
            => value;
    }
}
