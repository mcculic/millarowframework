using Millarow.Rest.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Millarow.Rest.Serialization
{
    public class RestSerializer : ICompositeComponent
    {
        public virtual void ResolveDependencies(IRestContainer container)
        {
            ContentFormatters = container.Resolve<IRestContentFormatter>().ToArray();
            RequestQueryValueFormatters = container.Resolve<RequestQueryValueFormatter>().ToArray();
            RequestRouteValueFormatters = container.Resolve<RequestRouteValueFormatter>().ToArray();
        }

        public virtual string SerializeQueryValue(RestValue value, CultureInfo cultureInfo)
        {
            var formatter = RequestQueryValueFormatters.FirstOrDefault(x => x.CanSerialize(value));
            if (formatter == null)
                throw new RestException(RestExceptionKind.Serialization, $"Query value '{value.Type}' formatter not found");

            return formatter.Serialize(value, cultureInfo);
        }

        public virtual string SerializeRouteValue(RestValue value, CultureInfo cultureInfo)
        {
            var formatter = RequestRouteValueFormatters.FirstOrDefault(x => x.CanSerialize(value));
            if (formatter == null)
                throw new RestException(RestExceptionKind.Serialization, $"Route value '{value.Type}' formatter not found");

            return formatter.Serialize(value, cultureInfo);
        }

        public virtual byte[] SerializeContent(ContentType contentType, RestValue value)
        {
            var formatter = ContentFormatters.FirstOrDefault(x => x.CanSerialize(contentType, value));
            if (formatter == null)
                throw new RestException(RestExceptionKind.Serialization, $"Content '{value.Type}' formatter not found for media type '{contentType.MediaType}'");

            return formatter.Serialize(contentType, value);
        }

        //TODO byte[] contentData -> Stream content
        public virtual object DeserializeContent(byte[] contentData, ContentType contentType, Type valueType)
        {
            var formatter = ContentFormatters.FirstOrDefault(x => x.CanDeserialize(contentType, valueType));
            if (formatter == null)
                throw new RestException(RestExceptionKind.Serialization, $"'{valueType}' formatter not found for media type '{contentType.MediaType}'");

            return formatter.Deserialize(contentData, contentType, valueType);
        }

        protected IEnumerable<IRestContentFormatter> ContentFormatters { get; set; }
        
        protected IEnumerable<RequestQueryValueFormatter> RequestQueryValueFormatters { get; set; }

        protected IEnumerable<RequestRouteValueFormatter> RequestRouteValueFormatters { get; set; }
    }
}
