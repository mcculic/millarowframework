using Millarow.Presentation.MVP.Views;
using System.Windows;

namespace Millarow.Presentation.WPF.MVP.Framework
{
    public interface IPlatformWindowHost
    {
        Window CreateWindow(IView view);

        void CloseWindow(Window window);
    }
}
