using System;
using System.ComponentModel;
using System.Globalization;

namespace Millarow.Rest.Metadata
{
    public class ResponseParameterMetadata
    {
        public ResponseParameterMetadata(Type parameterType, RequestParameterKind parameterKind)
        {
            parameterType.AssertNotNull(nameof(parameterType));

            ParameterType = parameterType;
            ParameterKind = parameterKind;
        }

        public RequestParameterKind ParameterKind { get; }

        public Type ParameterType { get; }

        public string ModelPropertyName { get; set; }

        public string ParameterName { get; set; }

        public string FileName { get; set; }

        public CultureInfo Culture { get; set; }

        public bool IsRequired { get; set; }

        public TypeConverter TypeConverter { get; set; }
    }
}
