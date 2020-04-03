using System.Collections.Generic;

namespace Millarow
{
    internal static class EnumerableHelper
    {
        public static IEnumerable<T> Append<T>(this IEnumerable<T> items, T item)
        {
            if (items != null)
                foreach (var i in items)
                    yield return i;

            yield return item;
        }
    }
}
