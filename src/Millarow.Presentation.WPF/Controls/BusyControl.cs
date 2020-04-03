using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Millarow.Presentation.WPF.Controls
{
    public class BusyControl : ContentControl
    {
        static BusyControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BusyControl), new FrameworkPropertyMetadata(typeof(BusyControl)));
        }

        public override void OnApplyTemplate()
        {
            if (IsTemplateValid)
            {
                PART_Busy.IsVisibleChanged -= PART_BusyFocusScope_IsVisibleChanged;

                if (AdornerLayer != null)
                    AdornerLayer.Remove(Adorner);
            }

            base.OnApplyTemplate();

            PART_Busy = GetTemplateChild(nameof(PART_Busy)) as UIElement;
            PART_Content = GetTemplateChild(nameof(PART_Content)) as UIElement;

            if (PART_Content != null && PART_Busy != null)
            {
                KeyboardNavigation.SetControlTabNavigation(PART_Busy, KeyboardNavigationMode.Cycle);
                KeyboardNavigation.SetDirectionalNavigation(PART_Busy, KeyboardNavigationMode.Cycle);
                KeyboardNavigation.SetTabNavigation(PART_Busy, KeyboardNavigationMode.Cycle);

                AdornerLayer = AdornerLayer.GetAdornerLayer(PART_Content);
                if (AdornerLayer != null)
                {
                    Adorner = new BusyControlAdorner(PART_Content, this);
                    AdornerLayer.Add(Adorner);
                }

                PART_Busy.IsVisibleChanged += PART_BusyFocusScope_IsVisibleChanged;

                IsTemplateValid = true;
            }
        }

        private void PART_BusyFocusScope_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (PART_Busy.IsVisible)
            {
                if (AdornerLayer == null || !PART_Busy.Focus())
                    PART_Content.IsEnabled = false;
            }
            else if (!PART_Content.IsEnabled)
            {
                PART_Content.IsEnabled = true;
            }
        }

        protected virtual void OnIsBusyChanged(bool oldValue, bool newValue)
        {
            IsBusyChanged?.Invoke(this, EventArgs.Empty);
        }

        protected bool IsTemplateValid { get; private set; }

        protected UIElement PART_Content { get; private set; }

        protected UIElement PART_Busy { get; private set; }

        protected AdornerLayer AdornerLayer { get; private set; }

        protected BusyControlAdorner Adorner { get; private set; }

        public event EventHandler IsBusyChanged;

        #region IsBusyProperty
        public static DependencyProperty IsBusyProperty = DependencyProperty.Register("IsBusy", typeof(bool), typeof(BusyControl), new PropertyMetadata(OnIsBusyChanged));
        public bool IsBusy
        {
            get { return (bool)GetValue(IsBusyProperty); }
            set { SetValue(IsBusyProperty, value); }
        }

        private static void OnIsBusyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BusyControl)
                ((BusyControl)d).OnIsBusyChanged((bool)e.OldValue, (bool)e.NewValue);
        }
        #endregion

        #region BusyStateProperty
        public static DependencyProperty BusyStateProperty = DependencyProperty.Register("BusyState", typeof(object), typeof(BusyControl));
        public object BusyState
        {
            get { return (object)GetValue(BusyStateProperty); }
            set { SetValue(BusyStateProperty, value); }
        }
        #endregion

        #region BusyStateTemplateProperty
        public static DependencyProperty BusyStateTemplateProperty = DependencyProperty.Register("BusyStateTemplate", typeof(DataTemplate), typeof(BusyControl));
        public DataTemplate BusyStateTemplate
        {
            get { return (DataTemplate)GetValue(BusyStateTemplateProperty); }
            set { SetValue(BusyStateTemplateProperty, value); }
        }
        #endregion

        #region BusyStateStringFormatProperty
        public static DependencyProperty BusyStateStringFormatProperty = DependencyProperty.Register("BusyStateStringFormat", typeof(string), typeof(BusyControl));
        public string BusyStateStringFormat
        {
            get { return (string)GetValue(BusyStateStringFormatProperty); }
            set { SetValue(BusyStateStringFormatProperty, value); }
        }
        #endregion

        #region BusyStateTemplateSelectorProperty
        public static DependencyProperty BusyStateTemplateSelectorProperty = DependencyProperty.Register("BusyStateTemplateSelector", typeof(DataTemplateSelector), typeof(BusyControl));
        public DataTemplateSelector BusyStateTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(BusyStateTemplateSelectorProperty); }
            set { SetValue(BusyStateTemplateSelectorProperty, value); }
        }
        #endregion

        #region OverlayBrushProperty
        public static DependencyProperty OverlayBrushProperty = DependencyProperty.Register("OverlayBrush", typeof(Brush), typeof(BusyControl));
        public Brush OverlayBrush
        {
            get { return (Brush)GetValue(OverlayBrushProperty); }
            set { SetValue(OverlayBrushProperty, value); }
        }
        #endregion

        #region OverlayOpacityProperty
        public static DependencyProperty OverlayOpacityProperty = DependencyProperty.Register("OverlayOpacity", typeof(double), typeof(BusyControl));
        public double OverlayOpacity
        {
            get { return (double)GetValue(OverlayOpacityProperty); }
            set { SetValue(OverlayOpacityProperty, value); }
        }
        #endregion

        #region HorizontalBusyStateAligmentProperty
        public static DependencyProperty HorizontalBusyStateAligmentProperty = DependencyProperty.Register("HorizontalBusyStateAligment", typeof(HorizontalAlignment), typeof(BusyControl));
        public HorizontalAlignment HorizontalBusyStateAligment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalBusyStateAligmentProperty); }
            set { SetValue(HorizontalBusyStateAligmentProperty, value); }
        }
        #endregion

        #region VerticalBusyStateAlignmentProperty
        public static DependencyProperty VerticalBusyStateAlignmentProperty = DependencyProperty.Register("VerticalBusyStateAlignment", typeof(VerticalAlignment), typeof(BusyControl));
        public VerticalAlignment VerticalBusyStateAlignment
        {
            get { return (VerticalAlignment)GetValue(VerticalBusyStateAlignmentProperty); }
            set { SetValue(VerticalBusyStateAlignmentProperty, value); }
        }
        #endregion
    }
}