using Millarow.Rest.Metadata;
using Millarow.Rest.ModelBinding.Binders;
using System;

namespace Millarow.Rest.ModelBinding
{
    public class ResponseBindingContext : IResponseBindingContext
    {
        public ResponseBindingContext(ResponseMetadata metadata, object model, IResponse response)
        {
            metadata.AssertNotNull(nameof(metadata));
            model.AssertNotNull(nameof(model));

            Metadata = metadata;
            Model = model;
            Response = response;
        }

        public void SetPropertyValue(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public ResponseMetadata Metadata { get; }

        public IResponse Response { get; }

        public object Model { get; }
    }
}
