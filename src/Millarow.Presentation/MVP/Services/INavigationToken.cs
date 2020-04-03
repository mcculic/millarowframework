using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Services
{
    public interface INavigationToken : IDisposable
    {
        void Close();

        Task WaitForNavigateAsync();

        Task WaitForCloseAsync();
    }
}
