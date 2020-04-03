using Millarow.Presentation.WPF.Framework;
using Millarow.Presentation.WPF.Interop;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WinForms = System.Windows.Forms;

namespace Millarow.Presentation.WPF.Controls
{
    [ContentProperty(nameof(Child))]
    public class WindowsFormsHostDecorator : Control
    {
        static WindowsFormsHostDecorator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WindowsFormsHostDecorator), new FrameworkPropertyMetadata(typeof(WindowsFormsHostDecorator)));
        }

        public WindowsFormsHostDecorator()
        {
            Host = new WindowsFormsHost();
            Image = new Image();
            Image.SetBinding(WidthProperty, Image, "Source.PixelWidth");
            Image.SetBinding(HeightProperty, Image, "Source.PixelHeight");

            View = Host;
        }

        private void OnChildChanged(WinForms.Control oldValue, WinForms.Control newValue)
        {
            if (oldValue != null)
                oldValue.Invalidated -= Child_Invalidated;

            Host.Child = newValue;

            if (newValue != null)
                newValue.Invalidated += Child_Invalidated;

            UpdateView();
        }

        private void OnCompatibilityModeChanged(bool oldValue, bool newValue)
        {
            UpdateView();
        }

        private void Child_Invalidated(object sender, WinForms.InvalidateEventArgs e)
        {
            Dispatcher.Invoke(delegate
            {
                UpdateView();
            });
        }

        private void UpdateView()
        {
            if (CompatibilityMode)
            {
                Image.Source = RenderControl(Child);
                View = Image;
            }
            else if (!ReferenceEquals(View, Host))
            {
                View = Host;
            }
        }

        private BitmapSource RenderControl(WinForms.Control control)
        {
            if (control == null || control.Width == 0 || control.Height == 0)
                return null;

            if (Buffer == null || Buffer.Width < control.Width || Buffer.Height < control.Height)
            {
                var dpi = DisplayInfo.FromPresentationSource(PresentationSource.FromVisual(Image));
                Buffer = new InteropBitmap(control.Width, control.Height, (float)dpi.DpiX, (float)dpi.DpiY, InteropBitmapFormat.Rgba32);
            }

            Buffer.Render(target => control.DrawToBitmap(target, control.Bounds));

            return Buffer.Bitmap;
        }

        private InteropBitmap Buffer { get; set; }

        private WindowsFormsHost Host { get; set; }

        private Image Image { get; set; }

        #region ViewProperty
        private static DependencyPropertyKey ViewPropertyKey = DependencyProperty.RegisterReadOnly("View", typeof(Visual), typeof(WindowsFormsHostDecorator), new PropertyMetadata());
        public static DependencyProperty ViewProperty = ViewPropertyKey.DependencyProperty;
        public Visual View
        {
            get { return (Visual)GetValue(ViewProperty); }
            private set { SetValue(ViewPropertyKey, value); }
        }
        #endregion

        #region ChildProperty
        public static DependencyProperty ChildProperty = DependencyProperty.Register("Child", typeof(WinForms.Control), typeof(WindowsFormsHostDecorator), new PropertyMetadata(OnChildChanged));
        public WinForms.Control Child
        {
            get { return (WinForms.Control)GetValue(ChildProperty); }
            set { SetValue(ChildProperty, value); }
        }

        private static void OnChildChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WindowsFormsHostDecorator)
                ((WindowsFormsHostDecorator)d).OnChildChanged((WinForms.Control)e.OldValue, (WinForms.Control)e.NewValue);
        }
        #endregion

        #region CompatibilityModeProperty
        public static DependencyProperty CompatibilityModeProperty = DependencyProperty.RegisterAttached("CompatibilityMode", typeof(bool), typeof(WindowsFormsHostDecorator), new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits, OnCompatibilityModeChanged));
        public bool CompatibilityMode
        {
            get { return (bool)GetValue(CompatibilityModeProperty); }
            set { SetValue(CompatibilityModeProperty, value); }
        }

        public static bool GetCompatibilityMode(DependencyObject d)
        {
            return (bool)d.GetValue(CompatibilityModeProperty);
        }

        public static void SetCompatibilityMode(DependencyObject d, bool value)
        {
            d.SetValue(CompatibilityModeProperty, value);
        }

        private static void OnCompatibilityModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WindowsFormsHostDecorator)
                ((WindowsFormsHostDecorator)d).OnCompatibilityModeChanged((bool)e.OldValue, (bool)e.NewValue);
        }
        #endregion
    }
}
