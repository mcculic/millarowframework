using System.Windows;
using System.Windows.Controls;

namespace Millarow.Presentation.WPF.Controls
{
    public class PropertyListItem : HeaderedContentControl
    {
        static PropertyListItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PropertyListItem), new FrameworkPropertyMetadata(typeof(PropertyListItem)));
        }

        #region PropertyNameProperty
        public static DependencyProperty PropertyNameProperty = DependencyProperty.Register("PropertyName", typeof(string), typeof(PropertyListItem));
        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }
        #endregion

        #region HeaderAnchorProperty
        private const PropertyListItemHeaderAnchor DefaultHeaderAnchor = PropertyListItemHeaderAnchor.Left;
        public static DependencyProperty HeaderAnchorProperty = DependencyProperty.Register("HeaderAnchor", typeof(PropertyListItemHeaderAnchor), typeof(PropertyListItem), new PropertyMetadata(DefaultHeaderAnchor));
        public PropertyListItemHeaderAnchor HeaderAnchor
        {
            get { return (PropertyListItemHeaderAnchor)GetValue(HeaderAnchorProperty); }
            set { SetValue(HeaderAnchorProperty, value); }
        }
        #endregion
    }
}