using System.Collections.Generic;
using System.Linq;

namespace Millarow.Rest
{
    public abstract class RequestContent
    {
        private List<RequestHeader> _headers;

        protected RequestContent(ContentType contentType)
        {
            contentType.AssertNotNull(nameof(contentType));

            ContentType = contentType;
        }

        public void AddHeader(RequestHeader header)
        {
            header.AssertNotNull(nameof(header));

            if (_headers == null)
                _headers = new List<RequestHeader>();

            _headers.Add(header);
        }

        public ContentType ContentType { get; }

        public IEnumerable<RequestHeader> Headers => _headers ?? Enumerable.Empty<RequestHeader>();
    }
}