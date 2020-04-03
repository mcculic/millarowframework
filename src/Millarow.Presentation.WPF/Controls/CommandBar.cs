using System.Windows;
using System.Windows.Controls;

namespace Millarow.Presentation.WPF.Controls
{
    public class CommandBar : ItemsControl
    {
        static CommandBar()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CommandBar), new FrameworkPropertyMetadata(typeof(CommandBar)));
        }

        public CommandBar()
        {
            Focusable = false;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CommandBarItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is CommandBarItem;
        }

        #region OrientationProperty
        public static DependencyProperty OrientationProperty = DependencyProperty.Register("Orientation", typeof(Orientation), typeof(CommandBar));
        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        #endregion
    }
}