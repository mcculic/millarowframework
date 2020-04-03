using System;
using System.Text.RegularExpressions;

namespace Millarow.Rest
{
    public class MimeType : IEquatable<MimeType>
    {
        public MimeType(string value)
        {
            value.AssertNotNull(nameof(value));

            Value = value;
        }

        public bool Match(string name)
        {
            name.AssertNotNull(nameof(name));

            if (Value == name)
                return true;

            var pattern = "^" + Regex.Escape(Value).Replace("\\*", ".*") + "$";

            return Regex.IsMatch(name, pattern);
        }

        public bool Equals(MimeType other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is MimeType other ? Equals(other) : false;
        }

        public override string ToString()
        {
            return Value;
        }

        public override int GetHashCode()
        {
            return Value == null ? 0 : Value.GetHashCode();
        }

        public string Value { get; }

        public bool IsEmpty => string.IsNullOrEmpty(Value);

        public static implicit operator MimeType(string name) => name == null ? null : new MimeType(name);

        public static implicit operator string(MimeType mimeType) => mimeType?.Value;
    }
}