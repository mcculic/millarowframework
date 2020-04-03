using Millarow.Presentation.MVP.Services;
using Millarow.Presentation.MVP.Views;
using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Presenters
{
    public abstract class AbstractDialogPresenter<TView> : AbstractPresenter<TView>, IDisposable
       where TView : class, IDialogView
    {
        public AbstractDialogPresenter(TView view, IDialogService dialogService)
            : base(view)
        {
            dialogService.AssertNotNull(nameof(dialogService));

            DialogService = dialogService;
        }

        protected async Task ShowDialogAsync()
        {
            await OnShowing();
            Token = DialogService.ShowDialog(View);
            await Token.WaitForShowAsync();
            await OnShown();
        }

        protected async Task CloseDialogAsync()
        {
            await OnClosing();
            Token.Close();
            await Token.WaitForCloseAsync();
            await OnClosed();
        }

        protected virtual Task OnShowing() => Task.CompletedTask;

        protected virtual Task OnShown() => Task.CompletedTask;

        protected virtual Task OnClosing() => Task.CompletedTask;

        protected virtual Task OnClosed() => Task.CompletedTask;

        protected virtual void Dispose(bool disposing)
        {
            Token?.Dispose();
            Token = null;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private IDialogService DialogService { get; }

        private IDialogServiceToken Token { get; set; }
    }
}
