using Millarow.Presentation.MVP.Services;
using Millarow.Presentation.MVP.Views;
using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Presenters
{
    public abstract class AbstractScreenPresenter<TView> : AbstractPresenter<TView>, IDisposable
        where TView : class, IScreenView
    {
        public AbstractScreenPresenter(TView view, INavigationService navigationService)
            : base(view)
        {
            navigationService.AssertNotNull(nameof(navigationService));

            NavigationService = navigationService;
        }

        protected async Task NavigateScreenAsync()
        {
            await OnNavigating();
            Token = NavigationService.Navigate(View);
            await Token.WaitForNavigateAsync();
            await OnNavigated();
        }

        protected async Task CloseScreenAsync()
        {
            await OnClosing();
            Token.Close();
            await Token.WaitForCloseAsync();
            await OnClosed();
        }

        protected virtual Task OnNavigating()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnNavigated()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnClosing()
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnClosed()
        {
            return Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
            Token?.Dispose();
            Token = null;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private INavigationService NavigationService { get; }

        private INavigationToken Token { get; set; }
    }
}