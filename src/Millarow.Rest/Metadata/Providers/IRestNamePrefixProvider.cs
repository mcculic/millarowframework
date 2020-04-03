namespace Millarow.Rest.Metadata.Providers
{
    public interface IRestNamePrefixProvider : IMetadataProvider
    {
        string Prefix { get; }
    }
}
