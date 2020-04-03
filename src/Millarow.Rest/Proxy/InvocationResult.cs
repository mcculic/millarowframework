using Millarow.Rest.Metadata;

namespace Millarow.Rest.Proxy
{
    public class InvocationResult
    {
        public InvocationResult(object[] args, object result, MethodMetadata methodMetadata)
        {
            Args = args;
            ReturnValue = result;
            MethodMetadata = methodMetadata;
        }

        public object[] Args { get; }

        public object ReturnValue { get; }

        public MethodMetadata MethodMetadata { get; }
    }
}
