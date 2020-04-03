using Millarow.Presentation.MVP;
using Millarow.Presentation.MVP.Views;
using Millarow.Presentation.WPF.Framework;

namespace Millarow.Presentation.WPF.MVP.ViewModels
{
    //TODO rename to AbstractScreenViewModel
    public abstract class ScreenViewModel : ViewModel, IScreenView
    {
        public IAsyncCommand CloseCommand { get; } = new AsyncCommand();

        public string Title { get; set; }
    }
}
