using System;
using System.Collections.Generic;

namespace Millarow
{
    internal static class TreeHelper
    {
        public static IEnumerable<T> FlattenTreeDepthFirst<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getChildren)
        {
            source.AssertNotNull(nameof(source));
            getChildren.AssertNotNull(nameof(getChildren));

            foreach (var item in source)
            {
                yield return item;

                var children = getChildren(item);
                if (children != null)
                {
                    foreach (var c in FlattenTreeDepthFirst(children, getChildren))
                        yield return c;
                }
            }
        }

        public static IEnumerable<T> FlattenTreeBreadthFirst<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> getChildren)
        {
            source.AssertNotNull(nameof(source));
            getChildren.AssertNotNull(nameof(getChildren));

            foreach (var root in source)
            {
                var queue = new Queue<T>(getChildren(root));
                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();

                    yield return current;

                    foreach (var child in getChildren(current))
                        queue.Enqueue(child);
                }
            }
        }
    }
}
