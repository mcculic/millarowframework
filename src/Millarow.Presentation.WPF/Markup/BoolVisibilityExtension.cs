using System;
using System.Windows;
using System.Windows.Markup;

namespace Millarow.Presentation.WPF.Markup
{
    [MarkupExtensionReturnType(typeof(object))]
    public class BoolVisibilityExtension : AbstractBindingExtension
    {
        public BoolVisibilityExtension()
        {
        }

        public BoolVisibilityExtension(PropertyPath path)
        {
            Path = path;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var binding = CreateBinding();

            binding.Converter = new Converters.BoolToVisibilityConverter();
            binding.Path = Path;

            return binding.ProvideValue(serviceProvider);
        }

        [ConstructorArgument("Path")]
        public PropertyPath Path { get; set; }
    }
}