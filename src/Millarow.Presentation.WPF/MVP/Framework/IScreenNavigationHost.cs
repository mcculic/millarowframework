using Millarow.Presentation.MVP.Views;

namespace Millarow.Presentation.WPF.MVP.Framework
{
    //TODO rename
    public interface IScreenNavigationHost
    {
        IView CurrentView { get; set; } //TODO view type?
    }
}
