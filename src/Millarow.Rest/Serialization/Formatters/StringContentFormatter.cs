using System.Collections.Generic;
using System.Text;

namespace Millarow.Rest.Serialization.Formatters
{
    public class StringContentFormatter : TypedContentFormatter<string>
    {
        public StringContentFormatter(IEnumerable<MimeType> supportedMediaTypes)
            : base(supportedMediaTypes)
        {
        }

        public override byte[] Serialize(ContentType contentType, string value)
        {
            var encoding = Encoding.GetEncoding(contentType.CharSet);

            return encoding.GetBytes(value);
        }

        public override string Deserialize(byte[] contentData, ContentType contentType)
        {
            var encoding = Encoding.GetEncoding(contentType.CharSet);

            return encoding.GetString(contentData);
        }
    }
}
