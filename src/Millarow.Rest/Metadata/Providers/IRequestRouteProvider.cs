namespace Millarow.Rest.Metadata.Providers
{
    public interface IRequestRouteProvider : IMetadataProvider
    {
        Maybe<string> Route { get; }
    }
}
