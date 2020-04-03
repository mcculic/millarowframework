using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Millarow.Presentation.WPF.Interop
{
    public class InteropBitmap
    {
        public InteropBitmap(int width, int height, float dpiX, float dpiY, InteropBitmapFormat format)
        {
            width.AssertGreaterThanZero(nameof(width));
            height.AssertGreaterThanZero(nameof(height));

            Width = width;
            Height = height;
            DpiX = dpiX;
            DpiY = dpiY;
            Format = CheckFormat(format);
            
            Bitmap = new WriteableBitmap(width, height, dpiX, dpiY, format.ToWpf(), null);
        }

        public void Render(Action<Bitmap> renderer, Int32Rect rect)
        {
            renderer.AssertNotNull(nameof(renderer));

            Bitmap.Lock();

            using (var target = CreateRenderTargetBitmap())
            {
                renderer(target);
            }

            Bitmap.AddDirtyRect(rect);
            Bitmap.Unlock();
        }

        public void Render(Action<Bitmap> renderer)
        {
            Render(renderer, new Int32Rect(0, 0, Width, Height));
        }

        private Bitmap CreateRenderTargetBitmap()
        {
            var ret = new Bitmap(Width, Height, Bitmap.BackBufferStride, Format.ToGdi(), Bitmap.BackBuffer);
            ret.SetResolution(DpiX, DpiY);

            return ret;
        }

        public int Width { get; }

        public int Height { get; }

        public float DpiX { get; }

        public float DpiY { get; }

        public InteropBitmapFormat Format { get; }

        public WriteableBitmap Bitmap { get; }

        private static InteropBitmapFormat CheckFormat(InteropBitmapFormat format)
        {
            switch (format)
            {
                case InteropBitmapFormat.Rgb24:
                case InteropBitmapFormat.Rgba32:
                    return format;
                default:
                    throw new ArgumentException($"Format '{format}' not supported", nameof(format));
            }
        }
    }
}