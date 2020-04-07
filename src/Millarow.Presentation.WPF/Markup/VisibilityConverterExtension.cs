using Millarow.Presentation.WPF.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Markup;

namespace Millarow.Presentation.WPF.Markup
{
    [MarkupExtensionReturnType(typeof(IValueConverter))]
    public class VisibilityConverterExtension : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Converters.TryGetValue(Mode, out var converter))
                return converter;

            throw new InvalidOperationException($"Unknown Mode property value '{Mode}'");
        }

        [DefaultValue(BoolToVisibilityConverterMode.Collapse)]
        public BoolToVisibilityConverterMode Mode { get; set; }

        private IReadOnlyDictionary<BoolToVisibilityConverterMode, BoolToVisibilityConverter> Converters = new Dictionary<BoolToVisibilityConverterMode, BoolToVisibilityConverter>
        {
            [BoolToVisibilityConverterMode.Collapse] = new BoolToVisibilityConverter(BoolToVisibilityConverterMode.Collapse),
            [BoolToVisibilityConverterMode.CollapseInverted] = new BoolToVisibilityConverter(BoolToVisibilityConverterMode.CollapseInverted),
            [BoolToVisibilityConverterMode.Hide] = new BoolToVisibilityConverter(BoolToVisibilityConverterMode.Hide),
            [BoolToVisibilityConverterMode.HideInverted] = new BoolToVisibilityConverter(BoolToVisibilityConverterMode.HideInverted)
        };
    }
}
