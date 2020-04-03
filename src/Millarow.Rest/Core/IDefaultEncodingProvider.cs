using System.Text;

namespace Millarow.Rest.Core
{
    public interface IDefaultEncodingProvider
    {
        Encoding Encoding { get; }
    }
}
