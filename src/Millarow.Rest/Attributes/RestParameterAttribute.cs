using Millarow.Rest.Metadata.Providers;
using System;

namespace Millarow.Rest.Attributes
{
    public abstract class RestParameterAttribute : Attribute, IMetadataProvider, IParameterDefinition, IParameterNameProvider
    {
        public string Name { get; set; }

        public abstract RequestParameterKind ParameterKind { get; }

        string IParameterNameProvider.Name => Name;
    }
}