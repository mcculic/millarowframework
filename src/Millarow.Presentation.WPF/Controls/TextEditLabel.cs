using Millarow.Presentation.WPF.Framework;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Millarow.Presentation.WPF.Controls
{
    public class TextEditLabel : Control
    {
        private const string TextBoxViewTypeName = "TextBoxView";

        static TextEditLabel()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextEditLabel), new FrameworkPropertyMetadata(typeof(TextEditLabel)));
        }

        public TextEditLabel()
        {
            Loaded += OnLoaded;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            PART_Edit = GetTemplateChild(nameof(PART_Edit)) as TextBoxBase;
            PART_View = GetTemplateChild(nameof(PART_View)) as FrameworkElement;
        }

        public void SelectAll()
        {
            PART_Edit?.SelectAll();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!IsTemplateValid())
                return;

            var editView = PART_Edit.GetVisualChildrenDepthFirst()
                .OfType<UIElement>()
                .FirstOrDefault(x => x.GetType().Name.Equals(TextBoxViewTypeName, StringComparison.Ordinal));

            if (editView != null)
            {
                var lt = editView.TranslatePoint(new Point(0, 0), PART_Edit);
                PART_View.Margin = new Thickness(lt.X, lt.Y, 0, 0);
            }

            PART_View.Visibility = IsEditMode ? Visibility.Hidden : Visibility.Visible;
            PART_Edit.Visibility = IsEditMode ? Visibility.Visible : Visibility.Hidden;
        }

        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            if (IsKeyboardFocused && IsTemplateValid())
            {
                e.Handled = true;

                if (IsEditMode)
                    Keyboard.Focus(PART_Edit);
                else
                    Keyboard.Focus(PART_View);
            }

            base.OnGotKeyboardFocus(e);
        }

        protected virtual void OnIsEditModeChanged(bool oldValue, bool newValue)
        {
            if (!IsTemplateValid())
                return;

            if (newValue)
            {
                PART_Edit.Visibility = Visibility.Visible;

                if (PART_View.IsKeyboardFocused)
                    Keyboard.Focus(PART_Edit);

                PART_View.Visibility = Visibility.Hidden;
            }
            else
            {
                PART_View.Visibility = Visibility.Visible;

                if (PART_Edit.IsKeyboardFocused)
                    Keyboard.Focus(PART_View);

                PART_Edit.Visibility = Visibility.Hidden;
            }
        }

        private bool IsTemplateValid()
        {
            return PART_View != null && PART_Edit != null;
        }

        protected TextBoxBase PART_Edit { get; private set; }

        protected FrameworkElement PART_View { get; private set; }

        #region TextProperty
        public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(TextEditLabel), new FrameworkPropertyMetadata { BindsTwoWayByDefault = true });
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region IsEditModeProperty
        public static DependencyProperty IsEditModeProperty = DependencyProperty.Register("IsEditMode", typeof(bool), typeof(TextEditLabel), new PropertyMetadata(OnIsEditModeChanged));
        public bool IsEditMode
        {
            get { return (bool)GetValue(IsEditModeProperty); }
            set { SetValue(IsEditModeProperty, value); }
        }

        private static void OnIsEditModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextEditLabel)
                ((TextEditLabel)d).OnIsEditModeChanged((bool)e.OldValue, (bool)e.NewValue);
        }
        #endregion

        #region TextAlignmentProperty
        public static DependencyProperty TextAlignmentProperty = DependencyProperty.Register("TextAlignment", typeof(TextAlignment), typeof(TextEditLabel), new PropertyMetadata(TextAlignment.Left));
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextAlignmentProperty, value); }
        }
        #endregion

        #region MaxLengthProperty
        public static DependencyProperty MaxLengthProperty = DependencyProperty.Register("MaxLength", typeof(int), typeof(TextEditLabel), new PropertyMetadata(int.MaxValue));
        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }
        #endregion

        #region IsUndoEnabledProperty
        public static DependencyProperty IsUndoEnabledProperty = DependencyProperty.Register("IsUndoEnabled", typeof(bool), typeof(TextEditLabel), new PropertyMetadata(true));
        public bool IsUndoEnabled
        {
            get { return (bool)GetValue(IsUndoEnabledProperty); }
            set { SetValue(IsUndoEnabledProperty, value); }
        }
        #endregion
    }
}