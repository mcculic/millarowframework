using Millarow.Presentation.MVP.Services;
using Millarow.Presentation.MVP.Views;
using Millarow.Presentation.WPF.MVP.Framework;
using Millarow.Threading;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Millarow.Presentation.WPF.MVP.Services
{
    public class PlatformDialogService : IDialogService
    {
        private readonly IPlatformWindowHost _host;
        private readonly IOwnerWindowProvider _ownerWindowProvider;
        
        public PlatformDialogService(IPlatformWindowHost host, IOwnerWindowProvider ownerWindowProvider)
        {
            host.AssertNotNull(nameof(host));
            ownerWindowProvider.AssertNotNull(nameof(ownerWindowProvider));

            _host = host;
            _ownerWindowProvider = ownerWindowProvider;
        }

        public IDialogServiceToken ShowDialog(IView view)
        {
            view.AssertNotNull(nameof(view));

            var owner = _ownerWindowProvider.GetOwnerWindow();
            var window = _host.CreateWindow(view);
            window.Owner = owner;
            window.WindowStartupLocation = owner == null ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner;

            var token = new Token(_host, window, view);
            token.Show();

            return token;
        }

        private class Token : IDialogServiceToken
        {
            private readonly IPlatformWindowHost _host;
            private readonly Window _window;
            private readonly IView _view; //TODO IDialogView
            private bool _closed;
            private AsyncOperationLock _showLock, _closeLock; //TODO сделать с одним локом с чем-то вроде Reset?

            public Token(IPlatformWindowHost host, Window window, IView view)
            {
                _host = host;
                _window = window;
                _view = view;
                _showLock = new AsyncOperationLock();
                _closeLock = new AsyncOperationLock();
            }

            public void Show()
            {
                _window.Closed += _window_Closed;
                _window.Loaded += _window_Loaded;

                _window.Dispatcher.InvokeAsync(() =>
                {
                    try
                    {
                        _window.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        if (!_closed)
                            _showLock.SetException(ex);
                    }
                });
            }

            public void Close()
            {
                if (!_closed)
                    _host.CloseWindow(_window);
            }

            public Task WaitForShowAsync() => _showLock.WaitCompleteAsync();

            public Task WaitForCloseAsync() => _closeLock.WaitCompleteAsync();

            private void _window_Loaded(object sender, RoutedEventArgs e)
            {
                _window.Loaded -= _window_Loaded;

                _showLock.SetCompleted();
            }

            private void _window_Closed(object sender, EventArgs e)
            {
                _window.Closed -= _window_Closed;

                _closed = true;
                _closeLock.SetCompleted();
            }

            public void Dispose()
            {
                _showLock.Dispose();
                _closeLock.Dispose();
            }
        }
    }
}