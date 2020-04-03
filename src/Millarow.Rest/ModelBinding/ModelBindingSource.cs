using Millarow.Rest.ModelBinding.Binders;
using System;
using System.Collections.Generic;

namespace Millarow.Rest.Core
{
    public class ModelBindingSource : IBindingData
    {
        private Dictionary<string, RestValue> _values;

        public void AddValue(string sourceName, RestValue value)
        {
            sourceName.AssertNotNull(nameof(sourceName));
            value.AssertNotNull(nameof(value));

            if (_values == null)
                _values = new Dictionary<string, RestValue>();

            _values.Add(sourceName, value);
        }

        public bool TryGetValue(string sourceName, BindingPath propertyPath, out RestValue value)
        {
            sourceName.AssertNotNull(nameof(sourceName));

            if (_values != null && _values.TryGetValue(sourceName, out var entry))
            {
                if (propertyPath.IsEmpty)
                {
                    value = entry;
                    return true;
                }

                var propertyValue = GetPropertyValue(entry, propertyPath, 0);
                if (propertyValue.HasValue)
                {
                    value = propertyValue.Value;
                    return true;
                }
            }

            value = null;
            return false;
        }

        private Maybe<RestValue> GetPropertyValue(RestValue sourceValue, BindingPath bindingPath, int pathIndex)
        {
            if (sourceValue.IsNull)
                return Maybe.Nothing<RestValue>();

            var propertyName = bindingPath[pathIndex];
            var propertyInfo = sourceValue.Type.GetProperty(propertyName);
            if (propertyInfo == null)
                throw new ArgumentException($"Invalid property name '{propertyName}'", nameof(bindingPath));

            var value = propertyInfo.GetValue(sourceValue.Value, null);
            var modelValue = new RestValue(value, propertyInfo.PropertyType);

            if (pathIndex + 1 == bindingPath.Length)
                return modelValue;

            return GetPropertyValue(modelValue, bindingPath, pathIndex + 1);
        }
    }
}