using Millarow.Rest.Core;
using Millarow.Rest.Metadata.Providers;
using Millarow.Rest.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Millarow.Rest.Metadata
{
    public class RestMetadataProvider : ICompositeComponent
    {
        public void ResolveDependencies(IRestContainer container)
        {
            DefaultCultureProvider = container.Required<IDefaultCultureProvider>();
            DefaultEncodingProvider = container.Required<IDefaultEncodingProvider>();
        }

        public EndpointMetadata GetEndpointMetadata(Type contractType, MetadataValueProvider provider)
        {
            contractType.AssertNotNull(nameof(contractType));
            provider.AssertNotNull(nameof(provider));

            var values = provider.Nested(TypeDescriptor.GetAttributes(contractType));
            var methods = contractType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            var valueFormatters = contractType.GetCustomAttributes<Attribute>().OfType<IRestValueFormatter>().ToArray();

            return new EndpointMetadata
            {
                Methods = methods.Select(x => GetMethodMetadata(x, values, valueFormatters)).ToArray()
            };
        }

        private MethodMetadata GetMethodMetadata(MethodInfo method, MetadataValueProvider provider, IEnumerable<IRestValueFormatter> valueFormatters)
        {
            method.AssertNotNull(nameof(method));
            provider.AssertNotNull(nameof(provider));

            var values = provider.Nested(method.GetCustomAttributes());
            if (!values.TryGetRequestMethod(out var requestMethod))
                throw new Exception("method not setted");//TODO msg

            var parameters = method.GetParameters().Where(x => !IsCancellationToken(x));
            var namePrefix = values.GetParameterPrefix();

            var methodValueFormatters = valueFormatters.Union(method.GetCustomAttributes<Attribute>().OfType<IRestValueFormatter>()).ToArray();

            return new MethodMetadata(method)
            {
                Request = new RequestMetadata(requestMethod)
                {
                    Route = values.GetRequestRoutePrefix() + values.GetRequestRoute(),
                    Timeout = values.GetRequestTimeout(),
                    ContentKind = values.GetRequestContentKind(),
                    Parameters = parameters.SelectMany(x => GetRequestParameters(x.Name, namePrefix, x.ParameterType, x.Name, methodValueFormatters, values.Nested(x.GetCustomAttributes()))).ToArray()
                },
                Response = new ResponseMetadata
                {
                    Parameters = GetResponseParameters(method.ReturnParameter, values).ToArray(),
                    IsAsync = typeof(Task).IsAssignableFrom(method.ReturnType),
                    ResultType = GetResultType(method.ReturnType)
                }
            };
        }

        private RequestParameterKind? GetParameterKind(Type parameterType, MetadataValueProvider provider)
        {
            if (provider.TryGetParameterKind(out var kind))
                return kind;

            if (typeof(RequestBinaryContent).IsAssignableFrom(parameterType))
                return RequestParameterKind.FormFile;

            if (typeof(RequestQuery).IsAssignableFrom(parameterType))
                return RequestParameterKind.Query;

            if (typeof(RequestHeader).IsAssignableFrom(parameterType))
                return RequestParameterKind.Header;

            if (typeof(RequestRouteSegment).IsAssignableFrom(parameterType))
                return RequestParameterKind.Route;

            return null;
        }

        private IEnumerable<RequestParameterMetadata> GetRequestParameters(string bindingSourceName, string namePrefix, Type parameterType, string memberName, IEnumerable<IRestValueFormatter> valueFormatters, MetadataValueProvider provider)
        {
            parameterType.AssertNotNull(nameof(parameterType));
            bindingSourceName.AssertNotNull(nameof(bindingSourceName));

            var parameterKind = GetParameterKind(parameterType, provider);
            if (parameterKind.HasValue)
            {
                var parameterName = $"{namePrefix}{provider.GetParameterName() ?? memberName}";
                var paramMetadata = GetRequestParameter(bindingSourceName, parameterName, parameterType, parameterKind.Value, provider);

                foreach (var formatter in valueFormatters)
                    paramMetadata.AddValueFormatter(formatter);

                yield return paramMetadata;
            }
            else if (TypeDescriptor.GetProperties(parameterType).OfType<PropertyDescriptor>().SelectMany(x => x.Attributes.OfType<IParameterDefinition>()).Any())
            {
                foreach (var property in TypeDescriptor.GetProperties(parameterType).OfType<PropertyDescriptor>())
                {
                    var npv = provider.Nested(property.Attributes).Nested(TypeDescriptor.GetAttributes(parameterType)); //TODO

                    var parameterName = $"{namePrefix}{npv.GetParameterName() ?? property.Name}";
                    var propBindingPath = npv.GetBindingPath().Append(property.Name);
                    var paramDef = property.Attributes.OfType<IParameterDefinition>().FirstOrDefault();
                    if (paramDef != null)
                    {
                        var paramMetadata = GetRequestParameter(bindingSourceName, parameterName, parameterType, paramDef.ParameterKind, npv);
                        paramMetadata.BindingPath = propBindingPath;

                        foreach (var formatter in valueFormatters)
                            paramMetadata.AddValueFormatter(formatter);

                        yield return paramMetadata;
                    }
                    else
                    {
                        var prefix = namePrefix + npv.GetParameterPrefix(); 
                        var pathProvider = new BindingPathProvider(propBindingPath);

                        foreach (var paramMetadata in GetRequestParameters(bindingSourceName, prefix, property.PropertyType, property.Name, valueFormatters, npv.With(pathProvider)))
                            yield return paramMetadata;
                    }
                }
            }
            else
            {
                var parameterName = $"{namePrefix}{provider.GetParameterName() ?? memberName}";
                var bodyParam = GetRequestParameter(bindingSourceName, parameterName, parameterType, RequestParameterKind.Body, provider);

                foreach (var formatter in valueFormatters)
                    bodyParam.AddValueFormatter(formatter);

                yield return bodyParam;
            }
        }
        
        private RequestParameterMetadata GetRequestParameter(string bindingSourceName, string parameterName, Type parameterType, RequestParameterKind parameterKind, MetadataValueProvider provider)
        {
            return new RequestParameterMetadata(bindingSourceName, parameterType, parameterKind)
            {
                ParameterName = parameterName,
                BindingPath = provider.GetBindingPath(),
                IsRequired = provider.GetIsRequired(),
                MediaType = provider.GetContentMediaType(),
                CharSet = provider.GetContentCharSet() ?? DefaultEncodingProvider.Encoding.WebName, //TODO
                FileName = provider.GetContentFileName(),
                Culture = DefaultCultureProvider.Culture,
                StringFormat = provider.GetStringFormat()
            };
        }

        private IEnumerable<ResponseParameterMetadata> GetResponseParameters(ParameterInfo parameter, MetadataValueProvider provider)
        {
            var values = provider.Nested(parameter.GetCustomAttributes(true));
            var resultType = GetResultType(parameter.ParameterType);
            if (resultType == typeof(void))
                yield break;

            if (resultType.IsAssignableFrom(typeof(IResponse)))
            {
                yield break;
            }
            else if (values.TryGetParameterKind(out var paramKind))
            {
                yield return GetResponseParameter(resultType, paramKind, values);
            }
            else
            {
                var restParams = GetResponseParameters(resultType, values);

                foreach (var param in restParams)
                    yield return param;
            }
        }

        private IEnumerable<ResponseParameterMetadata> GetResponseParameters(Type targetType, MetadataValueProvider provider)
        {
            targetType.AssertNotNull(nameof(targetType));
            provider.AssertNotNull(nameof(provider));

            if (TypeDescriptor.GetProperties(targetType).OfType<PropertyDescriptor>().SelectMany(x => x.Attributes.OfType<IParameterDefinition>()).Any())
            {
                foreach (var property in TypeDescriptor.GetProperties(targetType).OfType<PropertyDescriptor>())
                {
                    var paramDef = property.Attributes.OfType<IParameterDefinition>().FirstOrDefault();
                    if (paramDef == null)
                        continue;

                    var propertyValues = provider.Nested(property.Attributes);
                    var propertyMetadata = GetResponseParameter(property.PropertyType, paramDef.ParameterKind, propertyValues.Nested(TypeDescriptor.GetAttributes(targetType)));
                    propertyMetadata.ModelPropertyName = property.Name;
                    propertyMetadata.TypeConverter = property.Converter;

                    yield return propertyMetadata;
                }
            }
            else
            {
                var bodyParam = GetResponseParameter(targetType, RequestParameterKind.Body, provider);

                yield return bodyParam;
            }
        }

        private ResponseParameterMetadata GetResponseParameter(Type parameterType, RequestParameterKind parameterKind, MetadataValueProvider provider)
        {
            return new ResponseParameterMetadata(parameterType, parameterKind)
            {
                ParameterName = provider.GetParameterName(),
                IsRequired = provider.GetIsRequired(),
                FileName = provider.GetContentFileName(),
                Culture = DefaultCultureProvider.Culture
            };
        }

        private static Type GetResultType(Type returnType)
        {
            if (returnType == typeof(void) || returnType == typeof(Task))
                return typeof(void);

            if (returnType.IsGenericType)
            {
                if (returnType.GetGenericTypeDefinition() == typeof(Task<>))
                    return returnType.GetGenericArguments()[0];
            }

            return returnType;
        }

        private static bool IsCancellationToken(ParameterInfo parameter)
            => parameter.ParameterType == typeof(CancellationToken);

        protected IDefaultCultureProvider DefaultCultureProvider { get; private set; }

        protected IDefaultEncodingProvider DefaultEncodingProvider { get; private set; }

        private sealed class BindingPathProvider : IBindingPathProvider
        {
            public BindingPathProvider(BindingPath path) => BindingPath = path;

            public BindingPath BindingPath { get; }
        }

        private sealed class RestNamePrefixProvider : IRestNamePrefixProvider
        {
            public RestNamePrefixProvider(string namePrefix) => Prefix = namePrefix;

            public string Prefix { get; }
        }
    }
}