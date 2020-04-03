using Millarow.Rest.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Millarow.Rest.Serialization.Formatters
{
    public sealed class NullableValueFormatter : IRestValueFormatter, ICompositeComponent
    {
        public void ResolveDependencies(IRestContainer container)
        {
            ValueFormatters = container.Resolve<IRestValueFormatter>().ToArray();
        }

        public bool CanSerialize(RestValue value)
        {
            var type = Nullable.GetUnderlyingType(value.Type);
            if (type == null)
                return false;

            var typeValue = new RestValue(value.Value, type);

            return ValueFormatters.Any(x => x.CanSerialize(typeValue));
        }

        public string Serialize(RestValue value, CultureInfo cultureInfo)
        {
            var type = Nullable.GetUnderlyingType(value.Type);
            var typeValue = new RestValue(value.Value, type);
            var formatter = ValueFormatters.FirstOrDefault(x => x.CanSerialize(typeValue));
            if (formatter == null)
                throw new RestException(RestExceptionKind.Serialization, $"Value '{value.Type}' formatter not found"); //TODO msg

            return formatter.Serialize(typeValue, cultureInfo);
        }

        private IEnumerable<IRestValueFormatter> ValueFormatters { get; set; }
    }
}
