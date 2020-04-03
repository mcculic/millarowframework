using Millarow.Rest.Core;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Millarow.Rest.Serialization.Formatters
{
    public class DefaultRequestQueryValueFormatter : RequestQueryValueFormatter, ICompositeComponent
    {
        public virtual void ResolveDependencies(IRestContainer container)
        {
            ValueFormatters = container.Resolve<IRestValueFormatter>().ToArray();
        }

        public override bool CanSerialize(RestValue value)
        {
            return ValueFormatters.Any(x => x.CanSerialize(value));
        }

        public override string Serialize(RestValue value, CultureInfo cultureInfo)
        {
            var formatter = ValueFormatters.FirstOrDefault(x => x.CanSerialize(value));
            if (formatter == null)
                throw new RestException(RestExceptionKind.Serialization, $"Value '{value.Type}' formatter not found"); //TODO msg

            return formatter.Serialize(value, cultureInfo);
        }

        protected IEnumerable<IRestValueFormatter> ValueFormatters { get; private set; }
    }
}
