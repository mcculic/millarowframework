using Millarow.Rest.Metadata.Providers;
using System;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class RestRouteAttribute : Attribute, IRequestRoutePrefixProvider
    {
        public RestRouteAttribute(string prefix)
        {
            Prefix = prefix;
        }

        public string Prefix { get; set; }

        Maybe<string> IRequestRoutePrefixProvider.RoutePrefix => Prefix;
    }
}
