namespace Millarow.Rest.Metadata.Providers
{
    public interface IRequestMethodProvider : IMetadataProvider
    {
        Maybe<RequestMethod> RequestMethod { get; }
    }
}
