using System;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Millarow.Presentation.Threading.WPF
{
    public class DispatcherSynchronizationInvoke : ISynchronizationInvoke
    {
        private readonly Dispatcher _dispatcher;

        public DispatcherSynchronizationInvoke(Dispatcher dispatcher)
        {
            dispatcher.AssertNotNull(nameof(dispatcher));

            _dispatcher = dispatcher;
        }

        public void Invoke(Action callback)
        {
            callback.AssertNotNull(nameof(callback));

            _dispatcher.Invoke(callback);
        }

        public TResult Invoke<TResult>(Func<TResult> callback)
        {
            callback.AssertNotNull(nameof(callback));

            return _dispatcher.Invoke(callback);
        }

        public Task InvokeAsync(Action callback)
        {
            callback.AssertNotNull(nameof(callback));

            return _dispatcher.InvokeAsync(callback).Task;
        }

        public Task<TResult> InvokeAsync<TResult>(Func<TResult> callback)
        {
            callback.AssertNotNull(nameof(callback));

            return _dispatcher.InvokeAsync(callback).Task;
        }

        public bool CheckAccess() => _dispatcher.CheckAccess();

        public void VerifyAccess() => _dispatcher.VerifyAccess();
    }
}