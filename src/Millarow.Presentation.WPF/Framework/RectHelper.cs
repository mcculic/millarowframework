using System.Windows;

namespace Millarow.Presentation.WPF.Framework
{
    public static class RectHelper
    {
        public static Rect Pad(this Rect rect, Thickness padding)
        {
            var x = rect.Left + padding.Left;
            var y = rect.Top + padding.Top;
            var w = rect.Width - padding.Left - padding.Right;
            var h = rect.Height - padding.Top - padding.Bottom;

            return new Rect(x, y, w, h);
        }
    }
}
