using System;
using System.Runtime.Serialization;

namespace Millarow.Rest
{
    [Serializable]
    public enum RestExceptionKind
    {
        Http,
        Timeout,
        Mapping,
        Validation,
        Configuration,
        Serialization
    }

    [Serializable]
    public class RestException : Exception
    {
        public RestException(RestExceptionKind kind)
        {
            Kind = kind;
        }

        public RestException(RestExceptionKind kind, string message)
            : base(message)
        {
            Kind = kind;
        }

        public RestException(RestExceptionKind kind, string message, Exception inner)
            : base(message, inner)
        {
            Kind = kind;
        }

        protected RestException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
            Kind = (RestExceptionKind)info.GetValue(nameof(Kind), typeof(RestExceptionKind));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Kind), Kind);

            base.GetObjectData(info, context);
        }

        public RestExceptionKind Kind { get; }
    }
}
