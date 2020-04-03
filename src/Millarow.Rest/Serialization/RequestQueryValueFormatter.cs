using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Millarow.Rest.Serialization
{
    public abstract class RequestQueryValueFormatter
    {
        public abstract bool CanSerialize(RestValue value);

        public abstract string Serialize(RestValue value, CultureInfo cultureInfo);
    }
}
