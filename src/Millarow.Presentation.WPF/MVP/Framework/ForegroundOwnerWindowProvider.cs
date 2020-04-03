using System.Linq;
using System.Windows;

namespace Millarow.Presentation.WPF.MVP.Framework
{
    public class ForegroundOwnerWindowProvider : IOwnerWindowProvider
    {
        public ForegroundOwnerWindowProvider(Window window)
        {
            window.AssertNotNull(nameof(window));

            Window = window;
        }

        public Window GetOwnerWindow()
        {
            return GetActiveWindow(Window);
        }

        private Window GetActiveWindow(Window parent)
        {
            var last = parent.OwnedWindows.OfType<Window>().LastOrDefault();
            if (last != null)
                return GetActiveWindow(last);

            return parent;
        }

        protected Window Window { get; }
    }
}
