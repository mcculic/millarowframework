using Millarow.Rest.Core;
using Millarow.Rest.Serialization;
using System.Linq;
using System.Net.Http;

namespace Millarow.Rest.Http.Defaults
{
    public class DefaultHttpResponseMapper : IHttpResponseMapper, ICompositeComponent
    {
        public void ResolveDependencies(IRestContainer container)
        {
            RestSerializer = container.Required<RestSerializer>();
        }

        public virtual IResponse MapResponse(HttpResponseMessage message, IRestRequest request)
        {
            var response = new RestResponse(RestSerializer, request)
            {
                StatusCode = (int)message.StatusCode,
                StatusDescription = message.ReasonPhrase,
                Headers = message.Headers.ToRest().ToArray()
            };

            if (message.Content != null)
                response.Content = MapContent(message.Content);

            return response;
        }

        protected virtual ResponseContent MapContent(HttpContent content)
        {
            var data = content.ReadAsByteArrayAsync().Result;
            var contentType = content.Headers.ContentType.ToRest();
            var headers = content.Headers.ToRest().ToArray();

            return new ResponseContent(data, contentType, headers);
        }

        protected RestSerializer RestSerializer { get; private set; }
    }
}