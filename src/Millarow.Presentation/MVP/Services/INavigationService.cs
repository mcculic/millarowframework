using Millarow.Presentation.MVP.Views;

namespace Millarow.Presentation.MVP.Services
{
    public interface INavigationService
    {
        //TODO IScreenView?
        INavigationToken Navigate(IView view);
    }
}