using System;
using System.Net.Http;

namespace Millarow.Rest.Core
{
    public interface IHttpExecutionLogger
    {
        void LogRequest(HttpRequestMessage request);

        void LogRequestException(HttpRequestMessage request, Exception exception);

        void LogResponse(HttpResponseMessage response);

        void LogResponseException(HttpResponseMessage response, Exception exception);
    }
}
