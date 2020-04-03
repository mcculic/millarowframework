using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Services
{
    public interface IDialogServiceToken : IDisposable
    {
        void Close();

        Task WaitForShowAsync();

        Task WaitForCloseAsync();
    }
}