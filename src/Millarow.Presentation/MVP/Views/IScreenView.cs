namespace Millarow.Presentation.MVP.Views
{
    public interface IScreenView : IView
    {
        IAsyncCommand CloseCommand { get; }
    }
}
