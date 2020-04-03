using Millarow.Presentation.MVP;
using Millarow.Presentation.MVP.Views;

namespace Millarow.Presentation.WPF.MVP.ViewModels
{
    public class WaitDialogViewModel : DialogViewModel, IWaitDialogView
    {
        public IBusyState BusyState { get; } = new BusyStateViewModel();

        public override string Title => BusyState.Message;
    }
}