using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Markup;

namespace Millarow.Presentation.WPF.Markup
{
    [MarkupExtensionReturnType(typeof(object))]
    public class PropertyTitleExtension : MarkupExtension
    {
        public PropertyTitleExtension()
        {
        }

        public PropertyTitleExtension(string propertyName)
        {
            PropertyName = propertyName;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (PropertyName == null)
                return null;

            if (Type != null)
            {
                var propertyInfo = Type.GetProperty(PropertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                if (propertyInfo != null)
                {
                    var nameAttribute = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
                    if (nameAttribute != null)
                        return nameAttribute.DisplayName + Postfix;
                }
            }

            return PropertyName + Postfix;
        }

        [ConstructorArgument("propertyName")]
        public string PropertyName { get; set; }

        public Type Type { get; set; }

        public string Postfix { get; set; }
    }
}