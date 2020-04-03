using Millarow.Rest.Metadata.Providers;
using System;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method, AllowMultiple = false,   Inherited = true)]
    public class NamePrefixAttribute : Attribute, IRestNamePrefixProvider
    {
        public NamePrefixAttribute(string namePrefix)
        {
            Prefix = namePrefix;
        }

        public string Prefix { get; }
    }
}
