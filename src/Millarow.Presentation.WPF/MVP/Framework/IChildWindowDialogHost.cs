using Millarow.Presentation.MVP.Views;

namespace Millarow.Presentation.WPF.MVP.Framework
{
    public interface IChildWindowDialogHost
    {
        void AddDialogView(IView view); //TODO IDialogView?

        void RemoveDialogView(IView view);
    }
}
