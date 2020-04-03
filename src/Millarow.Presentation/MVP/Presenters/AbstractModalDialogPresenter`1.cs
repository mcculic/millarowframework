using Millarow.Presentation.MVP.Services;
using Millarow.Presentation.MVP.Views;
using Millarow.Threading;
using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Presenters
{
    public abstract class AbstractModalDialogPresenter<TView> : AbstractDialogPresenter<TView>
        where TView : class, IDialogView
    {
        public AbstractModalDialogPresenter(TView view, IDialogService dialogService)
            : base(view, dialogService)
        {
        }

        public virtual Task Close()
        {
            return AcceptDialogAsync();
        }

        protected virtual Task CancelDialogAsync()
        {
            return FailDialogAsync(new OperationCanceledException());
        }

        protected async Task RunDialogAsync()
        {
            RunLock = new AsyncOperationLock();

            await ShowDialogAsync();
            await RunLock.WaitCompleteAsync();
        }

        protected override Task OnShowing()
        {
            View.CloseCommand.Attach(Close);

            return base.OnShowing();
        }

        protected Task AcceptDialogAsync()
        {
            if (RunLock == null)
                throw new InvalidOperationException();

            RunLock.SetCompleted();

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

        private AsyncOperationLock RunLock { get; set; }
    }
}