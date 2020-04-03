using Millarow.Rest.Metadata.Providers;
using System;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class RestMethodAttribute : Attribute, IRequestMethodProvider, IRequestRouteProvider
    {
        protected RestMethodAttribute(RequestMethod method, string route)
        {
            Method = method;
            Route = route;
        }

        public RequestMethod Method { get; }

        public string Route { get; }

        Maybe<RequestMethod> IRequestMethodProvider.RequestMethod => Method;

        Maybe<string> IRequestRouteProvider.Route => Route == null ? Maybe.Nothing<string>() : Route;
    }

    public sealed class RestGetAttribute : RestMethodAttribute
    {
        public RestGetAttribute(string route = null) : base(RequestMethod.Get, route)
        {
        }
    }

    public sealed class RestHeadAttribute : RestMethodAttribute
    {
        public RestHeadAttribute(string route = null) : base(RequestMethod.Head, route)
        {
        }
    }

    public sealed class RestPostAttribute : RestMethodAttribute
    {
        public RestPostAttribute(string route = null) : base(RequestMethod.Post, route)
        {
        }
    }

    public sealed class RestPutAttribute : RestMethodAttribute
    {
        public RestPutAttribute(string route = null) : base(RequestMethod.Put, route)
        {
        }
    }

    public sealed class RestPatchAttribute : RestMethodAttribute
    {
        public RestPatchAttribute(string route = null) : base(RequestMethod.Patch, route)
        {
        }
    }

    public sealed class RestDeleteAttribute : RestMethodAttribute
    {
        public RestDeleteAttribute(string route = null) : base(RequestMethod.Delete, route)
        {
        }
    }

    public sealed class RestOptionsAttribute : RestMethodAttribute
    {
        public RestOptionsAttribute(string route = null) : base(RequestMethod.Options, route)
        {
        }
    }

    public sealed class RestTraceAttribute : RestMethodAttribute
    {
        public RestTraceAttribute(string route = null) : base(RequestMethod.Trace, route)
        {
        }
    }
}
