using System.Collections.Generic;
using System.Linq;

namespace Millarow.Rest
{
    public class RequestMultipartContent : RequestContent
    {
        private List<RequestFile> _parts;

        public RequestMultipartContent(string charSet) 
            : base(new ContentType(MimeTypes.Multipart.FormData, charSet))
        {
        }

        public void Part(RequestFile part)
        {
            part.AssertNotNull(nameof(part));

            if (_parts == null)
                _parts = new List<RequestFile>();

            _parts.Add(part);
        }

        public void Add(RequestContent part, string name, string fileName)
        {
            Part(new RequestFile(part, name)
            {
                FileName = fileName
            });
        }

        public string Boundary { get; set; }

        public IEnumerable<RequestFile> Parts => _parts ?? Enumerable.Empty<RequestFile>();
    }
}
