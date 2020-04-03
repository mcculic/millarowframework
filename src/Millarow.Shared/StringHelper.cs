using System.Collections.Generic;

namespace Millarow
{
    public static class StringHelper
    {
        public static IEnumerable<string> TokenSplit(this string value, params string[] tokens)
        {
            value.AssertNotNull(nameof(value));
            tokens.AssertNotNull(nameof(tokens));

            var head = 0;

            for (int i = 0; i < value.Length; i++)
            {
                foreach (var t in tokens)
                {
                    if (value.Length - i < t.Length)
                        continue;

                    if (value.IndexOf(t, i, t.Length) == i)
                    {
                        if (head != i)
                            yield return value.Substring(head, i - head);

                        yield return t;

                        i += t.Length - 1;
                        head = i + 1;

                        break;
                    }
                }
            }

            if (head < value.Length)
                yield return value.Substring(head);
        }
    }
}
