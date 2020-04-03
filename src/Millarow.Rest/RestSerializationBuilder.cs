using Millarow.Rest.Core;
using Millarow.Rest.Serialization;
using Millarow.Rest.Serialization.Formatters;
using System;
using System.Globalization;

namespace Millarow.Rest
{
    public class RestSerializationBuilder<TParent>
        where TParent : class
    {
        private readonly IRestContainerBuilder _container;
        private readonly TParent _parent;

        internal RestSerializationBuilder(IRestContainerBuilder container, TParent parent)
        {
            _container = container;
            _parent = parent;
        }

        public TParent With<T>(T component)
            where T : class
        {
            component.AssertNotNull(nameof(component));

            _container.Register(component);

            return _parent;
        }

        public TParent UseValueFormatter(IRestValueFormatter formatter)
        {
            formatter.AssertNotNull(nameof(formatter));

            return With(formatter);
        }

        public TParent UseValueFormatter<T>(Func<T, string> formatter)
        {
            formatter.AssertNotNull(nameof(formatter));

            return UseValueFormatter(new LambdaValueFormatter<T>(formatter));
        }

        public TParent UseToStringValueFormatter()
        {
            return UseValueFormatter(ToStringValueFormatter.Instance);
        }

        public TParent UseFormattableValueFormater<T>(string format)
            where T : IFormattable
        {
            return UseValueFormatter(new FormattableValueFormatter<T>(format));
        }

        public TParent UseQueryValueFormatter(RequestQueryValueFormatter formatter)
        {
            return With(formatter);
        }

        public TParent UseQueryValueFormatter<TValue>(Func<TValue, string> formatter)
        {
            return UseQueryValueFormatter(new LambdaQueryValueFormatter<TValue>(formatter));
        }

        public TParent UseRouteValueFormatter(RequestRouteValueFormatter formatter)
        {
            return With(formatter);
        }

        public TParent UseRouteValueFormatter<TValue>(Func<TValue, string> formatter)
        {
            return UseRouteValueFormatter(new LambdaRouteValueFormatter<TValue>(formatter));
        }

        public TParent UseContentFormatter(IRestContentFormatter formatter)
        {
            formatter.AssertNotNull(nameof(formatter));
            
            return With(formatter);
        }

        public TParent UseStringContentFormatter(params MimeType[] mediaTypes)
        {
            return UseContentFormatter(new StringContentFormatter(mediaTypes));
        }

        public TParent UseByteArrayContentFormatter(params MimeType[] mediaTypes)
        {
            return UseContentFormatter(new ByteArrayContentFormatter(mediaTypes));
        }

        private sealed class LambdaRouteValueFormatter<TValue> : RequestRouteValueFormatter
        {
            private readonly Func<TValue, string> _formatter;

            public LambdaRouteValueFormatter(Func<TValue, string> formatter)
                => _formatter = formatter;

            public override bool CanSerialize(RestValue value)
                => value.Type == typeof(TValue);

            public override string Serialize(RestValue value, CultureInfo cultureInfo)
                => _formatter((TValue)value.Value);
        }

        private sealed class LambdaQueryValueFormatter<TValue> : RequestQueryValueFormatter
        {
            private readonly Func<TValue, string> _formatter;

            public LambdaQueryValueFormatter(Func<TValue, string> formatter)
                => _formatter = formatter;

            public override bool CanSerialize(RestValue value)
                => value.Type == typeof(TValue);

            public override string Serialize(RestValue value, CultureInfo cultureInfo)
                => _formatter((TValue)value.Value);
        }

        private class LambdaValueFormatter<TValue> : IRestValueFormatter
        {
            private readonly Func<TValue, string> _formatter;

            public LambdaValueFormatter(Func<TValue, string> formatter)
                => _formatter = formatter;

            public bool CanSerialize(RestValue value)
                => value.Type == typeof(TValue);

            public string Serialize(RestValue value, CultureInfo cultureInfo)
            {
                //TODO checks
                return _formatter((TValue)value.Value);
            }
        }
    }
}
