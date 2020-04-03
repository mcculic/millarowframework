namespace Millarow.Rest.Metadata.Providers
{
    public interface IParameterNameProvider : IMetadataProvider
    {
        string Name { get; }
    }
}
