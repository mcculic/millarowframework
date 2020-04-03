using System.Reflection;

namespace Millarow.Rest.Metadata
{
    public class MethodMetadata
    {
        public MethodMetadata(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
        }

        public MethodInfo MethodInfo { get; }

        public RequestMetadata Request { get; set; }

        public ResponseMetadata Response { get; set; }
    }
}