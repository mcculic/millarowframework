namespace Millarow.Rest.Metadata.Providers
{
    public interface IRequestContentKindProvider : IMetadataProvider
    {
        Maybe<RequestBodyType> GetKind();
    }
}
