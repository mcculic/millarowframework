using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Millarow.Presentation.WPF.Controls
{
    public class ShadowBox : FrameworkElement
    {
        static ShadowBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ShadowBox), new FrameworkPropertyMetadata(typeof(ShadowBox)));
        }

        public ShadowBox()
        {
            Effect = new DropShadowEffect
            {
                Direction = Direction,
                Color = Color,
                BlurRadius = BlurRadius,
                ShadowDepth = Depth,
                RenderingBias = RenderingBias
            };
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var rect = new Rect(1, 1, ActualWidth - 2, ActualHeight - 2);

            drawingContext.DrawRectangle(Brushes.Black, null, rect);

            base.OnRender(drawingContext);
        }

        protected virtual void OnDirectionChanged(double oldValue, double newValue)
        {
            ShadowEffect.Direction = newValue;
        }

        protected virtual void OnColorChanged(Color oldValue, Color newValue)
        {
            ShadowEffect.Color = newValue;
        }

        protected virtual void OnDepthChanged(double oldValue, double newValue)
        {
            ShadowEffect.ShadowDepth = newValue;
        }

        protected virtual void OnBlurRadiusChanged(double oldValue, double newValue)
        {
            ShadowEffect.BlurRadius = newValue;
        }

        protected virtual void OnRenderingBiasChanged(RenderingBias oldValue, RenderingBias newValue)
        {
            ShadowEffect.RenderingBias = newValue;
        }

        protected DropShadowEffect ShadowEffect => Effect as DropShadowEffect;

        #region DirectionProperty
        public static DependencyProperty DirectionProperty = DependencyProperty.Register("Direction", typeof(double), typeof(ShadowBox), new PropertyMetadata(315.0, OnDirectionChanged));
        public double Direction
        {
            get { return (double)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        private static void OnDirectionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ShadowBox)
                ((ShadowBox)d).OnDirectionChanged((double)e.OldValue, (double)e.NewValue);
        }
        #endregion

        #region ColorProperty
        public static DependencyProperty ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(ShadowBox), new PropertyMetadata(Colors.Black, OnColorChanged));
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ShadowBox)
                ((ShadowBox)d).OnColorChanged((Color)e.OldValue, (Color)e.NewValue);
        }
        #endregion

        #region BlurRadiusProperty
        public static DependencyProperty BlurRadiusProperty = DependencyProperty.Register("BlurRadius", typeof(double), typeof(ShadowBox), new PropertyMetadata(5.0, OnBlurRadiusChanged));
        public double BlurRadius
        {
            get { return (double)GetValue(BlurRadiusProperty); }
            set { SetValue(BlurRadiusProperty, value); }
        }

        private static void OnBlurRadiusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ShadowBox)
                ((ShadowBox)d).OnBlurRadiusChanged((double)e.OldValue, (double)e.NewValue);
        }
        #endregion

        #region DepthProperty
        public static DependencyProperty DepthProperty = DependencyProperty.Register("Depth", typeof(double), typeof(ShadowBox), new PropertyMetadata(5.0, OnDepthChanged));
        public double Depth
        {
            get { return (double)GetValue(DepthProperty); }
            set { SetValue(DepthProperty, value); }
        }

        private static void OnDepthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ShadowBox)
                ((ShadowBox)d).OnDepthChanged((double)e.OldValue, (double)e.NewValue);
        }
        #endregion

        #region RenderingBiasProperty
        public static DependencyProperty RenderingBiasProperty = DependencyProperty.Register("RenderingBias", typeof(RenderingBias), typeof(ShadowBox), new PropertyMetadata(RenderingBias.Performance, OnRenderingBiasChanged));
        public RenderingBias RenderingBias
        {
            get { return (RenderingBias)GetValue(RenderingBiasProperty); }
            set { SetValue(RenderingBiasProperty, value); }
        }

        private static void OnRenderingBiasChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ShadowBox)
                ((ShadowBox)d).OnRenderingBiasChanged((RenderingBias)e.OldValue, (RenderingBias)e.NewValue);
        }
        #endregion
    }
}