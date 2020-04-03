using Millarow.Presentation.MVP.Services;
using Millarow.Presentation.MVP.Views;
using Millarow.Presentation.Threading;
using Millarow.Presentation.WPF.MVP.Framework;
using Millarow.Threading;
using System.Threading.Tasks;

namespace Millarow.Presentation.WPF.MVP.Services
{
    public class ChildWindowDialogService : IDialogService
    {
        private readonly IChildWindowDialogHost _host;
        private readonly ISynchronizationInvoke _syncInvoke;

        public ChildWindowDialogService(IChildWindowDialogHost host, ISynchronizationInvoke syncInvoke)
        {
            host.AssertNotNull(nameof(host));
            syncInvoke.AssertNotNull(nameof(syncInvoke));

            _host = host;
            _syncInvoke = syncInvoke;
        }

        public IDialogServiceToken ShowDialog(IView view)
        {
            view.AssertNotNull(nameof(view));

            _syncInvoke.VerifyAccess();

            var token = new Token(_host, _syncInvoke, view);
            token.Show();

            return token;
        }

        private class Token : IDialogServiceToken
        {
            private readonly IChildWindowDialogHost _host;
            private readonly ISynchronizationInvoke _syncInvoke;
            private readonly IView _view; //TODO IDialogView?
            private readonly AsyncOperationLock _closeLock;
            private bool _closed;

            public Token(IChildWindowDialogHost host, ISynchronizationInvoke syncInvoke, IView view)
            {
                _host = host;
                _syncInvoke = syncInvoke;
                _view = view;
                _closeLock = new AsyncOperationLock();
            }

            public void Show()
            {
                _host.AddDialogView(_view);
            }

            public void Close()
            {
                _syncInvoke.VerifyAccess();

                if (!_closed)
                {
                    _host.RemoveDialogView(_view);
                    _closed = true;
                    _closeLock.SetCompleted();
                }
            }

            public Task WaitForShowAsync() => Task.CompletedTask;

            public Task WaitForCloseAsync() => _closeLock.WaitCompleteAsync();

            public void Dispose() => _closeLock.Dispose();
        }
    }
}