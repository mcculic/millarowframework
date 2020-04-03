using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Millarow.Presentation.WPF.Controls
{
    public class ModalHostItem : ContentControl
    {
        static ModalHostItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ModalHostItem), new FrameworkPropertyMetadata(typeof(ModalHostItem)));
        }

        public ModalHostItem()
        {
            KeyboardNavigation.SetTabNavigation(this, KeyboardNavigationMode.Cycle);
            Focusable = true;

            PreviewGotKeyboardFocus += ModalHostItem_PreviewGotKeyboardFocus;
        }

        private void ModalHostItem_PreviewGotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            OldFocus = e.OldFocus;
        }

        protected virtual void OnIsActiveChanged(bool oldValue, bool newValue)
        {
            if (!newValue && OldFocus != null)
            {
                OldFocus.Focus();
                OldFocus = null;
            }
        }

        private IInputElement OldFocus { get; set; }

        #region IsActiveProperty
        public static DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(ModalHostItem), new PropertyMetadata(OnIsActiveChanged));
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        private static void OnIsActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ModalHostItem)
                ((ModalHostItem)d).OnIsActiveChanged((bool)e.OldValue, (bool)e.NewValue);
        }
        #endregion
    }
}