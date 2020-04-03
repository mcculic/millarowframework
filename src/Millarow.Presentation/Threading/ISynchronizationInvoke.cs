using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.Threading
{
    public interface ISynchronizationInvoke
    {
        void Invoke(Action callback);

        TResult Invoke<TResult>(Func<TResult> callback);

        Task InvokeAsync(Action callback);

        Task<TResult> InvokeAsync<TResult>(Func<TResult> callback);

        bool CheckAccess();

        void VerifyAccess();
    }
}