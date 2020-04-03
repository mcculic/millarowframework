namespace Millarow.Rest.Metadata.Providers
{
    public interface IOmitDefaultProvider : IMetadataProvider
    {
        Maybe<bool> OmitDefault { get; }
    }
}
