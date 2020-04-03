using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Millarow.Rest.Core
{
    public class BindingPath : IEquatable<BindingPath>, IEnumerable<string>
    {
        private readonly IReadOnlyList<string> _path;

        public BindingPath(IReadOnlyList<string> path)
        {
            path.AssertNotNull(nameof(path));

            _path = path;
        }

        public BindingPath(params string[] path)
        {
            path.AssertNotNull(nameof(path));

            _path = path;
        }

        public BindingPath Append(string propertyName)
        {
            return new BindingPath(_path.Append(propertyName).ToArray());
        }

        public bool Equals(BindingPath other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(_path, other._path))
                return true;

            return _path.SequenceEqual(other._path);
        }

        public override bool Equals(object obj)
            => obj is BindingPath other ? Equals(other) : false;

        public override int GetHashCode()
            => _path.Aggregate(0, (acc, x) => acc ^ x.GetHashCode());

        public override string ToString()
            => string.Join(".", _path);

        public IEnumerator<string> GetEnumerator() => _path.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Length => _path.Count;

        public bool IsEmpty => !_path.Any();

        public string this [int index] => _path[index];

        public static BindingPath Empty { get; } = new BindingPath(Array.Empty<string>());
    }
}