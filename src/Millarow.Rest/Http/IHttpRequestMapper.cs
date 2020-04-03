using System.Net.Http;

namespace Millarow.Rest.Http
{
    public interface IHttpRequestMapper
    {
        HttpRequestMessage MapRequest(IRestRequest request);
    }
}
