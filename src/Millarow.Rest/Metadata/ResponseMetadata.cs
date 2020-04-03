using System;
using System.Collections.Generic;

namespace Millarow.Rest.Metadata
{
    public class ResponseMetadata
    {
        public bool IsAsync { get; set; }

        public Type ResultType { get; set; }

        public IReadOnlyList<ResponseParameterMetadata> Parameters { get; set; }
    }
}
