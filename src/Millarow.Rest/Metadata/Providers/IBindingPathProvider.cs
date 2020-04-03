using Millarow.Rest.Core;

namespace Millarow.Rest.Metadata.Providers
{
    public interface IBindingPathProvider : IMetadataProvider
    {
        BindingPath BindingPath { get; }
    }
}
