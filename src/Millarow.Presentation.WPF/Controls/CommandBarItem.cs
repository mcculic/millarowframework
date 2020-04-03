using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Millarow.Presentation.WPF.Controls
{
    public class CommandBarItem : ContentControl
    {
        static CommandBarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CommandBarItem), new FrameworkPropertyMetadata(typeof(CommandBarItem)));
        }

        #region ContentPaddingProperty
        public static DependencyProperty ContentPaddingProperty = DependencyProperty.Register("ContentPadding", typeof(Thickness), typeof(CommandBarItem));
        public Thickness ContentPadding
        {
            get { return (Thickness)GetValue(ContentPaddingProperty); }
            set { SetValue(ContentPaddingProperty, value); }
        }
        #endregion

        #region IconProperty
        public static DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(object), typeof(CommandBarItem));
        public object Icon
        {
            get { return (object)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        #endregion

        #region IconTemplateProperty
        public static DependencyProperty IconTemplateProperty = DependencyProperty.Register("IconTemplate", typeof(DataTemplate), typeof(CommandBarItem));
        public DataTemplate IconTemplate
        {
            get { return (DataTemplate)GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        }
        #endregion

        #region IconTemplateSelectorProperty
        public static DependencyProperty IconTemplateSelectorProperty = DependencyProperty.Register("IconTemplateSelector", typeof(DataTemplateSelector), typeof(CommandBarItem));
        public DataTemplateSelector IconTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(IconTemplateSelectorProperty); }
            set { SetValue(IconTemplateSelectorProperty, value); }
        }
        #endregion

        #region IconStringFormatProperty
        public static DependencyProperty IconStringFormatProperty = DependencyProperty.Register("IconStringFormat", typeof(string), typeof(CommandBarItem));
        public string IconStringFormat
        {
            get { return (string)GetValue(IconStringFormatProperty); }
            set { SetValue(IconStringFormatProperty, value); }
        }
        #endregion

        #region IconVisibilityProperty
        public static DependencyProperty IconVisibilityProperty = DependencyProperty.Register("IconVisibility", typeof(Visibility), typeof(CommandBarItem));
        public Visibility IconVisibility
        {
            get { return (Visibility)GetValue(IconVisibilityProperty); }
            set { SetValue(IconVisibilityProperty, value); }
        }
        #endregion

        #region IconMarginProperty
        public static DependencyProperty IconMarginProperty = DependencyProperty.Register("IconMargin", typeof(Thickness), typeof(CommandBarItem));
        public Thickness IconMargin
        {
            get { return (Thickness)GetValue(IconMarginProperty); }
            set { SetValue(IconMarginProperty, value); }
        }
        #endregion

        #region IsDefaultProperty
        public static DependencyProperty IsDefaultProperty = DependencyProperty.Register("IsDefault", typeof(bool), typeof(CommandBarItem));
        public bool IsDefault
        {
            get { return (bool)GetValue(IsDefaultProperty); }
            set { SetValue(IsDefaultProperty, value); }
        }
        #endregion

        #region IsCancelProperty
        public static DependencyProperty IsCancelProperty = DependencyProperty.Register("IsCancel", typeof(bool), typeof(CommandBarItem));
        public bool IsCancel
        {
            get { return (bool)GetValue(IsCancelProperty); }
            set { SetValue(IsCancelProperty, value); }
        }
        #endregion

        #region CommandProperty
        public static DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(CommandBarItem));
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        #endregion

        #region CommandParameterProperty
        public static DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(CommandBarItem));
        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }
        #endregion

        
    }
}
