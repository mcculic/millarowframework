using System.Net.Http;

namespace Millarow.Rest.Http
{
    public interface IHttpResponseMapper
    {
        IResponse MapResponse(HttpResponseMessage message, IRestRequest request);
    }
}