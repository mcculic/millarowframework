using System;
using System.Linq;
using System.Reflection;

namespace Millarow.Reflection
{
    public static class ConstructorInfoHelper
    {
        public static bool IsParameterless(this ConstructorInfo info)
        {
            info.AssertNotNull(nameof(info));

            return !info.GetParameters().Any();
        }

        public static bool HasParameterlessConstructor(this Type type)
        {
            type.AssertNotNull(nameof(type));

            return type.GetConstructors().Any(x => x.IsParameterless());
        }
    }
}
