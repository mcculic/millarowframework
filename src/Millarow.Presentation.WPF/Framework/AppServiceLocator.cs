using System;

namespace Millarow.Presentation.WPF.Framework
{
    public static class AppServiceLocator
    {
        private static object _providerPropertyKey;

        public static void Initialize(IServiceProvider serviceProvider, object providerPropertyKey)
        {
            serviceProvider.AssertNotNull(nameof(serviceProvider));
            providerPropertyKey.AssertNotNull(nameof(providerPropertyKey));

            AppProperty.Set(providerPropertyKey, serviceProvider);
            _providerPropertyKey = providerPropertyKey;
        }

        public static void Initialize(IServiceProvider serviceProvider)
        {
            Initialize(serviceProvider, $"{nameof(AppServiceLocator)}_Provider");
        }

        public static object Resolve(Type valueType)
        {
            var provider = AppProperty.Get<IServiceProvider>(_providerPropertyKey);

            var value = provider.GetService(valueType);
            if (valueType.IsAssignableFrom(value.GetType()))
                return value;

            throw new InvalidCastException();
        }

        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }
    }
}