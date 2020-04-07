using Millarow.Presentation.WPF.Framework;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;

namespace Millarow.Presentation.WPF.Controls
{
    [TemplatePart(Name = nameof(PART_Header), Type = typeof(UIElement))]
    [TemplatePart(Name = nameof(PART_Icon), Type = typeof(UIElement))]
    public class CustomWindow : Window
    {
        static CustomWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(typeof(CustomWindow)));
        }

        public CustomWindow()
        {
            SetActualHeaderBrushes();
            StateChanged += CustomWindow_StateChanged;
            Activated += CustomWindow_Activated;
            Deactivated += CustomWindow_Deactivated;

            CloseCommand = new DelegateCommand(() => SystemCommands.CloseWindow(this));
            MaximizeCommand = new DelegateCommand(() => SystemCommands.MaximizeWindow(this));
            MinimizeCommand = new DelegateCommand(() => SystemCommands.MinimizeWindow(this));
            RestoreCommand = new DelegateCommand(() => SystemCommands.RestoreWindow(this));
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (PART_Icon != null)
                PART_Icon.MouseDown -= OnIconMouseDown;

            PART_Header = GetTemplateChild(nameof(PART_Header)) as UIElement;
            PART_Icon = GetTemplateChild(nameof(PART_Icon)) as UIElement;

            if (PART_Icon != null)
                PART_Icon.MouseDown += OnIconMouseDown;
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property.In(ActiveHeaderBackgroundProperty, ActiveHeaderForegroundProperty, InactiveHeaderBackgroundProperty, InactiveHeaderForegroundProperty))
                SetActualHeaderBrushes();
        }

        private void SetActualHeaderBrushes()
        {
            ActualHeaderBackground = IsActive ? ActiveHeaderBackground : InactiveHeaderBackground;
            ActualHeaderForeground = IsActive ? ActiveHeaderForeground : InactiveHeaderForeground;
        }

        private void CustomWindow_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                var taskbar = new Interop.Taskbar();
                if (taskbar.AutoHide)
                {
                    var chrome = WindowChrome.GetWindowChrome(this);
                    if (chrome != null)
                        chrome.NonClientFrameEdges = (NonClientFrameEdges)Enum.Parse(typeof(NonClientFrameEdges), taskbar.Position.ToString(), true);
                }
            }
        }

        private void CustomWindow_Deactivated(object sender, EventArgs e)
        {
            SetActualHeaderBrushes();
        }

        private void CustomWindow_Activated(object sender, EventArgs e)
        {
            SetActualHeaderBrushes();
        }

        private void OnIconMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && e.LeftButton == MouseButtonState.Pressed)
                Close();
            else
            {
                if (e.LeftButton == MouseButtonState.Pressed && PART_Header != null)
                    ShowSystemMenu(new Point(0, PART_Header.RenderSize.Height));
                else
                    ShowSystemMenu(e.GetPosition(this));
            }
        }

        private void ShowSystemMenu(Point pt)
        {
            SystemCommands.ShowSystemMenu(this, PointToScreen(pt));
        }

        public Thickness MaximizedMargin
        {
            get
            {
                if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.MajorRevision >= 2)
                    return new Thickness(0);

                var m1 = SystemParameters.WindowResizeBorderThickness;

                return new Thickness
                {
                    Top = m1.Top * 2,
                    Bottom = m1.Bottom  * 2,
                    Left = m1.Left * 2,
                    Right = m1.Right * 2
                };
            }
        }

        protected UIElement PART_Header { get; private set; }

        protected UIElement PART_Icon { get; private set; }

        #region HeaderProperty
        public static DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(object), typeof(CustomWindow));
        public object Header
        {
            get { return (object)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        #endregion

        #region HeaderTemplateProperty
        public static DependencyProperty HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(CustomWindow));
        public DataTemplate HeaderTemplate
        {
            get { return (DataTemplate)GetValue(HeaderTemplateProperty); }
            set { SetValue(HeaderTemplateProperty, value); }
        }
        #endregion

        #region HeaderHeightProperty
        public static DependencyProperty HeaderHeightProperty = DependencyProperty.Register("HeaderHeight", typeof(double), typeof(CustomWindow));
        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }
        #endregion

        #region HeaderFontFamilyProperty
        public static DependencyProperty HeaderFontFamilyProperty = DependencyProperty.Register("HeaderFontFamily", typeof(FontFamily), typeof(CustomWindow));
        public FontFamily HeaderFontFamily
        {
            get { return (FontFamily)GetValue(HeaderFontFamilyProperty); }
            set { SetValue(HeaderFontFamilyProperty, value); }
        }
        #endregion

        #region HeaderFontSizeProperty
        public static DependencyProperty HeaderFontSizeProperty = DependencyProperty.Register("HeaderFontSize", typeof(double), typeof(CustomWindow));
        public double HeaderFontSize
        {
            get { return (double)GetValue(HeaderFontSizeProperty); }
            set { SetValue(HeaderFontSizeProperty, value); }
        }
        #endregion

        #region HeaderFontWeightProperty
        public static DependencyProperty HeaderFontWeightProperty = DependencyProperty.Register("HeaderFontWeight", typeof(FontWeight), typeof(CustomWindow));
        public FontWeight HeaderFontWeight
        {
            get { return (FontWeight)GetValue(HeaderFontWeightProperty); }
            set { SetValue(HeaderFontWeightProperty, value); }
        }
        #endregion

        #region HeaderFontStyleProperty
        public static DependencyProperty HeaderFontStyleProperty = DependencyProperty.Register("HeaderFontStyle", typeof(FontStyle), typeof(CustomWindow));
        public FontStyle HeaderFontStyle
        {
            get { return (FontStyle)GetValue(HeaderFontStyleProperty); }
            set { SetValue(HeaderFontStyleProperty, value); }
        }
        #endregion

        #region ShowHeaderIconProperty
        public static DependencyProperty ShowHeaderIconProperty = DependencyProperty.Register("ShowHeaderIcon", typeof(bool), typeof(CustomWindow), new PropertyMetadata(true));
        public bool ShowHeaderIcon
        {
            get { return (bool)GetValue(ShowHeaderIconProperty); }
            set { SetValue(ShowHeaderIconProperty, value); }
        }
        #endregion

        #region HeaderIconProperty
        public static DependencyProperty HeaderIconProperty = DependencyProperty.Register("HeaderIcon", typeof(object), typeof(CustomWindow));
        public object HeaderIcon
        {
            get { return (object)GetValue(HeaderIconProperty); }
            set { SetValue(HeaderIconProperty, value); }
        }
        #endregion

        #region HeaderIconTemplateProperty
        public static DependencyProperty HeaderIconTemplateProperty = DependencyProperty.Register("HeaderIconTemplate", typeof(DataTemplate), typeof(CustomWindow));
        public DataTemplate HeaderIconTemplate
        {
            get { return (DataTemplate)GetValue(HeaderIconTemplateProperty); }
            set { SetValue(HeaderIconTemplateProperty, value); }
        }
        #endregion

        #region SystemButtonStyleProperty
        public static DependencyProperty SystemButtonStyleProperty = DependencyProperty.Register("SystemButtonStyle", typeof(Style), typeof(CustomWindow));
        public Style SystemButtonStyle
        {
            get { return (Style)GetValue(SystemButtonStyleProperty); }
            set { SetValue(SystemButtonStyleProperty, value); }
        }
        #endregion

        #region ActiveHeaderBackgroundProperty
        public static DependencyProperty ActiveHeaderBackgroundProperty = DependencyProperty.Register("ActiveHeaderBackground", typeof(Brush), typeof(CustomWindow));
        public Brush ActiveHeaderBackground
        {
            get { return (Brush)GetValue(ActiveHeaderBackgroundProperty); }
            set { SetValue(ActiveHeaderBackgroundProperty, value); }
        }
        #endregion

        #region InactiveHeaderBackgroundProperty
        public static DependencyProperty InactiveHeaderBackgroundProperty = DependencyProperty.Register("InactiveHeaderBackground", typeof(Brush), typeof(CustomWindow));
        public Brush InactiveHeaderBackground
        {
            get { return (Brush)GetValue(InactiveHeaderBackgroundProperty); }
            set { SetValue(InactiveHeaderBackgroundProperty, value); }
        }
        #endregion

        #region ActiveHeaderForegroundProperty
        public static DependencyProperty ActiveHeaderForegroundProperty = DependencyProperty.Register("ActiveHeaderForeground", typeof(Brush), typeof(CustomWindow));
        public Brush ActiveHeaderForeground
        {
            get { return (Brush)GetValue(ActiveHeaderForegroundProperty); }
            set { SetValue(ActiveHeaderForegroundProperty, value); }
        }
        #endregion

        #region InactiveHeaderForegroundProperty
        public static DependencyProperty InactiveHeaderForegroundProperty = DependencyProperty.Register("InactiveHeaderForeground", typeof(Brush), typeof(CustomWindow));
        public Brush InactiveHeaderForeground
        {
            get { return (Brush)GetValue(InactiveHeaderForegroundProperty); }
            set { SetValue(InactiveHeaderForegroundProperty, value); }
        }
        #endregion

        #region ShowMaximizeButtonProperty
        public static DependencyProperty ShowMaximizeButtonProperty = DependencyProperty.Register("ShowMaximizeButton", typeof(bool), typeof(CustomWindow), new PropertyMetadata(true));
        public bool ShowMaximizeButton
        {
            get { return (bool)GetValue(ShowMaximizeButtonProperty); }
            set { SetValue(ShowMaximizeButtonProperty, value); }
        }
        #endregion

        #region MaximizeButtonContentProperty
        public static DependencyProperty MaximizeButtonContentProperty = DependencyProperty.Register("MaximizeButtonContent", typeof(object), typeof(CustomWindow));
        public object MaximizeButtonContent
        {
            get { return (object)GetValue(MaximizeButtonContentProperty); }
            set { SetValue(MaximizeButtonContentProperty, value); }
        }
        #endregion

        #region MaximizeButtonContentTemplateProperty
        public static DependencyProperty MaximizeButtonContentTemplateProperty = DependencyProperty.Register("MaximizeButtonContentTemplate", typeof(DataTemplate), typeof(CustomWindow));
        public DataTemplate MaximizeButtonContentTemplate
        {
            get { return (DataTemplate)GetValue(MaximizeButtonContentTemplateProperty); }
            set { SetValue(MaximizeButtonContentTemplateProperty, value); }
        }
        #endregion

        #region MaximizeButtonStyleProperty
        public static DependencyProperty MaximizeButtonStyleProperty = DependencyProperty.Register("MaximizeButtonStyle", typeof(Style), typeof(CustomWindow));
        public Style MaximizeButtonStyle
        {
            get { return (Style)GetValue(MaximizeButtonStyleProperty); }
            set { SetValue(MaximizeButtonStyleProperty, value); }
        }
        #endregion

        #region ShowMinimizeButtonProperty
        public static DependencyProperty ShowMinimizeButtonProperty = DependencyProperty.Register("ShowMinimizeButton", typeof(bool), typeof(CustomWindow), new PropertyMetadata(true));
        public bool ShowMinimizeButton
        {
            get { return (bool)GetValue(ShowMinimizeButtonProperty); }
            set { SetValue(ShowMinimizeButtonProperty, value); }
        }
        #endregion

        #region MinimizeButtonContentProperty
        public static DependencyProperty MinimizeButtonContentProperty = DependencyProperty.Register("MinimizeButtonContent", typeof(object), typeof(CustomWindow));
        public object MinimizeButtonContent
        {
            get { return (object)GetValue(MinimizeButtonContentProperty); }
            set { SetValue(MinimizeButtonContentProperty, value); }
        }
        #endregion

        #region MinimizeButtonContentTemplateProperty
        public static DependencyProperty MinimizeButtonContentTemplateProperty = DependencyProperty.Register("MinimizeButtonContentTemplate", typeof(DataTemplate), typeof(CustomWindow));
        public DataTemplate MinimizeButtonContentTemplate
        {
            get { return (DataTemplate)GetValue(MinimizeButtonContentTemplateProperty); }
            set { SetValue(MinimizeButtonContentTemplateProperty, value); }
        }
        #endregion

        #region ShowRestoreButtonProperty
        public static DependencyProperty ShowRestoreButtonProperty = DependencyProperty.Register("ShowRestoreButton", typeof(bool), typeof(CustomWindow), new PropertyMetadata(true));
        public bool ShowRestoreButton
        {
            get { return (bool)GetValue(ShowRestoreButtonProperty); }
            set { SetValue(ShowRestoreButtonProperty, value); }
        }
        #endregion

        #region RestoreButtonContentProperty
        public static DependencyProperty RestoreButtonContentProperty = DependencyProperty.Register("RestoreButtonContent", typeof(object), typeof(CustomWindow));
        public object RestoreButtonContent
        {
            get { return (object)GetValue(RestoreButtonContentProperty); }
            set { SetValue(RestoreButtonContentProperty, value); }
        }
        #endregion

        #region RestoreButtonContentTemplateProperty
        public static DependencyProperty RestoreButtonContentTemplateProperty = DependencyProperty.Register("RestoreButtonContentTemplate", typeof(DataTemplate), typeof(CustomWindow));
        public DataTemplate RestoreButtonContentTemplate
        {
            get { return (DataTemplate)GetValue(RestoreButtonContentTemplateProperty); }
            set { SetValue(RestoreButtonContentTemplateProperty, value); }
        }
        #endregion

        #region ShowCloseButtonProperty
        public static DependencyProperty ShowCloseButtonProperty = DependencyProperty.Register("ShowCloseButton", typeof(bool), typeof(CustomWindow), new PropertyMetadata(true));
        public bool ShowCloseButton
        {
            get { return (bool)GetValue(ShowCloseButtonProperty); }
            set { SetValue(ShowCloseButtonProperty, value); }
        }
        #endregion

        #region CloseButtonContentProperty
        public static DependencyProperty CloseButtonContentProperty = DependencyProperty.Register("CloseButtonContent", typeof(object), typeof(CustomWindow));
        public object CloseButtonContent
        {
            get { return (object)GetValue(CloseButtonContentProperty); }
            set { SetValue(CloseButtonContentProperty, value); }
        }
        #endregion

        #region CloseButtonContentTemplateProperty
        public static DependencyProperty CloseButtonContentTemplateProperty = DependencyProperty.Register("CloseButtonContentTemplate", typeof(DataTemplate), typeof(CustomWindow));
        public DataTemplate CloseButtonContentTemplate
        {
            get { return (DataTemplate)GetValue(CloseButtonContentTemplateProperty); }
            set { SetValue(CloseButtonContentTemplateProperty, value); }
        }
        #endregion

        #region ActualHeaderBackgroundProperty
        private static DependencyPropertyKey ActualHeaderBackgroundPropertyKey = DependencyProperty.RegisterReadOnly("ActualHeaderBackground", typeof(Brush), typeof(CustomWindow), new PropertyMetadata());
        public static DependencyProperty ActualHeaderBackgroundProperty = ActualHeaderBackgroundPropertyKey.DependencyProperty;
        public Brush ActualHeaderBackground
        {
            get { return (Brush)GetValue(ActualHeaderBackgroundProperty); }
            private set { SetValue(ActualHeaderBackgroundPropertyKey, value); }
        }
        #endregion

        #region ActualHeaderForegroundProperty
        private static DependencyPropertyKey ActualHeaderForegroundPropertyKey = DependencyProperty.RegisterReadOnly("ActualHeaderForeground", typeof(Brush), typeof(CustomWindow), new PropertyMetadata());
        public static DependencyProperty ActualHeaderForegroundProperty = ActualHeaderForegroundPropertyKey.DependencyProperty;
        public Brush ActualHeaderForeground
        {
            get { return (Brush)GetValue(ActualHeaderForegroundProperty); }
            private set { SetValue(ActualHeaderForegroundPropertyKey, value); }
        }
        #endregion

        public ICommand CloseCommand { get; }

        public ICommand MaximizeCommand { get; }

        public ICommand MinimizeCommand { get; }

        public ICommand RestoreCommand { get; }
    }
}
