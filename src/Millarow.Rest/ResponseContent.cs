using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Millarow.Rest
{
    public class ResponseContent
    {
        public ResponseContent(byte[] data, ContentType contentType, IReadOnlyList<ResponseHeader> headers)
        {
            data.AssertNotNull(nameof(data));
            contentType.AssertNotNull(nameof(contentType));
            headers.AssertNotNull(nameof(headers));
            
            Data = data;
            ContentType = contentType;
            Headers = headers;
        }

        public Stream ReadAsStream()
        {
            return new MemoryStream(Data, false);
        }

        public byte[] ReadAsArray()
        {
            return Data.ToArray();
        }

        private byte[] Data { get; }

        public ContentType ContentType { get; }
        
        public IReadOnlyList<ResponseHeader> Headers { get; }
    }
}
