using Millarow.Presentation.MVP.Views;
using Millarow.Presentation.MVP.Services;
using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Presenters
{
    public class WaitDialogPresenter<T> : AbstractModalDialogPresenter<IWaitDialogView, T>
    {
        public WaitDialogPresenter(IWaitDialogView view, IDialogService dialogService)
            : base(view, dialogService)
        {
        }

        public Task<T> Show(string message, Func<IBusyState, Task<T>> taskFactory)
        {
            View.BusyState.Message = message;
            TaskFactory = taskFactory;

            return RunDialogAsync();
        }

        protected override async Task OnShown()
        {
            await base.OnShown();

            try
            {
                await AcceptDialogAsync(await TaskFactory(View.BusyState));
            }
            catch (Exception ex)
            {
                await FailDialogAsync(ex);
            }
        }

        public override async Task Close()
        {
            var cb = View.BusyState.CancellationCallback;
            if (cb != null)
                cb();
            else
                await base.Close();
        }

        private Func<IBusyState, Task<T>> TaskFactory { get; set; }
    }
}
