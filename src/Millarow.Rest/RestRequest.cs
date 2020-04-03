using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Millarow.Rest
{
    public class RestRequest : IRestRequest
    {
        private List<RequestHeader> _headers;
        private List<RequestQuery> _queries;
        private List<RequestRouteSegment> _routeSegments;

        public RestRequest()
        {
        }

        public RestRequest(RequestMethod method, string route = null)
        {
            Method = method;
            Route = route;
        }

        public void AddHeader(RequestHeader header)
        {
            header.AssertNotNull(nameof(header));

            if (_headers == null)
                _headers = new List<RequestHeader>();

            _headers.Add(header);
        }

        public void AddHeader<T>(string name, T value)
        {
            name.AssertNotNullOrEmpty(nameof(name));

            AddHeader(new RequestHeader(name, RestValue.Create(value)));
        }

        public void AddRouteSegment(RequestRouteSegment segment)
        {
            segment.AssertNotNull(nameof(segment));

            if (_routeSegments == null)
                _routeSegments = new List<RequestRouteSegment>();

            _routeSegments.Add(segment);
        }

        public void AddRouteSegment<T>(string name, T value)
        {
            name.AssertNotNullOrEmpty(nameof(name));

            AddRouteSegment(new RequestRouteSegment(name, RestValue.Create(value)));
        }

        public void AddQuery(RequestQuery query)
        {
            query.AssertNotNull(nameof(query));

            if (_queries == null)
                _queries = new List<RequestQuery>();

            _queries.Add(query);
        }

        public void AddQuery<T>(string name, T value)
        {
            name.AssertNotNullOrEmpty(nameof(name));

            AddQuery(new RequestQuery(name, RestValue.Create(value)));
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Route))
                return Method.ToString().ToUpperInvariant();

            return $"{Method.ToString().ToUpperInvariant()} {Route}";
        }

        public RequestMethod Method { get; set; }

        public string Route  { get; set; }

        public TimeSpan? Timeout { get; set; }

        public RequestContent Content { get; set; }

        public CultureInfo CultureInfo { get; set; }

        public IEnumerable<RequestHeader> Headers => _headers?.AsEnumerable() ?? Enumerable.Empty<RequestHeader>();

        public IEnumerable<RequestRouteSegment> RouteSegments => _routeSegments?.AsEnumerable() ?? Enumerable.Empty<RequestRouteSegment>();
        
        public IEnumerable<RequestQuery> Queries => _queries?.AsEnumerable() ?? Enumerable.Empty<RequestQuery>();
    }
}