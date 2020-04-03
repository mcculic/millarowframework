using Millarow.Rest.Core;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Millarow.Rest.Http.Defaults
{
    internal class DefaultHttpExecutionStrategy : IHttpExecutionStrategy, ICompositeComponent
    {
        public async Task ExecuteAsync(Func<HttpRequestMessage> requestMessageFactory, IHttpExecutionContext context)
        {
            for (int retry = 0; retry <= MaxRetryCount; retry++)
            {
                var request = requestMessageFactory();
                Logger.LogRequest(request);

                try
                {
                    var response = await context.Invoker.SendAsync(request, context.CancellationToken);
                    Logger.LogResponse(response);
                    ResponseValidator?.Invoke(response);

                    context.SuccessResult(response);
                    break;
                }
                catch (OperationCanceledException) when (!context.CancellationToken.IsCancellationRequested)
                {
                    context.TimeoutResult();
                }
                catch (HttpRequestException ex)
                {
                    Logger.LogRequestException(request, ex);
                    context.ErrorResult(ex.Message);

                    if (PostErrorDelay != TimeSpan.Zero)
                        await Task.Delay(PostErrorDelay);
                }
                finally
                {
                    request?.Dispose();
                }
            }
        }

        public void ResolveDependencies(IRestContainer container)
        {
            Logger = container.Optional<IHttpExecutionLogger>();
        }

        public int MaxRetryCount { get; set; }

        public TimeSpan PostErrorDelay { get; set; }

        public Action<HttpResponseMessage> ResponseValidator { get; set; }

        public IHttpExecutionLogger Logger { get; set; }
    }
}
