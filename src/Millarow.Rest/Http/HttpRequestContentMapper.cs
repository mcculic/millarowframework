using Millarow.Rest.Core;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Millarow.Rest.Http
{
    public class HttpRequestContentMapper : ICompositeComponent
    {
        protected IReadOnlyCollection<IHttpRequestContentFormatter> ContentFormatters;

        public virtual void ResolveDependencies(IRestContainer container)
        {
            ContentFormatters = container.Resolve<IHttpRequestContentFormatter>().ToArray();
        }

        public virtual HttpContent MapContent(RequestContent content)
        {
            var formatter = ContentFormatters.Where(x => x.CanMapContent(content)).FirstOrDefault();
            if (formatter == null)
                throw new RestException(RestExceptionKind.Mapping, $"Formatter not found"); //TODO msg

            return formatter.MapContent(content);
        }
    }
}