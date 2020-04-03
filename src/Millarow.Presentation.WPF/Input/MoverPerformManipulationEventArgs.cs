using System.Windows;

namespace Millarow.Presentation.WPF.Input
{
    public class MoverPerformManipulationEventArgs : MoverManipulationEventArgs
    {
        public MoverPerformManipulationEventArgs(UIElement sourceElement, RectManipulations manipulation, Vector sizeVector, Vector positionVector)
            : base(sourceElement, manipulation)
        {
            SizeVector = sizeVector;
            PositionVector = positionVector;
        }

        public Vector SizeVector { get; }

        public Vector PositionVector { get; }
    }
}