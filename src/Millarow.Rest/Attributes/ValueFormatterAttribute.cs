using Millarow.Rest.Serialization;
using System;
using System.Globalization;

namespace Millarow.Rest.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = true)]
    public abstract class ValueFormatterAttribute : Attribute, IRestValueFormatter
    {
        public abstract bool CanSerialize(RestValue value);

        public abstract string Serialize(RestValue value, CultureInfo cultureInfo);
    }
}
