using System;
using System.Threading.Tasks;

namespace Millarow.Presentation.MVP.Views
{
    public interface IWindowView : IDisposable
    {
        Task ShowAsync();

        Task CloseAsync();

        string Caption { get; set; }

        bool CanRequestClose { get; set; }

        event EventHandler CloseRequested;
    }
}
