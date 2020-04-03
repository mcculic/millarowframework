using Millarow.Rest.Core;
using Millarow.Rest.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Millarow.Rest.Metadata
{
    public class RequestParameterMetadata
    {
        private readonly List<IRestValueFormatter> _valueFormatters = new List<IRestValueFormatter>();

        public RequestParameterMetadata(string bindingSourceName, Type parameterType, RequestParameterKind parameterKind)
        {
            bindingSourceName.AssertNotNull(nameof(bindingSourceName));
            parameterType.AssertNotNull(nameof(parameterType));

            BindingSourceName = bindingSourceName;
            ParameterType = parameterType;
            ParameterKind = parameterKind;
        }

        public void AddValueFormatter(IRestValueFormatter formatter)
        {
            formatter.AssertNotNull(nameof(formatter));

            _valueFormatters.Add(formatter);
        }

        public string BindingSourceName { get; }

        public BindingPath BindingPath { get; set; }

        public RequestParameterKind ParameterKind { get; }

        public Type ParameterType { get; }

        public string ParameterName { get; set; }

        public MimeType MediaType { get; set; }

        public string CharSet { get; set; }
        
        public string FileName { get; set; }

        public CultureInfo Culture { get; set; }

        public string StringFormat { get; set; }

        public bool IsRequired { get; set; }

        public bool? EmitDefault { get; set; }

        public IEnumerable<IRestValueFormatter> ValueFormatters => _valueFormatters;
    }
}