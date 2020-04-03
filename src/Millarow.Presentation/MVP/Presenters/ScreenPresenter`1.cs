using Millarow.Presentation.MVP.Services;
using Millarow.Presentation.MVP.Views;
using Millarow.Threading;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Presenters
{
    //TODO в чем разница между ScreenPresenter и AbstractScreenPresenter?
    public abstract class ScreenPresenter<TView> : AbstractScreenPresenter<TView>, IScreenPresenter
        where TView : class, IScreenView
    {
        public ScreenPresenter(INavigationService navigationService, TView view)
            : base(view, navigationService)
        {
            RunLock = new AsyncOperationLock();
        }

        protected async Task RunScreenAsync()
        {
            await NavigateScreenAsync();
            await RunLock.WaitCompleteAsync();
            RunLock.Reset();
        }

        protected override Task OnClosed()
        {
            RunLock.SetCompleted();

            return base.OnClosed();
        }

        public virtual async Task<bool> RequestClose()
        {
            await CloseScreenAsync();

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            RunLock.Dispose();
            base.Dispose(disposing);
        }

        private AsyncOperationLock RunLock { get; set; }
    }
}
