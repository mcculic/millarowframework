using Millarow.Rest.Metadata;

namespace Millarow.Rest.ModelBinding.Binders
{
    public interface IResponseBindingContext
    {
        ResponseMetadata Metadata { get; }
        
        object Model { get; }

        void SetPropertyValue(string propertyName, object value);
    }
}