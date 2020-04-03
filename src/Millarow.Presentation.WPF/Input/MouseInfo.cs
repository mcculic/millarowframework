using System.Windows;

namespace Millarow.Presentation.WPF.Input
{
    public static class MouseInfo
    {
        public static Point GetCursorPosition()
        {
            var pos = System.Windows.Forms.Control.MousePosition;

            return new Point(pos.X, pos.Y);
        }
    }
}