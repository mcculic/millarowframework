using System.Windows;
using System.Windows.Controls;

namespace Millarow.Presentation.WPF.Controls
{
    public class PopupContainer : ContentControl
    {
        static PopupContainer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupContainer), new FrameworkPropertyMetadata(typeof(PopupContainer)));
        }


        #region PopupContentProperty
        public static DependencyProperty PopupContentProperty = DependencyProperty.Register("PopupContent", typeof(object), typeof(PopupContainer));
        public object PopupContent
        {
            get { return (object)GetValue(PopupContentProperty); }
            set { SetValue(PopupContentProperty, value); }
        }
        #endregion

        #region PopupContentTemplateProperty
        public static DependencyProperty PopupContentTemplateProperty = DependencyProperty.Register("PopupContentTemplate", typeof(DataTemplate), typeof(PopupContainer));
        public DataTemplate PopupContentTemplate
        {
            get { return (DataTemplate)GetValue(PopupContentTemplateProperty); }
            set { SetValue(PopupContentTemplateProperty, value); }
        }
        #endregion

        #region PopupContentStringFormatProperty
        public static DependencyProperty PopupContentStringFormatProperty = DependencyProperty.Register("PopupContentStringFormat", typeof(string), typeof(PopupContainer));
        public string PopupContentStringFormat
        {
            get { return (string)GetValue(PopupContentStringFormatProperty); }
            set { SetValue(PopupContentStringFormatProperty, value); }
        }
        #endregion

        #region PopupHorizontalAlignmentProperty
        public static DependencyProperty PopupHorizontalAlignmentProperty = DependencyProperty.Register("PopupHorizontalAlignment", typeof(HorizontalAlignment), typeof(PopupContainer));
        public HorizontalAlignment PopupHorizontalAlignment
        {
            get { return (HorizontalAlignment)GetValue(PopupHorizontalAlignmentProperty); }
            set { SetValue(PopupHorizontalAlignmentProperty, value); }
        }
        #endregion

        #region PopupVerticalAlignmentProperty
        public static DependencyProperty PopupVerticalAlignmentProperty = DependencyProperty.Register("PopupVerticalAlignment", typeof(VerticalAlignment), typeof(PopupContainer));
        public VerticalAlignment PopupVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(PopupVerticalAlignmentProperty); }
            set { SetValue(PopupVerticalAlignmentProperty, value); }
        }
        #endregion
    }
}