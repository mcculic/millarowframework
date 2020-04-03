using Millarow.Rest.Metadata.Providers;

namespace Millarow.Rest.Attributes
{
    public abstract class ResponseParameterAttribute : RestParameterAttribute, IIsRequiredProvider
    {
        public bool IsRequired { get; set; }

        Maybe<bool> IIsRequiredProvider.IsRequired => IsRequired;
    }
}