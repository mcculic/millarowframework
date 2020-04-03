using System;
using System.Runtime.Serialization;

namespace Millarow.Rest.Serialization
{
    [Serializable]
    public class RestSerializationException : RestException
    {
        public RestSerializationException()
            : base(RestExceptionKind.Serialization)
        {
        }

        public RestSerializationException(string message)
            : base(RestExceptionKind.Serialization, message)
        {
        }

        public RestSerializationException(string message, Exception inner)
            : base(RestExceptionKind.Serialization, message, inner)
        {
        }

        protected RestSerializationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
