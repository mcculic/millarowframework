using Millarow.Presentation.MVP;
using Millarow.Presentation.MVP.Views;
using Millarow.Presentation.WPF.Framework;

namespace Millarow.Presentation.WPF.MVP.ViewModels
{
    //TODO rename to AbstractDialogViewModel 
    public abstract class DialogViewModel : ViewModel, IDialogView
    {
        public abstract string Title { get; }

        public IAsyncCommand CloseCommand { get; } = new AsyncCommand();
    }
}
