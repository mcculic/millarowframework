using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace Millarow.Presentation.WPF.Converters
{
    public class BoolToVisibilityConverter : AbstractTypedConverter<bool, Visibility>
    {
        public BoolToVisibilityConverter(BoolToVisibilityConverterMode mode)
        {
            Mode = mode;
        }

        public BoolToVisibilityConverter()
            : this(BoolToVisibilityConverterMode.Collapse)
        {
        }

        public override Visibility Convert(bool value, object parameter, CultureInfo culture)
        {
            switch (Mode)
            {
                case BoolToVisibilityConverterMode.Collapse:
                    return value ? Visibility.Visible : Visibility.Collapsed;
                case BoolToVisibilityConverterMode.CollapseInverted:
                    return value ? Visibility.Collapsed : Visibility.Visible;
                case BoolToVisibilityConverterMode.Hide:
                    return value ? Visibility.Visible : Visibility.Hidden;
                case BoolToVisibilityConverterMode.HideInverted:
                    return value ? Visibility.Hidden : Visibility.Visible;
                default:
                    throw new NotImplementedException($"{nameof(BoolToVisibilityConverterMode)} value '{Mode}' is not implemented.");
            }
        }

        [ConstructorArgument("mode")]
        public BoolToVisibilityConverterMode Mode { get; }
    }
}