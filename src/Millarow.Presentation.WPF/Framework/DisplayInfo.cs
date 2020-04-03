using System.Windows;

namespace Millarow.Presentation.WPF.Framework
{
    public class DisplayInfo
    {
        public static DisplayInfo FromPresentationSource(PresentationSource source)
        {
            return new DisplayInfo
            {
                DpiX = 96.0 * source?.CompositionTarget.TransformToDevice.M11 ?? 1,
                DpiY = 96.0 * source?.CompositionTarget.TransformToDevice.M22 ?? 1
            };
        }

        public double DpiX { get; set; }

        public double DpiY { get; set; }
    }
}