namespace Millarow.Presentation.MVP.Views
{
    public interface IWaitDialogView : IDialogView
    {
        IBusyState BusyState { get; }
    }
}
