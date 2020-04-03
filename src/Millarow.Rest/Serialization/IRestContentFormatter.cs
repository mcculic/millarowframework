using System;

namespace Millarow.Rest.Serialization
{
    public interface IRestContentFormatter
    {
        bool CanSerialize(ContentType contentType, RestValue value);

        bool CanDeserialize(ContentType contentType, Type valueType);

        byte[] Serialize(ContentType contentType, RestValue value);

        object Deserialize(byte[] contentData, ContentType contentType, Type valueType);
    }
}
