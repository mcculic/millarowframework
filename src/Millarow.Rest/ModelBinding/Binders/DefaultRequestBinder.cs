using Millarow.Rest.Core;
using Millarow.Rest.Metadata;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Millarow.Rest.ModelBinding.Binders
{
    public class DefaultRequestBinder : IRequestBinder, ICompositeComponent
    {
        public void ResolveDependencies(IRestContainer container)
        {
            DefaultEncodingProvider = container.Required<IDefaultEncodingProvider>();
            DefaultMediaTypeProvider = container.Required<IDefaultMediaTypeProvider>();
        }

        public virtual void Bind(IRequestBindingContext bindingContext)
        {
            var bindingSource = bindingContext.BindingSource;

            foreach (var p in bindingContext.Metadata.Parameters)
            {
                if (bindingSource.TryGetValue(p.BindingSourceName, p.BindingPath, out var sourceValue))
                {
                    BindParameter(bindingContext, p, sourceValue);
                }
                else if (p.IsRequired)
                {
                    throw new InvalidOperationException($"{p.BindingPath} property value is missed. TODO mg");
                }
            }
        }

        protected virtual void BindParameter(IRequestBindingContext bindingContext, RequestParameterMetadata param, RestValue sourceValue)
        {
            if (param.ParameterKind == RequestParameterKind.Route)
            {
                var segments = Bind(param, sourceValue, (n, v) => new RequestRouteSegment(n, v));
                foreach (var segment in segments)
                    bindingContext.AddRouteSegment(segment);
            }
            else if (param.ParameterKind == RequestParameterKind.Header)
            {
                var headers = Bind(param, sourceValue, (n, v) => new RequestHeader(n, v));
                foreach (var header in headers)
                    bindingContext.AddHeader(header);
            }
            else if (param.ParameterKind == RequestParameterKind.Query)
            {
                var queries = Bind(param, sourceValue, (n, v) => new RequestQuery(n, v));

                if (param.StringFormat != null) //TODO костыль
                {
                    foreach (var query in queries.Where(x => !x.Content.IsNull)) //TODO filter at source level
                    {
                        var value = (IFormattable)query.Content.Value;
                        var formattedValue = value.ToString(param.StringFormat, bindingContext.CultureInfo);

                        bindingContext.AddQuery(new RequestQuery(query.Name, RestValue.Create(formattedValue)));
                    }
                }
                else
                {
                    foreach (var query in queries.Where(x => !x.Content.IsNull)) //TODO filter at source level
                        bindingContext.AddQuery(query);
                }
            }
            else if (param.ParameterKind.In(RequestParameterKind.Body, RequestParameterKind.FormField, RequestParameterKind.FormFile))
            {
                if (bindingContext.BindingSource.TryGetValue(param.BindingSourceName, param.BindingPath, out var contentValue))
                {
                    if (param.ParameterKind == RequestParameterKind.Body)
                    {
                        var contentMediaType = param.MediaType ?? MimeTypes.Application.Json;//TODO
                        var contentType = new ContentType(contentMediaType, DefaultEncodingProvider.Encoding.WebName);//TODO charset

                        bindingContext.Body = new RequestBody(contentValue, contentType);
                    }
                    else if (param.ParameterKind == RequestParameterKind.FormField)
                    {
                        var field = new RequestFormField(param.ParameterName, contentValue, param.MediaType ?? DefaultMediaTypeProvider.MediaType);

                        bindingContext.AddFormField(field);
                    }
                    else if (param.ParameterKind == RequestParameterKind.FormFile)
                    {
                        throw new NotImplementedException();
                        //var file = new RequestFormFile(contentValue, param.ParameterName)
                        //{
                        //    FileName = param.FileName,
                        //};

                        //bindingContext.AddFormField(field);
                    }
                    else
                        throw new NotImplementedException();
                }
                else if (param.IsRequired)
                {
                    throw new RestException(RestExceptionKind.Mapping, "req param missed");//TODO msg
                }
            }
            else
                throw new NotImplementedException(param.ParameterKind.ToString()); //TODO msg

            //TODO mark parameter as valid
        }

        private static IEnumerable<T> Bind<T>(RequestParameterMetadata parameter, RestValue source, Func<string, RestValue, T> factory)
        {
            var name = parameter.ParameterName;
            if (source.Value == null)
                yield break;

            if (source.Value is T t)
            {
                if (parameter.EmitDefault.GetValueOrDefault() || !Equals(t, default(T)))
                    yield return t;
            }
            else if (source.Value is IEnumerable<T> tList)
            {
                foreach (var value in tList)
                {
                    if (parameter.EmitDefault.GetValueOrDefault() || !Equals(value, default(T)))
                        yield return value;
                }
            }
            else if (source.Value is KeyValuePair<string, string> pair)
            {
                //TODO check for not null name
                yield return factory(name ?? pair.Key, RestValue.Create(pair.Value));
            }
            else if (source.Value is IEnumerable<KeyValuePair<string, string>> pairs)
            {
                var result = pairs.Select(x => factory(name ?? pair.Key, RestValue.Create(x.Value)));
                foreach (var value in result)
                    yield return value;
            }
            else if (source.Value is NameValueCollection nmcol)
            {
                var result = nmcol.Keys.OfType<string>().SelectMany(n => nmcol.GetValues(n).Select(v => factory(n, RestValue.Create(v))));
                foreach (var value in result)
                    yield return value;
            }
            else if (source.Value is string str)
            {
                if (parameter.EmitDefault.GetValueOrDefault() || str != null)
                    yield return factory(name, RestValue.Create(str));
            }
            else if (source.Value is IEnumerable<string> strList)
            {
                foreach (var p in strList.Select(x => factory(name, RestValue.Create(x))))
                    yield return p;
            }
            else if (source.Type.IsArray)
            {
                var elementType = source.Type.GetElementType();

                foreach (var el in (Array)source.Value)
                    yield return factory(name, new RestValue(el, elementType));
            }
            else
            {
                //var value = source.Value == null ? null : ConvertToString(parameter, source.Value);
                //if (parameter.EmitDefault.GetValueOrDefault() || value != null)
                yield return factory(name, source);
            }
        }

        public IDefaultEncodingProvider DefaultEncodingProvider { get; private set; }

        public IDefaultMediaTypeProvider DefaultMediaTypeProvider { get; private set; }
    }
}