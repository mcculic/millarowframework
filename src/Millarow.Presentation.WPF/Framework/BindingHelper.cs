using System.Windows;
using System.Windows.Data;

namespace Millarow.Presentation.WPF.Framework
{
    public static class BindingHelper
    {
        public static void SetBinding(this DependencyObject target, DependencyProperty property, object source, string sourcePath)
        {
            target.AssertNotNull(nameof(target));

            BindingOperations.SetBinding(target, property, new Binding(sourcePath) { Source = source });
        }

        public static void SetBinding(this DependencyObject target, DependencyProperty property, string sourcePath, IValueConverter converter, object converterParam = null, object source = null)
        {
            target.AssertNotNull(nameof(target));

            var binding = new Binding(sourcePath)
            {
                Converter = converter,
                ConverterParameter = converterParam,
                Source = source
            };

            BindingOperations.SetBinding(target, property, binding);
        }

        public static void BindConverter(this DependencyObject target, DependencyProperty property, IValueConverter converter, object sourceValue)
        {
            target.AssertNotNull(nameof(target));

            var binding = new Binding
            {
                Converter = converter,
                Source = sourceValue,
                BindsDirectlyToSource = true
            };

            BindingOperations.SetBinding(target, property, binding);
        }
    }
}
