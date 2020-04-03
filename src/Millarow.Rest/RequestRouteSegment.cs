using System.Diagnostics;

namespace Millarow.Rest
{
    [DebuggerDisplay("{Name}={Content}")]
    public sealed class RequestRouteSegment : IRestParameter
    {
        public RequestRouteSegment(string name, RestValue content)
        {
            name.AssertNotNullOrEmpty(nameof(name));
            content.AssertNotNull(nameof(content));

            Name = name;
            Content = content;
        }

        public override string ToString() => $"RouteSegment {Name}";

        public string Name { get; }

        public RestValue Content { get; }
    }
}
