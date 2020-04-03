using System;

namespace Millarow.Presentation.WPF.Interop
{
    public enum InteropBitmapFormat : int
    {
        Rgb24 = 24,
        Rgba32 = 32
    }

    public static class InteropBitmapFormatHelper
    {
        public static System.Drawing.Imaging.PixelFormat ToGdi(this InteropBitmapFormat format)
        {
            switch (format)
            {
                case InteropBitmapFormat.Rgb24:
                    return System.Drawing.Imaging.PixelFormat.Format24bppRgb;
                case InteropBitmapFormat.Rgba32:
                    return System.Drawing.Imaging.PixelFormat.Format32bppRgb;
                default:
                    throw new ArgumentException($"Unknown value '{format}'", nameof(format));
            }
        }

        public static System.Windows.Media.PixelFormat ToWpf(this InteropBitmapFormat format)
        {
            switch (format)
            {
                case InteropBitmapFormat.Rgb24:
                    return System.Windows.Media.PixelFormats.Bgr24;
                case InteropBitmapFormat.Rgba32:
                    return System.Windows.Media.PixelFormats.Bgra32;
                default:
                    throw new ArgumentException($"Unknown value '{format}'", nameof(format));
            }
        }
    }
}
