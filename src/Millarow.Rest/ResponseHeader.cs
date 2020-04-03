using System.Diagnostics;

namespace Millarow.Rest
{
    [DebuggerDisplay("{Name}={Value}")]
    public class ResponseHeader : IRestParameter
    {
        public ResponseHeader(string name, string value)
        {
            name.AssertNotNullOrEmpty(nameof(name));
            value.AssertNotNull(nameof(value));

            Name = name;
            Value = value;
        }

        public override string ToString() => $"Header {Name}";

        public string Name { get; }

        public string Value { get; }
    }
}