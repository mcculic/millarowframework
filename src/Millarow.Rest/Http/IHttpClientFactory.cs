using System.Net.Http;

namespace Millarow.Rest.Http
{
    public interface IHttpClientFactory
    {
        HttpClient CreateClient();
    }
}
