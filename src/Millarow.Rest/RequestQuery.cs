using System.Diagnostics;

namespace Millarow.Rest
{
    [DebuggerDisplay("{Name}={Content}")]
    public sealed class RequestQuery : IRestParameter
    {
        public RequestQuery(string name, RestValue content)
        {
            name.AssertNotNullOrEmpty(nameof(name));

            Name = name;
            Content = content;
        }

        public override string ToString() => $"Query {Name}";

        public string Name { get; }

        public RestValue Content { get; }
    }
}