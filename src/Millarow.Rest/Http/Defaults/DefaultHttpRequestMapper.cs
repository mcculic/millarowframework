using Millarow.Rest.Core;
using Millarow.Rest.Serialization;
using System.Net.Http;

namespace Millarow.Rest.Http.Defaults
{
    public class DefaultHttpRequestMapper : IHttpRequestMapper, ICompositeComponent
    {
        public void ResolveDependencies(IRestContainer container)
        {
            ContentMapper = container.Required<HttpRequestContentMapper>();
            RouteRenderer = container.Required<RestRouteRenderer>();
            RestSerializer = container.Required<RestSerializer>();
        }

        public HttpRequestMessage MapRequest(IRestRequest request)
        {
            var url = RouteRenderer.Render(request.Route, request.RouteSegments, request.Queries, request.CultureInfo);
            var requestMessage = new HttpRequestMessage(request.Method.ToHttp(), url);

            if (request.Content != null)
                requestMessage.Content = ContentMapper.MapContent(request.Content);

            foreach (var header in request.Headers)
            {
                var headerValue = RestSerializer.SerializeQueryValue(header.Content, request.CultureInfo);
                
                requestMessage.Headers.Add(header.Name, headerValue);
            }

            return requestMessage;
        }

        protected HttpRequestContentMapper ContentMapper { get; private set; }

        protected RestSerializer RestSerializer { get; private set; }

        protected RestRouteRenderer RouteRenderer { get; private set; }
    }
}
