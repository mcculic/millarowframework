using Millarow.Rest.Metadata.Providers;
using System;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class RequestFileAttribute : RequestParameterAttribute, IContentFileNameProvider
    {
        public override RequestParameterKind ParameterKind => RequestParameterKind.FormFile;

        public string FileName { get; set; }

        Maybe<string> IContentFileNameProvider.FileName => FileName == null ? Maybe.Nothing<string>() : FileName;
    }
}
