using System.ComponentModel;
using System.Windows;

namespace Millarow.Presentation.WPF.Input
{
    public class MoverManipulationEventArgs : CancelEventArgs
    {
        public MoverManipulationEventArgs(UIElement sourceElement, RectManipulations manipulation)
        {
            sourceElement.AssertNotNull(nameof(sourceElement));

            SourceElement = sourceElement;
            Manipulation = manipulation;
        }

        public UIElement SourceElement { get; }

        public RectManipulations Manipulation { get; }
    }
}