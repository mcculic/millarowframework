namespace Millarow.Rest.Metadata.Providers
{
    public interface IContentMediaTypeProvider : IMetadataProvider, IMetadataProviderHierarchy
    {
        Maybe<MimeType> MediaType { get; }
    }
}
