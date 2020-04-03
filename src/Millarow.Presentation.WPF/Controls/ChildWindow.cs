using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Millarow.Presentation.WPF.Controls
{
    [TemplatePart(Name = nameof(PART_Title))]
    [TemplatePart(Name = nameof(PART_Header))]
    [TemplatePart(Name = nameof(PART_Icon))]
    [TemplatePart(Name = nameof(PART_Commands))]
    [TemplatePart(Name = nameof(PART_Content))]
    public class ChildWindow : HeaderedContentControl
    {
        static ChildWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChildWindow), new FrameworkPropertyMetadata(typeof(ChildWindow)));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Title = GetTemplateChild(nameof(PART_Title)) as UIElement;
            PART_Icon = GetTemplateChild(nameof(PART_Icon)) as UIElement;
            PART_Header = GetTemplateChild(nameof(PART_Header)) as UIElement;
            PART_Commands = GetTemplateChild(nameof(PART_Commands)) as UIElement;
            PART_Content = GetTemplateChild(nameof(PART_Content)) as UIElement;
        }

        public UIElement PART_Header { get; protected set; }
        public UIElement PART_Icon { get; protected set; }
        public UIElement PART_Title { get; protected set; }
        public UIElement PART_Commands { get; protected set; }
        public UIElement PART_Content { get; protected set; }

        #region HeaderPaddingProperty
        public static DependencyProperty HeaderPaddingProperty = DependencyProperty.Register("HeaderPadding", typeof(Thickness), typeof(ChildWindow));
        public Thickness HeaderPadding
        {
            get { return (Thickness)GetValue(HeaderPaddingProperty); }
            set { SetValue(HeaderPaddingProperty, value); }
        }
        #endregion

        #region ShowTitleProperty
        public static DependencyProperty ShowTitleProperty = DependencyProperty.Register("ShowTitle", typeof(bool), typeof(ChildWindow), new PropertyMetadata(true));
        public bool ShowTitle
        {
            get { return (bool)GetValue(ShowTitleProperty); }
            set { SetValue(ShowTitleProperty, value); }
        }
        #endregion

        #region TitleBackgroundProperty
        public static DependencyProperty TitleBackgroundProperty = DependencyProperty.Register("TitleBackground", typeof(Brush), typeof(ChildWindow));
        public Brush TitleBackground
        {
            get { return (Brush)GetValue(TitleBackgroundProperty); }
            set { SetValue(TitleBackgroundProperty, value); }
        }
        #endregion

        #region TitleMinHeightProperty
        public static DependencyProperty TitleMinHeightProperty = DependencyProperty.Register("TitleMinHeight", typeof(double), typeof(ChildWindow));
        public double TitleMinHeight
        {
            get { return (double)GetValue(TitleMinHeightProperty); }
            set { SetValue(TitleMinHeightProperty, value); }
        }
        #endregion

        #region HeaderForegroundProperty
        public static DependencyProperty HeaderForegroundProperty = DependencyProperty.Register("HeaderForeground", typeof(Brush), typeof(ChildWindow));
        public Brush HeaderForeground
        {
            get { return (Brush)GetValue(HeaderForegroundProperty); }
            set { SetValue(HeaderForegroundProperty, value); }
        }
        #endregion

        #region HeaderVerticalAlignmentProperty
        public static DependencyProperty HeaderVerticalAlignmentProperty = DependencyProperty.Register("HeaderVerticalAlignment", typeof(VerticalAlignment), typeof(ChildWindow));
        public VerticalAlignment HeaderVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(HeaderVerticalAlignmentProperty); }
            set { SetValue(HeaderVerticalAlignmentProperty, value); }
        }
        #endregion

        #region ShowIconProperty
        public static DependencyProperty ShowIconProperty = DependencyProperty.Register("ShowIcon", typeof(bool), typeof(ChildWindow), new PropertyMetadata(true));
        public bool ShowIcon
        {
            get { return (bool)GetValue(ShowIconProperty); }
            set { SetValue(ShowIconProperty, value); }
        }
        #endregion

        #region IconProperty
        public static DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(object), typeof(ChildWindow));
        public object Icon
        {
            get { return (object)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        #endregion

        #region IconPaddingProperty
        public static DependencyProperty IconPaddingProperty = DependencyProperty.Register("IconPadding", typeof(Thickness), typeof(ChildWindow));
        public Thickness IconPadding
        {
            get { return (Thickness)GetValue(IconPaddingProperty); }
            set { SetValue(IconPaddingProperty, value); }
        }
        #endregion

        #region IconTemplateProperty
        public static DependencyProperty IconTemplateProperty = DependencyProperty.Register("IconTemplate", typeof(DataTemplate), typeof(ChildWindow));
        public DataTemplate IconTemplate
        {
            get { return (DataTemplate)GetValue(IconTemplateProperty); }
            set { SetValue(IconTemplateProperty, value); }
        }
        #endregion

        #region IconTemplateSelectorProperty
        public static DependencyProperty IconTemplateSelectorProperty = DependencyProperty.Register("IconTemplateSelector", typeof(DataTemplateSelector), typeof(ChildWindow));
        public DataTemplateSelector IconTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(IconTemplateSelectorProperty); }
            set { SetValue(IconTemplateSelectorProperty, value); }
        }
        #endregion

        #region IconStringFormatProperty
        public static DependencyProperty IconStringFormatProperty = DependencyProperty.Register("IconStringFormat", typeof(string), typeof(ChildWindow));
        public string IconStringFormat
        {
            get { return (string)GetValue(IconStringFormatProperty); }
            set { SetValue(IconStringFormatProperty, value); }
        }
        #endregion

        #region IconVerticalAlignmentProperty
        public static DependencyProperty IconVerticalAlignmentProperty = DependencyProperty.Register("IconVerticalAlignment", typeof(VerticalAlignment), typeof(ChildWindow));
        public VerticalAlignment IconVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(IconVerticalAlignmentProperty); }
            set { SetValue(IconVerticalAlignmentProperty, value); }
        }
        #endregion

        #region ShowCommandsProperty
        public static DependencyProperty ShowCommandsProperty = DependencyProperty.Register("ShowCommands", typeof(bool), typeof(ChildWindow), new PropertyMetadata(true));
        public bool ShowCommands
        {
            get { return (bool)GetValue(ShowCommandsProperty); }
            set { SetValue(ShowCommandsProperty, value); }
        }
        #endregion

        #region CommandsProperty
        public static DependencyProperty CommandsProperty = DependencyProperty.Register("Commands", typeof(object), typeof(ChildWindow));
        public object Commands
        {
            get { return (object)GetValue(CommandsProperty); }
            set { SetValue(CommandsProperty, value); }
        }
        #endregion

        #region CommandsTemplateProperty
        public static DependencyProperty CommandsTemplateProperty = DependencyProperty.Register("CommandsTemplate", typeof(DataTemplate), typeof(ChildWindow));
        public DataTemplate CommandsTemplate
        {
            get { return (DataTemplate)GetValue(CommandsTemplateProperty); }
            set { SetValue(CommandsTemplateProperty, value); }
        }
        #endregion

        #region CommandsTemplateSelectorProperty
        public static DependencyProperty CommandsTemplateSelectorProperty = DependencyProperty.Register("CommandsTemplateSelector", typeof(DataTemplateSelector), typeof(ChildWindow));
        public DataTemplateSelector CommandsTemplateSelector
        {
            get { return (DataTemplateSelector)GetValue(CommandsTemplateSelectorProperty); }
            set { SetValue(CommandsTemplateSelectorProperty, value); }
        }
        #endregion

        #region CommandsStringFormatProperty
        public static DependencyProperty CommandsStringFormatProperty = DependencyProperty.Register("CommandsStringFormat", typeof(string), typeof(ChildWindow));
        public string CommandsStringFormat
        {
            get { return (string)GetValue(CommandsStringFormatProperty); }
            set { SetValue(CommandsStringFormatProperty, value); }
        }
        #endregion

        #region CommandsPaddingProperty
        public static DependencyProperty CommandsPaddingProperty = DependencyProperty.Register("CommandsPadding", typeof(Thickness), typeof(ChildWindow));
        public Thickness CommandsPadding
        {
            get { return (Thickness)GetValue(CommandsPaddingProperty); }
            set { SetValue(CommandsPaddingProperty, value); }
        }
        #endregion

        #region CommandsVerticalAlignmentProperty
        public static DependencyProperty CommandsVerticalAlignmentProperty = DependencyProperty.Register("CommandsVerticalAlignment", typeof(VerticalAlignment), typeof(ChildWindow));
        public VerticalAlignment CommandsVerticalAlignment
        {
            get { return (VerticalAlignment)GetValue(CommandsVerticalAlignmentProperty); }
            set { SetValue(CommandsVerticalAlignmentProperty, value); }
        }
        #endregion

        #region DropShadowProperty
        public static DependencyProperty DropShadowProperty = DependencyProperty.Register("DropShadow", typeof(bool), typeof(ChildWindow), new PropertyMetadata(true));
        public bool DropShadow
        {
            get { return (bool)GetValue(DropShadowProperty); }
            set { SetValue(DropShadowProperty, value); }
        }
        #endregion

        #region IsActiveProperty
        public static DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(ChildWindow));
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }
        #endregion
    }
}