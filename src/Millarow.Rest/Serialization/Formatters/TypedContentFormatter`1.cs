using System;
using System.Collections.Generic;
using System.Linq;

namespace Millarow.Rest.Serialization.Formatters
{
    public abstract class TypedContentFormatter<T> : IRestContentFormatter
    {
        public TypedContentFormatter(IEnumerable<MimeType> supportedMediaTypes)
        {
            supportedMediaTypes.AssertNotNull(nameof(supportedMediaTypes));

            SupportedMediaTypes = supportedMediaTypes;
        }

        public abstract byte[] Serialize(ContentType contentType, T value);

        public abstract T Deserialize(byte[] contentData, ContentType contentType);

        public virtual bool CanDeserialize(ContentType contentType)
            => IsSupportedMediaType(contentType.MediaType);

        public virtual bool CanSerialize(ContentType contentType, T value) 
            => IsSupportedMediaType(contentType.MediaType);

        private bool IsSupportedMediaType(MimeType mediaType)
            => SupportedMediaTypes.Any(x => x.Match(mediaType));

        public IEnumerable<MimeType> SupportedMediaTypes { get; }

        bool IRestContentFormatter.CanSerialize(ContentType contentType, RestValue value)
            => value.Type == typeof(T) && CanSerialize(contentType, (T)value.Value);

        bool IRestContentFormatter.CanDeserialize(ContentType contentType, Type valueType)
            => valueType == typeof(T) && CanDeserialize(contentType);

        byte[] IRestContentFormatter.Serialize(ContentType contentType, RestValue value)
        {
            var valueType = value.Type;

            if (!typeof(T).IsAssignableFrom(valueType) || valueType.IsInstanceOfType(value))
                throw new ArgumentException("Invalid value type"); //TODO msg

            return Serialize(contentType, (T)value.Value);
        }

        object IRestContentFormatter.Deserialize(byte[] contentData, ContentType contentType, Type valueType)
        {
            if (valueType != typeof(T))
                throw new ArgumentException("Invalid value type"); //TODO msg

            return Deserialize(contentData, contentType);
        }
    }
}
