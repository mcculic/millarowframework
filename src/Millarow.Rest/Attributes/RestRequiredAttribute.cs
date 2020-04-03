using Millarow.Rest.Metadata.Providers;
using System;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
    public sealed class RestRequiredAttribute : Attribute, IIsRequiredProvider
    {
        public bool IsRequired { get; set; } = true;

        Maybe<bool> IIsRequiredProvider.IsRequired => IsRequired;
    }
}
