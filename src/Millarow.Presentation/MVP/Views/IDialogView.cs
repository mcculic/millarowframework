namespace Millarow.Presentation.MVP.Views
{
    public interface IDialogView : IView
    {
        IAsyncCommand CloseCommand { get; }

        string Title { get; }
    }
}