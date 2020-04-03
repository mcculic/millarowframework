using Millarow.Presentation.WPF.Framework;
using System;
using System.Windows.Markup;

namespace Millarow.Presentation.WPF.Markup
{
    [MarkupExtensionReturnType(typeof(object))]
    public class AppPropertyExtension : MarkupExtension
    {
        public AppPropertyExtension()
        {
        }

        public AppPropertyExtension(object key)
        {
            Key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Key == null)
                return null;

            if (AppProperty.TryGet(Key, out var value))
                return value;

            return null;
        }

        [ConstructorArgument("key")]
        public object Key { get; set; }
    }
}