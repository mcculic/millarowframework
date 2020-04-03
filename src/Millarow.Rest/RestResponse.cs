using Millarow.Rest.Serialization;
using System;
using System.Collections.Generic;

namespace Millarow.Rest
{
    public class RestResponse : IResponse
    {
        public RestResponse(RestSerializer restSerializer, IRestRequest request)
        {
            restSerializer.AssertNotNull(nameof(restSerializer));
            request.AssertNotNull(nameof(request));

            RestSerializer = restSerializer;
            Request = request;
        }

        public object ReadContentAs(Type targetType)
        {
            if (Content == null)
                throw new InvalidOperationException("Content is null"); //TODO msg

            var contentData = Content.ReadAsArray();

            return RestSerializer.DeserializeContent(contentData, Content.ContentType, targetType);
        }

        public T ReadContentAs<T>()
        {
            return (T)ReadContentAs(typeof(T));
        }

        private RestSerializer RestSerializer { get; }

        public IRestRequest Request { get; }

        public int StatusCode { get; set; }

        public string StatusDescription { get; set; }

        public IEnumerable<ResponseHeader> Headers { get; set; }

        public ResponseContent Content { get; set; }
    }
}
