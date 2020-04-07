namespace Millarow.Presentation.MVP.Views
{
    public interface IWindowManager
    {
        IWindowView CreateWindow(IView contentView, bool modal);
    }
}
