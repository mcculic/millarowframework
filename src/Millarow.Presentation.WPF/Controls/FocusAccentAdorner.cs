using Millarow.Presentation.WPF.Framework;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Millarow.Presentation.WPF.Controls
{
    public class FocusAccentAdorner : Adorner
    {
        private bool _skipNextLayoutUpdate;

        static FocusAccentAdorner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FocusAccentAdorner), new FrameworkPropertyMetadata(typeof(FocusAccentAdorner)));
        }

        public FocusAccentAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
        }

        public void SetTarget(UIElement target)
        {
            if (AccentTarget != null)
            {
                AccentTarget.LayoutUpdated -= AccentTarget_LayoutUpdated;
            }

            AccentTarget = target;
            InvalidateVisual();

            if (AccentTarget != null)
            {
                AccentTarget.LayoutUpdated += AccentTarget_LayoutUpdated;
            }
        }

        private void AccentTarget_LayoutUpdated(object sender, EventArgs e)
        {
            if (_skipNextLayoutUpdate)
                _skipNextLayoutUpdate = false;
            else
            {
                _skipNextLayoutUpdate = true;
                InvalidateVisual();
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (AccentTarget != null && GetAccented(AccentTarget))
            {
                var focusLocation = AccentTarget.TranslatePoint(new Point(), AdornedElement);
                var focusSize = AccentTarget.RenderSize;
                var focusRect = new Rect(focusLocation, focusSize);
                var boundsRect = new Rect(AdornedElement.RenderSize);

                drawingContext.PushOpacity(OverlayOpacity);
                drawingContext.DrawGeometry(OverlayBackground, null, CreateOverlayGeometry(boundsRect, focusRect));
                drawingContext.Pop();

                drawingContext.DrawRectangle(AccentBackground, AccentBorder, focusRect);
            }

            _skipNextLayoutUpdate = true;
        }

        private Geometry CreateOverlayGeometry(Rect bounds, Rect focus)
        {
            var boundsGeometry = new RectangleGeometry(bounds);
            var focusGeometry = new RectangleGeometry(focus);

            return new CombinedGeometry(GeometryCombineMode.Exclude, boundsGeometry, focusGeometry);
        }

        protected UIElement AccentTarget { get; private set; }

        #region AccentedProperty
        private static FrameworkPropertyMetadata AccentedPropertyMetadata = new FrameworkPropertyMetadata { AffectsRender = false, PropertyChangedCallback = OnAccentedChanged };
        public static DependencyProperty AccentedProperty = DependencyProperty.RegisterAttached("Accented", typeof(bool), typeof(FocusAccentAdorner), AccentedPropertyMetadata);

        public static bool GetAccented(DependencyObject obj) { return (bool)obj.GetValue(AccentedProperty); }
        public static void SetAccented(DependencyObject obj, bool value) { obj.SetValue(AccentedProperty, value); }

        private static void OnAccentedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var decorator = d.TraverseUpVisual().OfType<FocusAccentAdornerDecorator>().FirstOrDefault();

            decorator?.HandleAccentedChanged(d);
        }
        #endregion

        #region OverlayBackgroundProperty
        private static FrameworkPropertyMetadata OverlayBackgroundPropertyMetadata = new FrameworkPropertyMetadata { AffectsRender = true };
        public static DependencyProperty OverlayBackgroundProperty = DependencyProperty.Register("OverlayBackground", typeof(Brush), typeof(FocusAccentAdorner), OverlayBackgroundPropertyMetadata);
        public Brush OverlayBackground
        {
            get { return (Brush)GetValue(OverlayBackgroundProperty); }
            set { SetValue(OverlayBackgroundProperty, value); }
        }
        #endregion

        #region OverlayOpacityProperty
        private static FrameworkPropertyMetadata OverlayOpacityPropertyMetadata = new FrameworkPropertyMetadata { AffectsRender = true };
        public static DependencyProperty OverlayOpacityProperty = DependencyProperty.Register("OverlayOpacity", typeof(double), typeof(FocusAccentAdorner), OverlayOpacityPropertyMetadata);
        public double OverlayOpacity
        {
            get { return (double)GetValue(OverlayOpacityProperty); }
            set { SetValue(OverlayOpacityProperty, value); }
        }
        #endregion

        #region AccentBackgroundProperty
        private static FrameworkPropertyMetadata AccentBackgroundPropertyMetadata = new FrameworkPropertyMetadata { AffectsRender = true };
        public static DependencyProperty AccentBackgroundProperty = DependencyProperty.Register("AccentBackground", typeof(Brush), typeof(FocusAccentAdorner), AccentBackgroundPropertyMetadata);
        public Brush AccentBackground
        {
            get { return (Brush)GetValue(AccentBackgroundProperty); }
            set { SetValue(AccentBackgroundProperty, value); }
        }
        #endregion

        #region AccentBorderProperty
        private static FrameworkPropertyMetadata AccentBorderPropertyMetadata = new FrameworkPropertyMetadata { AffectsRender = true };
        public static DependencyProperty AccentBorderProperty = DependencyProperty.Register("AccentBorder", typeof(Pen), typeof(FocusAccentAdorner), AccentBorderPropertyMetadata);
        public Pen AccentBorder
        {
            get { return (Pen)GetValue(AccentBorderProperty); }
            set { SetValue(AccentBorderProperty, value); }
        }
        #endregion
    }
}