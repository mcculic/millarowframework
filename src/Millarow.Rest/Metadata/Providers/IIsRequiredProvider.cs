namespace Millarow.Rest.Metadata.Providers
{
    public interface IIsRequiredProvider : IMetadataProvider
    {
        Maybe<bool> IsRequired { get; }
    }
}
