namespace Millarow.Rest.Metadata.Providers
{
    public interface IStringFormatProvider : IMetadataProvider
    {
        Maybe<string> Format { get; }
    }
}
