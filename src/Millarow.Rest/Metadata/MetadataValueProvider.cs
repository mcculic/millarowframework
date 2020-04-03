using Millarow.Rest.Metadata.Providers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Millarow.Rest.Metadata
{
    public class MetadataValueProvider
    {
        private readonly IEnumerable<IMetadataProvider> _providers;

        public MetadataValueProvider(IEnumerable<IMetadataProvider> providers)
        {
            providers.AssertNotNull(nameof(providers));

            _providers = providers;
        }

        public MetadataValueProvider(IEnumerable providers)
        {
            providers.AssertNotNull(nameof(providers));

            _providers = providers.OfType<IMetadataProvider>();
        }

        public MetadataValueProvider()
            : this(Enumerable.Empty<IMetadataProvider>())
        {
        }

        public MetadataValueProvider Nested(IEnumerable<IMetadataProvider> children)
        {
            children.AssertNotNull(nameof(children));

            return new MetadataValueProvider(children.Concat(_providers.Where(x => x is IMetadataProviderHierarchy)));
        }

        public MetadataValueProvider With(IMetadataProvider provider)
        {
            provider.AssertNotNull(nameof(provider));

            return Nested(new[] { provider });
        }

        public MetadataValueProvider Nested(IEnumerable children, params IMetadataProvider[] providers)
        {
            children.AssertNotNull(nameof(children));

            return new MetadataValueProvider(children.OfType<IMetadataProvider>().Concat(_providers).Concat(providers));
        }

        public bool TryGetValue<TProvider, TValue>(Func<TProvider, Maybe<TValue>> selector, out TValue result)
            where TProvider : IMetadataProvider
        {
            foreach (var provider in _providers.OfType<TProvider>())
            {
                var value = selector(provider);
                if (value.HasValue)
                {
                    result = value.Value;
                    return true;
                }
            }

            result = default(TValue);
            return false;
        }

        public TValue GetValue<TProvider, TValue>(Func<TProvider, TValue> selector)
            where TProvider : IMetadataProvider
            where TValue : class
        {
            var provider = _providers.OfType<TProvider>().FirstOrDefault();
            if (provider == null)
                return null;

            return selector(provider);
        }

        public TValue GetValue<TProvider, TValue>(Func<TProvider, Maybe<TValue>> selector)
            where TProvider : IMetadataProvider
            where TValue : class
        {
            return TryGetValue(selector, out var result) ? result : null;
        }

        public TValue GetValue<TProvider, TValue>(Func<TProvider, Maybe<TValue>> selector, TValue defaultValue)
            where TProvider : IMetadataProvider
            where TValue : struct
        {
            return TryGetValue(selector, out var result) ? result : defaultValue;
        }

        public TValue? GetNullable<TProvider, TValue>(Func<TProvider, Maybe<TValue>> selector)
            where TProvider : IMetadataProvider
            where TValue : struct
        {
            return TryGetValue(selector, out var result) ? result : default(TValue?);
        }

        public static readonly MetadataValueProvider Empty = new MetadataValueProvider(Array.Empty<IMetadataProvider>());
    }
}
