using System;
using System.Windows;

namespace Millarow.Presentation.WPF.Input
{
    public class MoverElementTestEventArgs : EventArgs
    {
        public MoverElementTestEventArgs(UIElement sourceElement)
        {
            sourceElement.AssertNotNull(nameof(sourceElement));

            SourceElement = sourceElement;
            ResizeBorderPadding = new Thickness(3);
        }

        public UIElement SourceElement { get; }

        public bool CanMove { get; set; }

        public bool CanResize { get; set; }

        public Thickness ResizeBorderPadding { get; set; }
    }
}