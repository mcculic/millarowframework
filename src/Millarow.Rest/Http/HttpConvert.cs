using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Millarow.Rest.Http
{
    public static class HttpConvert
    {
        public static HttpMethod ToHttp(this RequestMethod from)
        {
            if (_methods.TryGetValue(from, out var result))
                return result;

            throw new ArgumentException($"Unknown {nameof(RequestMethod)} value '{from}'");
        }

        public static ContentType ToRest(this MediaTypeHeaderValue from)
        {
            if (from == null)
                return null;

            return new ContentType(from.MediaType, from.CharSet);
        }

        public static IEnumerable<ResponseHeader> ToRest(this HttpHeaders from)
        {
            if (from == null)
                return Enumerable.Empty<ResponseHeader>();

            return from.SelectMany(h => h.Value.Select(v => new ResponseHeader(h.Key, $"{string.Join("; ", h.Value)}")));
        }

        private static readonly IReadOnlyDictionary<RequestMethod, HttpMethod> _methods = new Dictionary<RequestMethod, HttpMethod>
        {
            [RequestMethod.Get] = HttpMethod.Get,
            [RequestMethod.Head] = HttpMethod.Head,
            [RequestMethod.Post] = HttpMethod.Post,
            [RequestMethod.Put] = HttpMethod.Put,
            [RequestMethod.Patch] = new HttpMethod("PATCH"),
            [RequestMethod.Delete] = HttpMethod.Delete,
            [RequestMethod.Options] = HttpMethod.Options,
            [RequestMethod.Trace] = HttpMethod.Trace,
        };
    }
}
