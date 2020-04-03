using System;
using System.Net.Http;
using System.Threading;

namespace Millarow.Rest.Http
{
    internal class HttpExecutionContext : IHttpExecutionContext
    {
        public void SuccessResult(HttpResponseMessage responseMessage)
        {
            responseMessage.AssertNotNull(nameof(responseMessage));

            if (ResponseMessage != null)
                throw new InvalidOperationException();

            ResponseMessage = responseMessage;
            IsError = false;
            IsTimeout = false;
        }

        public void ErrorResult(string errorMessage)
        {
            errorMessage.AssertNotNull(nameof(errorMessage));

            ErrorMessage = errorMessage;
            IsError = true;
        }

        public void TimeoutResult()
        {
            IsTimeout = true;
        }

        public HttpMessageInvoker Invoker { get; set; }

        public CancellationToken CancellationToken { get; set; }

        public HttpResponseMessage ResponseMessage { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsError { get; set; }

        public bool IsTimeout { get; set; }
    }
}
