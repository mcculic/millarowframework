using System;
using System.Globalization;
using System.Windows.Data;

namespace Millarow.Presentation.WPF.Converters
{
    public abstract class AbstractTypedConverter<TFrom, TTo, TParameter> : IValueConverter
    {
        public abstract TTo Convert(TFrom value, TParameter parameter, CultureInfo culture);

        public virtual TFrom ConvertBack(TTo value, TParameter parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TFrom typedValue && parameter is TParameter typedParameter)
                return Convert(typedValue, typedParameter, culture);
            else
                return null;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TTo typedValue && parameter is TParameter typedParameter)
                return ConvertBack(typedValue, typedParameter, culture);
            else
                return null;
        }
    }
}
