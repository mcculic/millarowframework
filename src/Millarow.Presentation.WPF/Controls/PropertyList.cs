using System.Windows;
using System.Windows.Controls;

namespace Millarow.Presentation.WPF.Controls
{
    public class PropertyList : ItemsControl
    {
        static PropertyList()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyList), new FrameworkPropertyMetadata(typeof(PropertyList)));
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new PropertyListItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is PropertyListItem;
        }
    }
}