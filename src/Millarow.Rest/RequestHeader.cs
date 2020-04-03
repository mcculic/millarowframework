using System.Diagnostics;

namespace Millarow.Rest
{
    [DebuggerDisplay("{Name}={Content}")]
    public class RequestHeader : IRestParameter
    {
        public RequestHeader(string name, RestValue content)
        {
            name.AssertNotNullOrEmpty(nameof(name));
            content.AssertNotNull(nameof(content));

            Name = name;
            Content = content;
        }

        public override string ToString() => $"Header {Name}";

        public string Name { get; }

        public RestValue Content { get; }
    }
}