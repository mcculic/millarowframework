using Millarow.Rest.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Millarow.Rest.Http.Defaults
{
    public sealed class DefaultHttpExecutionLogger : IHttpExecutionLogger, ICompositeComponent
    {
        private readonly HashSet<MimeType> _printableMediaTypes;
        private IRestLogger _logger;

        public DefaultHttpExecutionLogger()
        {
            _printableMediaTypes = new HashSet<MimeType>();
        }

        public void AddPrintableMediaType(MimeType mediaType)
        {
            mediaType.AssertNotNull(nameof(mediaType));

            _printableMediaTypes.Add(mediaType);
        }

        public void LogRequest(HttpRequestMessage request)
        {
            request.AssertNotNull(nameof(request));

            if (LogRequests)
            {
                var message = new StringBuilder();
                message.AppendLine("HTTP request");//TODO message
                message.AppendLine(FormatRequest(request));

                _logger.LogInfo(message.ToString());
            }
        }

        public void LogRequestException(HttpRequestMessage request, Exception exception)
        {
            request.AssertNotNull(nameof(request));
            exception.AssertNotNull(nameof(exception));

            if (LogRequestExceptions)
            {
                var message = new StringBuilder();
                message.AppendLine($"HTTP request error - '{exception.Message}'");
                message.AppendLine(FormatRequest(request));

                _logger.LogError(message.ToString(), exception);
            }
        }

        public void LogResponse(HttpResponseMessage response)
        {
            response.AssertNotNull(nameof(response));

            if (LogResponses)
            {
                var message = new StringBuilder();
                message.AppendLine("HTTP response"); //TODO message
                message.Append(FormatResponse(response));

                _logger.LogInfo(message.ToString());
            }
        }

        public void LogResponseException(HttpResponseMessage response, Exception exception)
        {
            response.AssertNotNull(nameof(response));
            exception.AssertNotNull(nameof(exception));

            if (LogResponseExceptions)
            {
                var message = new StringBuilder();
                message.AppendLine($"HTTP response error - '{exception.Message}'");
                message.AppendLine(FormatResponse(response));
                message.AppendLine(FormatRequest(response.RequestMessage));

                _logger.LogError(message.ToString(), exception);
            }
        }

        private string FormatRequest(HttpRequestMessage request)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"[Request] {request}");

            var content = GetContentLogString(request.Content);
            if (content != null)
                sb.AppendLine($"[RequestContent] {content}");

            return sb.ToString();
        }

        private string FormatResponse(HttpResponseMessage response)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"[Response] {response}");

            var content = GetContentLogString(response.Content);
            if (content != null)
                sb.AppendLine($"[ResponseContent] {content}");

            return sb.ToString();
        }

        private string GetContentLogString(HttpContent content)
        {
            if (content == null)
                return null;

            var contentMediaType = content.Headers.ContentType?.MediaType;
            if (contentMediaType != null && _printableMediaTypes.Any(x => x.Match(contentMediaType)))
                return Task.Run(content.ReadAsStringAsync).Result;

            return null;
        }

        public void ResolveDependencies(IRestContainer container)
        {
            _logger = container.Required<IRestLogger>();
        }

        public bool LogRequests { get; set; } = true;

        public bool LogRequestExceptions { get; set; } = true;

        public bool LogResponses { get; set; } = true;
        
        public bool LogResponseExceptions { get; set; }

        public IEnumerable<string> PrintableMediaTypes { get; set; }
    }
}
