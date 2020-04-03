using Millarow.Presentation.WPF.Framework;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Millarow.Presentation.WPF.Controls
{
    public class FocusAccentAdornerDecorator : AdornerDecorator
    {
        static FocusAccentAdornerDecorator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FocusAccentAdornerDecorator), new FrameworkPropertyMetadata(typeof(FocusAccentAdornerDecorator)));
        }

        public FocusAccentAdornerDecorator()
        {
            Loaded += FocusAccentAdornerDecorator_Loaded;
            Unloaded += FocusAccentAdornerDecorator_Unloaded;
        }

        private void FocusAccentAdornerDecorator_Loaded(object sender, RoutedEventArgs e)
        {
            Adorner = new FocusAccentAdorner(Child);

            Adorner.SetBinding(FocusAccentAdorner.OverlayBackgroundProperty, this, nameof(OverlayBackground));
            Adorner.SetBinding(FocusAccentAdorner.OverlayOpacityProperty, this, nameof(OverlayOpacity));
            Adorner.SetBinding(FocusAccentAdorner.AccentBackgroundProperty, this, nameof(AccentBackground));
            Adorner.SetBinding(FocusAccentAdorner.AccentBorderProperty, this, nameof(AccentBorder));

            AdornerLayer.Add(Adorner);
        }

        private void FocusAccentAdornerDecorator_Unloaded(object sender, RoutedEventArgs e)
        {
            if (Adorner != null)
                AdornerLayer.Remove(Adorner);
        }

        internal void HandleAccentedChanged(DependencyObject d)
        {
            var accent = GetAccentedParent(d);

            if (accent != null && accent.IsDescendantOf(this))
                Adorner.SetTarget(accent);
            else
                Adorner.SetTarget(null);
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            if (e.NewFocus is UIElement target)
                FocusedElement = target;

            Adorner.SetTarget(GetAccentedParent(FocusedElement));

            base.OnGotKeyboardFocus(e);
        }

        private UIElement GetAccentedParent(DependencyObject d)
        {
            if (FocusedElement == null)
                return null;

            return FocusedElement.TraverseUpVisual()
                .Where(x => FocusAccentAdorner.GetAccented(x))
                .OfType<UIElement>().FirstOrDefault();
        }

        protected FocusAccentAdorner Adorner { get; private set; }

        protected UIElement FocusedElement { get; private set; }

        #region OverlayBackgroundProperty
        public static DependencyProperty OverlayBackgroundProperty = DependencyProperty.Register("OverlayBackground", typeof(Brush), typeof(FocusAccentAdornerDecorator));
        public Brush OverlayBackground
        {
            get { return (Brush)GetValue(OverlayBackgroundProperty); }
            set { SetValue(OverlayBackgroundProperty, value); }
        }
        #endregion

        #region OverlayOpacityProperty
        public static DependencyProperty OverlayOpacityProperty = DependencyProperty.Register("OverlayOpacity", typeof(double), typeof(FocusAccentAdornerDecorator));
        public double OverlayOpacity
        {
            get { return (double)GetValue(OverlayOpacityProperty); }
            set { SetValue(OverlayOpacityProperty, value); }
        }
        #endregion

        #region AccentBackgroundProperty
        public static DependencyProperty AccentBackgroundProperty = DependencyProperty.Register("AccentBackground", typeof(Brush), typeof(FocusAccentAdornerDecorator));
        public Brush AccentBackground
        {
            get { return (Brush)GetValue(AccentBackgroundProperty); }
            set { SetValue(AccentBackgroundProperty, value); }
        }
        #endregion

        #region AccentBorderProperty
        public static DependencyProperty AccentBorderProperty = DependencyProperty.Register("AccentBorder", typeof(Pen), typeof(FocusAccentAdornerDecorator));
        public Pen AccentBorder
        {
            get { return (Pen)GetValue(AccentBorderProperty); }
            set { SetValue(AccentBorderProperty, value); }
        }
        #endregion
    }
}