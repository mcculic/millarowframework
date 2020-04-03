using System.Collections.Generic;
using System.Linq;

namespace Millarow.Rest
{
    public class RequestUrlEncodedContent : RequestContent
    {
        private List<RequestFormField> _fields;

        public RequestUrlEncodedContent(string charSet)
            : base(new ContentType(MimeTypes.Application.FormUrlEncoded, charSet))
        {
        }

        public void AddField(RequestFormField field)
        {
            field.AssertNotNull(nameof(field));

            if (_fields == null)
                _fields = new List<RequestFormField>();

            _fields.Add(field);
        }

        public void AddField<T>(string name, T value, MimeType mediaType)
        {
            name.AssertNotNull(nameof(name));
            mediaType.AssertNotNull(nameof(mediaType));

            _fields.Insert(0, new RequestFormField(name, RestValue.Create(value), mediaType));
        }

        public IEnumerable<RequestFormField> Fields => _fields ?? Enumerable.Empty<RequestFormField>();
    }
}
