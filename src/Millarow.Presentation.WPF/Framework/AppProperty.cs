using System;
using System.Windows;

namespace Millarow.Presentation.WPF.Framework
{
    public static class AppProperty
    {
        public static bool Has(object propertyKey)
        {
            return GetCurrentApplication().Properties.Contains(propertyKey);
        }

        public static bool TryGet(object propertyKey, out object value)
        {
            var app = GetCurrentApplication();
            if (!app.Properties.Contains(propertyKey))
            {
                value = null;
                return false;
            }

            value = app.Properties[propertyKey];
            return true;
        }

        public static object Get(object propertyKey)
        {
            var app = GetCurrentApplication();
            if (!app.Properties.Contains(propertyKey))
                throw new InvalidOperationException($"Property '{propertyKey}' not found");

            return app.Properties[propertyKey];
        }

        public static T Get<T>(object propertyKey)
            where T : class
        {
            var value = Get(propertyKey);
            var valueType = value.GetType();
            if (typeof(T).IsAssignableFrom(valueType))
                return (T)value;

            throw new InvalidOperationException($"Property '{propertyKey}' has invalid type '{valueType}', expected: '{typeof(T)}'");
        }

        public static void Set(object propertyKey, object value)
        {
            GetCurrentApplication().Properties[propertyKey] = value;
        }

        private static Application GetCurrentApplication()
        {
            var app = Application.Current;
            if (app == null)
                throw new InvalidOperationException("Application is null");

            return app;
        }
    }
}
