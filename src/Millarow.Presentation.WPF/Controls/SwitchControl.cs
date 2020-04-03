using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Millarow.Presentation.WPF.Controls
{
    public class SwitchControl : ItemsControl
    {
        static SwitchControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SwitchControl), new FrameworkPropertyMetadata(typeof(SwitchControl)));
        }

        internal void Update()
        {
            foreach (var child in Items.OfType<SwitchControlItem>())
            {
                if (Equals(CurrentSwitch, child.Switch))
                    child.Visibility = Visibility.Visible;
                else
                    child.Visibility = Visibility.Collapsed;
            }
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is SwitchControlItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new SwitchControlItem();
        }

        protected virtual void OnCurrentSwitchChanged(object oldSwitch, object newSwitch)
        {
            Update();
        }

        #region CurrentSwitchProperty
        public static DependencyProperty CurrentSwitchProperty = DependencyProperty.Register("CurrentSwitch", typeof(object), typeof(SwitchControl), new PropertyMetadata(OnCurrentSwitchChanged));
        public object CurrentSwitch
        {
            get { return (object)GetValue(CurrentSwitchProperty); }
            set { SetValue(CurrentSwitchProperty, value); }
        }

        private static void OnCurrentSwitchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is SwitchControl)
                ((SwitchControl)d).OnCurrentSwitchChanged((object)e.OldValue, (object)e.NewValue);
        }
        #endregion
    }
}