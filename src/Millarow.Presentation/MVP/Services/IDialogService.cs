using Millarow.Presentation.MVP.Views;

namespace Millarow.Presentation.MVP.Services
{
    public interface IDialogService
    {
        IDialogServiceToken ShowDialog(IView view);
    }
}