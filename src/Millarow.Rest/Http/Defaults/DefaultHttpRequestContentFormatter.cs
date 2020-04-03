using Millarow.Rest.Core;
using Millarow.Rest.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Millarow.Rest.Http
{
    public class DefaultHttpRequestContentFormatter : IHttpRequestContentFormatter, ICompositeComponent
    {
        public virtual bool CanMapContent(RequestContent content)
        {
            return true; //TODO
        }

        public virtual HttpContent MapContent(RequestContent content)
        {
            if (content is RequestBinaryContent binaryContent)
                return CreateContent(binaryContent);

            if (content is RequestMultipartContent multipartContent)
                return CreateContent(multipartContent);

            if (content is RequestUrlEncodedContent urlEncodedContent)
                return CreateContent(urlEncodedContent);

            if (content is RequestBody body)
                return CreateContent(body);

            throw new Exception();
        }

        protected virtual HttpContent CreateContent(RequestBody content)
        {
            var contentType = content.ContentType;
            var contentData = RestSerializer.SerializeContent(contentType, content.Content);

            var result = new ByteArrayContent(contentData);

            result.Headers.ContentType = new MediaTypeHeaderValue(contentType.MediaType)
            {
                CharSet = contentType.CharSet
            };

            return WithHeaders(WithContentType(result, contentType), content.Headers, CultureInfo);
        }

        protected virtual HttpContent CreateContent(RequestBinaryContent content)
        {
            var contentStream = content.ReadAsStream();
            var result = new StreamContent(contentStream);
            var contentType = content.ContentType;

            result.Headers.ContentType = new MediaTypeHeaderValue(contentType.MediaType)
            {
                CharSet = contentType.CharSet
            };

            return WithHeaders(WithContentType(result, contentType), content.Headers, CultureInfo);
        }

        protected virtual HttpContent CreateContent(RequestMultipartContent content)
        {
            var result = string.IsNullOrEmpty(content.Boundary) ? new MultipartFormDataContent() : new MultipartFormDataContent(content.Boundary);
            var contentType = content.ContentType;

            foreach (var form in content.Parts)
            {
                var formResult = MapContent(form.Content);
                var formName = form.Name ?? string.Empty;
                var formFileName = form.FileName ?? string.Empty;

                result.Add(formResult, formName, formFileName);
            }

            return WithHeaders(WithContentType(result, contentType), content.Headers, CultureInfo);
        }

        protected CultureInfo CultureInfo => CultureInfo.CurrentCulture;//TODO!!

        protected virtual HttpContent CreateContent(RequestUrlEncodedContent content)
        {
            var values = new List<KeyValuePair<string, string>>();
            var charSet = content.ContentType.CharSet;

            foreach (var field in content.Fields)
            {
                var fieldContentType = new ContentType(field.MediaType, charSet);
                var fieldData = RestSerializer.SerializeContent(fieldContentType, field.Content);
                var fieldValue = Encoding.GetEncoding(charSet).GetString(fieldData);

                values.Add(new KeyValuePair<string, string>(field.Name, fieldValue));
            }

            var result = new FormUrlEncodedContent(values);

            return WithHeaders(WithContentType(result, content.ContentType), content.Headers, CultureInfo);
        }

        protected virtual HttpContent WithContentType(HttpContent content, ContentType contentType)
        {
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType.MediaType)
            {
                CharSet = contentType.CharSet
            };

            return content;
        }

        protected virtual HttpContent WithHeaders(HttpContent content, IEnumerable<RequestHeader> headers, CultureInfo cultureInfo)
        {
            foreach (var header in headers)
            {
                var value = RestSerializer.SerializeQueryValue(header.Content, cultureInfo);

                content.Headers.Add(header.Name, value);
            }

            return content;
        }

        public void ResolveDependencies(IRestContainer container)
        {
            RestSerializer = container.Required<RestSerializer>();
        }

        protected RestSerializer RestSerializer { get; private set; }
    }
}
