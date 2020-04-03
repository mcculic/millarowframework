using System;

namespace Millarow.Rest.Metadata.Providers
{
    public interface ITimeoutProvider : IMetadataProvider, IMetadataProviderHierarchy
    {
        Maybe<TimeSpan> Timeout { get; }
    }
}