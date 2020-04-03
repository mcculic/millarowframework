using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Millarow.Presentation.WPF.Converters
{
    public class PropertyTitleConverter<T> : AbstractTypedConverter<string, string>
    {
        public override string Convert(string value, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var propertyInfo = ParsePath(value).LastOrDefault();
            if (propertyInfo == null)
                return value;

            var displayName = GetDisplayName(propertyInfo);
            if (parameter is string postfix)
                displayName += postfix;

            return FormatDisplayName(displayName);
        }

        protected virtual string FormatDisplayName(string value)
        {
            return value;
        }

        private static string GetDisplayName(PropertyInfo propertyInfo)
        {
            var nameAttribute = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
            if (nameAttribute != null)
                return nameAttribute.DisplayName;

            return propertyInfo.Name;
        }

        private static IEnumerable<PropertyInfo> ParsePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                yield break;

            var segments = path.Split('.');
            var type = typeof(T);

            foreach (var segment in segments)
            {
                var propertyInfo = type.GetProperty(segment, BindingFlags.Instance | BindingFlags.Public);
                if (propertyInfo == null)
                    break;

                yield return propertyInfo;

                type = propertyInfo.PropertyType;
            }
        }
    }
}
