namespace Millarow.Rest.Metadata.Providers
{
    public interface IContentFileNameProvider : IMetadataProvider
    {
        Maybe<string> FileName { get; }
    }
}
