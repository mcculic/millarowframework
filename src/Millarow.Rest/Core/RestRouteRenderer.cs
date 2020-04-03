using Millarow.Rest.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Millarow.Rest.Core
{
    public class RestRouteRenderer : ICompositeComponent
    {
        public void ResolveDependencies(IRestContainer container)
        {
            RestSerializer = container.Required<RestSerializer>();
        }

        public string Render(string route, IEnumerable<RequestRouteSegment> segments, IEnumerable<RequestQuery> queries, CultureInfo cultureInfo)
        {
            //TODO checks null
            var url = new StringBuilder(route);

            //todo validate for token existance in template
            foreach (var segment in segments)
                RenderSegment(segment, url, cultureInfo);

            if (queries.Any() == true)
            {
                url.Append("?");
                url.Append(string.Join("&", queries.Select(x => $"{x.Name}={FormatQueryValue(x.Content, cultureInfo)}")));
            }

            return url.ToString();
        }

        protected virtual void RenderSegment(RequestRouteSegment segment, StringBuilder result, CultureInfo cultureInfo)
        {
            var token = "{" + segment.Name + "}";
            var value = FormatSegmentValue(segment.Content, cultureInfo);

            result.Replace(token, value);
        }

        private string FormatSegmentValue(RestValue value, CultureInfo cultureInfo)
        {
            var stringValue = RestSerializer.SerializeQueryValue(value, cultureInfo);
            if (stringValue == null)
                return null;

            return Uri.EscapeDataString(stringValue);
        }

        private string FormatQueryValue(RestValue value, CultureInfo cultureInfo)
        {
            var stringValue = RestSerializer.SerializeQueryValue(value, cultureInfo);
            if (stringValue == null)
                return null;

            return Uri.EscapeDataString(stringValue);
        }

        protected RestSerializer RestSerializer { get; private set; }
    }
}
