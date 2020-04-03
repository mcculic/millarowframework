using System.Collections.Generic;

namespace Millarow.Rest.Metadata
{
    public class EndpointMetadata
    {
        public EndpointMetadata()
        {
        }

        public IEnumerable<MethodMetadata> Methods { get; set; }
    }
}
