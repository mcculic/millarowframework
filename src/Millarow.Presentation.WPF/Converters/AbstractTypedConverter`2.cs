using System;
using System.Globalization;
using System.Windows.Data;

namespace Millarow.Presentation.WPF.Converters
{
    public abstract class AbstractTypedConverter<TFrom, TTo> : IValueConverter
    {
        public abstract TTo Convert(TFrom value, object parameter, CultureInfo culture);

        public virtual TFrom ConvertBack(TTo value, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TFrom typedValue)
                return Convert(typedValue, parameter, culture);
            else
                return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TTo typedValue)
                return ConvertBack(typedValue, parameter, culture);
            else
                return null;
        }
    }
}