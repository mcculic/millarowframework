using System;
using System.Collections.Generic;
using System.Globalization;

namespace Millarow.Rest
{
    public interface IRestRequest
    {
        void AddHeader(RequestHeader header);

        void AddHeader<T>(string name, T value);

        void AddRouteSegment(RequestRouteSegment segment);

        void AddRouteSegment<T>(string name, T value);

        void AddQuery(RequestQuery query);

        void AddQuery<T>(string name, T value);

        RequestMethod Method { get; }

        string Route { get; set; }

        TimeSpan? Timeout { get; set; }

        CultureInfo CultureInfo { get; }

        RequestContent Content { get; set; }

        IEnumerable<RequestHeader> Headers { get; }

        IEnumerable<RequestRouteSegment> RouteSegments { get; }

        IEnumerable<RequestQuery> Queries { get; }
    }
}