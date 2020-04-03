using Millarow.Presentation.WPF.Framework;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;

namespace Millarow.Presentation.WPF.Controls
{
    [TemplatePart(Name = nameof(PART_TitleBar), Type = typeof(UIElement))]
    [TemplatePart(Name = nameof(PART_Icon), Type = typeof(UIElement))]
    public class CustomWindow : Window
    {
        static CustomWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomWindow), new FrameworkPropertyMetadata(typeof(CustomWindow)));
        }

        public CustomWindow()
        {
            CloseCommand = new DelegateCommand(Close);
            MinimizeCommand = new DelegateCommand(Minimize);
            MaximizeCommand = new DelegateCommand(Maximize);
            RestoreCommand = new DelegateCommand(Restore);

            StateChanged += CustomWindow_StateChanged;
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
                        chrome.NonClientFrameEdges = NonClientFrameEdges.Bottom;
                }
            }
        }

        private void Minimize()
        {
            SystemCommands.MinimizeWindow(this);
        }

        private void Maximize()
        {
            SystemCommands.MaximizeWindow(this);
        }

        private void Restore()
        {
            SystemCommands.RestoreWindow(this);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_TitleBar = GetTemplateChild(nameof(PART_TitleBar)) as UIElement;
            PART_Icon = GetTemplateChild(nameof(PART_Icon)) as UIElement;

            if (PART_Icon != null)
                PART_Icon.MouseDown += icon_MouseDown;
        }

        private void icon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && e.LeftButton == MouseButtonState.Pressed)
                Close();
            else
            {
                if (e.LeftButton == MouseButtonState.Pressed && PART_TitleBar != null)
                    ShowSystemMenu(new Point(0, PART_TitleBar.RenderSize.Height));
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

        protected UIElement PART_TitleBar { get; private set; }

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

        #region TitleBarHeightProperty
        public static DependencyProperty TitleBarHeightProperty = DependencyProperty.Register("TitleBarHeight", typeof(double), typeof(CustomWindow));
        public double TitleBarHeight
        {
            get { return (double)GetValue(TitleBarHeightProperty); }
            set { SetValue(TitleBarHeightProperty, value); }
        }
        #endregion

        #region TitleBarBackgroundProperty
        public static DependencyProperty TitleBarBackgroundProperty = DependencyProperty.Register("TitleBarBackground", typeof(Brush), typeof(CustomWindow));
        public Brush TitleBarBackground
        {
            get { return (Brush)GetValue(TitleBarBackgroundProperty); }
            set { SetValue(TitleBarBackgroundProperty, value); }
        }
        #endregion

        #region TitleBarIconProperty
        public static DependencyProperty TitleBarIconProperty = DependencyProperty.Register("TitleBarIcon", typeof(ImageSource), typeof(CustomWindow));
        public ImageSource TitleBarIcon
        {
            get { return (ImageSource)GetValue(TitleBarIconProperty); }
            set { SetValue(TitleBarIconProperty, value); }
        }
        #endregion

        #region ShowIconProperty
        public static DependencyProperty ShowIconProperty = DependencyProperty.Register("ShowIcon", typeof(bool), typeof(CustomWindow));
        public bool ShowIcon
        {
            get { return (bool)GetValue(ShowIconProperty); }
            set { SetValue(ShowIconProperty, value); }
        }
        #endregion

        #region IconWidthProperty
        public static DependencyProperty IconWidthProperty = DependencyProperty.Register("IconWidth", typeof(double), typeof(CustomWindow));
        public double IconWidth
        {
            get { return (double)GetValue(IconWidthProperty); }
            set { SetValue(IconWidthProperty, value); }
        }
        #endregion

        #region IconHeightProperty
        public static DependencyProperty IconHeightProperty = DependencyProperty.Register("IconHeight", typeof(double), typeof(CustomWindow));
        public double IconHeight
        {
            get { return (double)GetValue(IconHeightProperty); }
            set { SetValue(IconHeightProperty, value); }
        }
        #endregion

        #region CloseCommandProperty
        private static DependencyPropertyKey CloseCommandPropertyKey = DependencyProperty.RegisterReadOnly("CloseCommand", typeof(ICommand), typeof(CustomWindow), new PropertyMetadata(null));
        public static DependencyProperty CloseCommandProperty = CloseCommandPropertyKey.DependencyProperty;
        public ICommand CloseCommand
        {
            get { return (ICommand)GetValue(CloseCommandProperty); }
            private set { SetValue(CloseCommandPropertyKey, value); }
        }
        #endregion

        #region RestoreCommandProperty
        private static DependencyPropertyKey RestoreCommandPropertyKey = DependencyProperty.RegisterReadOnly("RestoreCommand", typeof(ICommand), typeof(CustomWindow), new PropertyMetadata(null));
        public static DependencyProperty RestoreCommandProperty = RestoreCommandPropertyKey.DependencyProperty;
        public ICommand RestoreCommand
        {
            get { return (ICommand)GetValue(RestoreCommandProperty); }
            private set { SetValue(RestoreCommandPropertyKey, value); }
        }
        #endregion

        #region MaximizeCommandProperty
        private static DependencyPropertyKey MaximizeCommandPropertyKey = DependencyProperty.RegisterReadOnly("MaximizeCommand", typeof(ICommand), typeof(CustomWindow), new PropertyMetadata(null));
        public static DependencyProperty MaximizeCommandProperty = MaximizeCommandPropertyKey.DependencyProperty;
        public ICommand MaximizeCommand
        {
            get { return (ICommand)GetValue(MaximizeCommandProperty); }
            private set { SetValue(MaximizeCommandPropertyKey, value); }
        }
        #endregion

        #region MinimizeCommandProperty
        private static DependencyPropertyKey MinimizeCommandPropertyKey = DependencyProperty.RegisterReadOnly("MinimizeCommand", typeof(ICommand), typeof(CustomWindow), new PropertyMetadata(null));
        public static DependencyProperty MinimizeCommandProperty = MinimizeCommandPropertyKey.DependencyProperty;
        public ICommand MinimizeCommand
        {
            get { return (ICommand)GetValue(MinimizeCommandProperty); }
            private set { SetValue(MinimizeCommandPropertyKey, value); }
        }
        #endregion

        #region TitleBarForegroundProperty

        public static readonly DependencyProperty TitleBarForegroundProperty = DependencyProperty.Register("TitleBarForeground", typeof(Brush), typeof(CustomWindow));

        public Brush TitleBarForeground
        {
            get { return (Brush)GetValue(TitleBarForegroundProperty); }
            set { SetValue(TitleBarForegroundProperty, value); }
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
    }
}
