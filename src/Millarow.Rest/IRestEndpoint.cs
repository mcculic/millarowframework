using System.Threading;
using System.Threading.Tasks;

namespace Millarow.Rest
{
    public interface IRestEndpoint
    {
        Task<IResponse> ExecuteAsync(IRestRequest request, CancellationToken cancellationToken);
    }
}