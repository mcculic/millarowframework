using Millarow.Presentation.MVP.Services;
using Millarow.Presentation.MVP.Views;
using Millarow.Threading;
using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Presenters
{
    public abstract class AbstractModalDialogPresenter<TView, TResult> : AbstractDialogPresenter<TView>
        where TView : class, IDialogView
    {
        public AbstractModalDialogPresenter(TView view, IDialogService dialogService)
            : base(view, dialogService)
        {
        }

        public virtual Task Close()
        {
            return CloseDialogAsync();
        }

        protected async Task<TResult> RunDialogAsync()
        {
            RunLock = new AsyncOperationLock<TResult>();

            await ShowDialogAsync();
            return await RunLock.WaitResultAsync();
        }

        protected virtual Task CancelDialogAsync()
        {
            return FailDialogAsync(new OperationCanceledException());
        }

        protected override Task OnShowing()
        {
            View.CloseCommand.Attach(Close).IgnoreReentry();

            return base.OnShowing();
        }

        protected Task AcceptDialogAsync(TResult result)
        {
            if (RunLock == null)
                throw new InvalidOperationException();

            RunLock.SetResult(result);

            return CloseDialogAsync();
        }

        protected Task FailDialogAsync(Exception exception)
        {
            if (RunLock == null)
                throw new InvalidOperationException();

            RunLock.SetException(exception);

            return CloseDialogAsync();
        }

        protected override void Dispose(bool disposing)
        {
            RunLock?.Dispose();
            base.Dispose(disposing);
        }

        private AsyncOperationLock<TResult> RunLock { get; set; }
    }
}