using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Millarow.Rest.Http
{
    public interface IHttpExecutionStrategy
    {
        Task ExecuteAsync(Func<HttpRequestMessage> requestMessageFactory, IHttpExecutionContext context);
    }
}