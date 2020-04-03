using System;
using System.Collections.Generic;

namespace Millarow.Rest.Metadata
{
    public class RequestMetadata
    {
        public RequestMetadata(RequestMethod requestMethod)
        {
            Method = requestMethod;
        }

        public RequestMethod Method { get; }

        public string Route { get; set; }

        public RequestBodyType ContentKind { get; set; }

        public TimeSpan? Timeout { get; set; }

        public IReadOnlyList<RequestParameterMetadata> Parameters { get; set; }
    }
}
