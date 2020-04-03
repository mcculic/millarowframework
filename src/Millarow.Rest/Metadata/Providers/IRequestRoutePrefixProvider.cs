namespace Millarow.Rest.Metadata.Providers
{
    public interface IRequestRoutePrefixProvider : IMetadataProvider, IMetadataProviderHierarchy
    {
        Maybe<string> RoutePrefix { get; }
    }
}
