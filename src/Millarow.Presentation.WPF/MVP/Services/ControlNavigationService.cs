using Millarow.Presentation.MVP.Services;
using Millarow.Presentation.MVP.Views;
using Millarow.Presentation.Threading;
using Millarow.Presentation.WPF.MVP.Framework;
using Millarow.Threading;
using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.WPF.MVP.Services
{
    //TODO rename
    [Obsolete("rename me")]
    public class ControlNavigationService : INavigationService
    {
        private readonly IScreenNavigationHost _host;
        private readonly ISynchronizationInvoke _syncInvoke;

        public ControlNavigationService(IScreenNavigationHost host, ISynchronizationInvoke syncInvoke)
        {
            host.AssertNotNull(nameof(host));
            syncInvoke.AssertNotNull(nameof(syncInvoke));
            
            _host = host;
            _syncInvoke = syncInvoke;
        }

        public INavigationToken Navigate(IView view)
        {
            view.AssertNotNull(nameof(view));

            _syncInvoke.VerifyAccess();

            var token = new Token(_host, view, _syncInvoke);
            token.Navigate();

            return token;
        }

        private class Token : INavigationToken
        {
            private readonly IScreenNavigationHost _host;
            private readonly IView _view;
            private IView _prev; //TODO view type? IScreenView?
            private readonly ISynchronizationInvoke _syncInvoke;
            private readonly AsyncOperationLock _closeLock;

            public Token(IScreenNavigationHost host, IView view, ISynchronizationInvoke syncInvoke)
            {
                _host = host;
                _view = view;
                _syncInvoke = syncInvoke;
                _closeLock = new AsyncOperationLock();
            }

            public void Navigate()
            {
                _prev = _host.CurrentView;
                _host.CurrentView = _view;
            }

            public void Close()
            {
                _syncInvoke.VerifyAccess();

                if (!Equals(_host.CurrentView, _view))
                    throw new InvalidOperationException("View is not current"); //TODO message

                _host.CurrentView = _prev;
                _closeLock.SetCompleted();
            }

            public Task WaitForNavigateAsync()
            {
                return Task.CompletedTask;
            }

            public Task WaitForCloseAsync()
            {
                return _closeLock.WaitCompleteAsync();
            }

            public void Dispose()
            {
                _closeLock.Dispose();
            }
        }
    }
}
