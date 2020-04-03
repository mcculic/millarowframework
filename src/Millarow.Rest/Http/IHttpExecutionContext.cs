using System.Net.Http;
using System.Threading;

namespace Millarow.Rest.Http
{
    public interface IHttpExecutionContext
    {
        void SuccessResult(HttpResponseMessage responseMessage);

        void ErrorResult(string errorMessage);

        void TimeoutResult();

        HttpMessageInvoker Invoker { get; set; }

        CancellationToken CancellationToken { get; set; }
    }
}
