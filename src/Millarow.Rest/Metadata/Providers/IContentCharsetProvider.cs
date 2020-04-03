namespace Millarow.Rest.Metadata.Providers
{
    public interface IContentCharSetProvider : IMetadataProvider, IMetadataProviderHierarchy
    {
        Maybe<string> CharSet { get; }
    }
}
