namespace Millarow.Rest.Metadata.Providers
{
    public interface IParameterDefinition : IMetadataProvider
    {
        RequestParameterKind ParameterKind { get; }
    }
}