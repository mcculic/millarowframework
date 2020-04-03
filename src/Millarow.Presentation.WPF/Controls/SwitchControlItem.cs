using System.Windows;
using System.Windows.Controls;

namespace Millarow.Presentation.WPF.Controls
{
    public class SwitchControlItem : ContentControl
    {
        static SwitchControlItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SwitchControlItem), new FrameworkPropertyMetadata(typeof(SwitchControlItem)));
        }

        protected virtual void OnSwitchChanged(object oldValue, object newValue)
        {
            if (Parent is SwitchControl parent)
                parent.Update();
        }

        #region SwitchProperty
        public static DependencyProperty SwitchProperty = DependencyProperty.Register("Switch", typeof(object), typeof(SwitchControlItem), new PropertyMetadata(OnSwitchChanged));
        public object Switch
        {
            get { return (object)GetValue(SwitchProperty); }
            set { SetValue(SwitchProperty, value); }
        }

        private static void OnSwitchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SwitchControlItem)
                ((SwitchControlItem)d).OnSwitchChanged((object)e.OldValue, (object)e.NewValue);
        }
        #endregion
    }
}
