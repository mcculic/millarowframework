using System.Windows;

namespace Millarow.Presentation.WPF.Input
{
    internal class MoverManipulationInfo
    {
        public MoverManipulationInfo(UIElement sourceElement, RectManipulations manipulations, Point prevMousePos)
        {
            SourceElement = sourceElement;
            Manipulations = manipulations;
            PrevMousePos = prevMousePos;
        }

        public UIElement SourceElement { get; }

        public RectManipulations Manipulations { get; }

        public Point PrevMousePos { get; set; }
    }
}
