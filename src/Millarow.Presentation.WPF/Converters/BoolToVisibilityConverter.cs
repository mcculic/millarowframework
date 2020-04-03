using System.Globalization;
using System.Windows;

namespace Millarow.Presentation.WPF.Converters
{
    public class BoolToVisibilityConverter : AbstractTypedConverter<bool, Visibility>
    {
        public BoolToVisibilityConverter()
        {
            TrueVisibility = Visibility.Visible;
            FalseVisibility = Visibility.Collapsed;
        }

        public override Visibility Convert(bool value, object parameter, CultureInfo culture)
        {
            if (value)
                return TrueVisibility;
            else
                return FalseVisibility;
        }

        public Visibility FalseVisibility { get; set; }

        public Visibility TrueVisibility { get; set; }
    }
}