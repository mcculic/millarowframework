using System.Net.Http;

namespace Millarow.Rest.Http
{
    public interface IHttpRequestContentFormatter
    {
        bool CanMapContent(RequestContent content);

        HttpContent MapContent(RequestContent content);
    }
}