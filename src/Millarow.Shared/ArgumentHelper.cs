using System;

namespace Millarow
{
    internal static class ArgumentHelper
    {
        public static bool In<T>(this T value, params T[] items)
        {
            if (items == null)
                return false;

            return Array.IndexOf(items, value) != -1;
        }
    }
}