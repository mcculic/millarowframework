using Millarow.Presentation.MVP.Views;
using Millarow.Presentation.MVP.Services;
using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Presenters
{
    public class WaitDialogPresenter : AbstractModalDialogPresenter<IWaitDialogView>
    {
        public WaitDialogPresenter(IDialogService dialogService, IWaitDialogView view)
            : base(view, dialogService)
        {
        }

        public Task Show(Func<IBusyState, Task> body)
        {
            Body = body;

            return RunDialogAsync();
        }

        protected override async Task OnShown()
        {
            await base.OnShown();

            try
            {
                await Body(View.BusyState);
                await AcceptDialogAsync();
            }
            catch (Exception ex)
            {
                await FailDialogAsync(ex);
            }
        }

        public override Task Close()
        {
            View.BusyState.CancellationCallback?.Invoke();

            return Task.CompletedTask;
        }

        private Func<IBusyState, Task> Body { get; set; }
    }
}
