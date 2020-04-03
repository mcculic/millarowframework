using Millarow.Rest.Core;
using Millarow.Rest.Http;
using Millarow.Rest.Http.Defaults;
using Millarow.Rest.Metadata;
using Millarow.Rest.Serialization;
using Millarow.Rest.Serialization.Formatters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;

namespace Millarow.Rest
{
    public class RestClientBuilder
    {
        private readonly RestContainerBuilder _container;
        private readonly RestLogger _logger = new RestLogger();
        private readonly DefaultHttpClientFactory _httpClientFactory = new DefaultHttpClientFactory();
        private readonly DefaultHttpExecutionLogger _httpExecutionLogger = new DefaultHttpExecutionLogger();
        private readonly DefaultHttpExecutionStrategy _httpExecutionStrategy = new DefaultHttpExecutionStrategy();
        private readonly DefaultCultureProvider _defaultCultureProvider = new DefaultCultureProvider();
        private readonly DefaultEncodingProvider _defaultContentEncodingProvider = new DefaultEncodingProvider();
        private readonly DefaultMediaTypeProvider _defaultMediaTypeProvider = new DefaultMediaTypeProvider();

        public RestClientBuilder(string baseAddress = null)
        {
            if (baseAddress != null)
            {
                var baseUrl = baseAddress;
                if (!baseUrl.EndsWith("/"))
                    baseUrl += "/";

                _httpClientFactory.Configure(client =>
                {
                    client.BaseAddress = new Uri(baseUrl, UriKind.Absolute);
                });
            }

            _container = new RestContainerBuilder();

            _container.Register(StringValueFormatter.Instance);
            _container.Register<NullableValueFormatter>();
            _container.Register<DefaultRequestQueryValueFormatter>();
            _container.Register<DefaultRequestRouteValueFormatter>();
            _container.Register<RestSerializer>();

            _container.Register<RestMetadataProvider>();
            _container.Register<HttpRequestContentMapper>();
            _container.Register<DefaultHttpRequestContentFormatter>();
            _container.Register<RestRouteRenderer>();
            _container.Register<DefaultHttpRequestMapper>();
            _container.Register<DefaultHttpResponseMapper>();
            
            _container.Register(_logger);
            _container.Register(_httpClientFactory);
            _container.Register(_httpExecutionLogger);
            _container.Register(_httpExecutionStrategy);
            _container.Register(_defaultCultureProvider);
            _container.Register(_defaultContentEncodingProvider);
            _container.Register(_defaultMediaTypeProvider);
        }

        public RestClient CreateClient()
        {
            return new RestClient(_container.CreateContainer());
        }

        public RestClientBuilder With(Action<IRestContainerBuilder> configure)
        {
            configure.AssertNotNull(nameof(configure));

            configure(_container);

            return this;
        }

        public RestClientBuilder Timeout(TimeSpan value)
        {
            _httpClientFactory.Configure(client => client.Timeout = value);

            return this;
        }

        public RestClientBuilder MaxResponseContentBufferSize(long value)
        {
            _httpClientFactory.Configure(client => client.MaxResponseContentBufferSize = value);

            return this;
        }

        public RestClientBuilder ExecutionStrategy(Action<HttpResponseMessage> responseValidator, int maxRetryCount = 3, TimeSpan postErrorDelay = default(TimeSpan))
        {
            responseValidator.AssertNotNull(nameof(responseValidator));

            _httpExecutionStrategy.ResponseValidator = responseValidator;
            _httpExecutionStrategy.MaxRetryCount = maxRetryCount;
            _httpExecutionStrategy.PostErrorDelay = postErrorDelay;

            return this;
        }

        public RestClientBuilder DefaultCulture(CultureInfo cultureInfo)
        {
            cultureInfo.AssertNotNull(nameof(cultureInfo));

            _defaultCultureProvider.Culture = cultureInfo;

            return this;
        }

        public RestClientBuilder DefaultEncoding(Encoding encoding)
        {
            encoding.AssertNotNull(nameof(encoding));

            _defaultContentEncodingProvider.Encoding = encoding;

            return this;
        }

        public RestClientBuilder DefaultRequestMediaType(MimeType mediaType)
        {
            mediaType.AssertNotNull(nameof(mediaType));

            _defaultMediaTypeProvider.MediaType = mediaType;

            return this;
        }

        public RestClientBuilder WithRequestEnricher(Action<IRestRequest> enricher)
        {
            enricher.AssertNotNull(nameof(enricher));

            _container.Register(new RequestEnricher(enricher));

            return this;
        }

        public RestClientBuilder WithDefaultRequestHeader<T>(string name, T value)
        {
            name.AssertNotNullOrEmpty(nameof(name));

            var header = new RequestHeader(name, RestValue.Create(value));
            _container.Register(new RequestEnricher(x => x.AddHeader(header)));

            return this;
        }

        public RestClientBuilder WithDefaultRequestQuery<T>(string name, T value)
        {
            name.AssertNotNullOrEmpty(nameof(name));

            var query = new RequestQuery(name, RestValue.Create(value));
            _container.Register(new RequestEnricher(x => x.AddQuery(query)));

            return this;
        }

        public RestClientBuilder WithDefaultRequestSegment<T>(string name, T value)
        {
            name.AssertNotNullOrEmpty(nameof(name));

            var segment = new RequestRouteSegment(name, RestValue.Create(value));
            _container.Register(new RequestEnricher(x => x.AddRouteSegment(segment)));

            return this;
        }

        public RestClientBuilder WithBearerAuthorization(string token)
        {
            return WithDefaultRequestHeader("Authorization", $"Bearer {token}");
        }

        public RestClientBuilder WithBasicAuthorization(string userName, string password)
        {
            var bytes = Encoding.ASCII.GetBytes($"{userName}:{password}");

            return WithDefaultRequestHeader("Authorization", $"Basic {Convert.ToBase64String(bytes)}");
        }

        public RestClientBuilder UseRequestValidator(Action<IRestRequest> validateAction)
        {
            _container.Register(new RequestValidator(validateAction));
            return this;
        }

        public RestClientBuilder UseResponseValidator(Action<IResponse> validateAction)
        {
            _container.Register(new ResponseValidator(validateAction));
            return this;
        }

        public RestClientBuilder UseLogger(Action<string> logInfo = null, Action<string, Exception> logError = null)
        {
            if (logInfo != null)
                _logger.LogInfoActions.Add(logInfo);

            if (logError != null)
                _logger.LogErrorActions.Add(logError);

            return this;
        }

        public RestClientBuilder WithLogMediaType(string mediaType)
        {
            mediaType.AssertNotNull(nameof(mediaType));

            _httpExecutionLogger.AddPrintableMediaType(mediaType);

            return this;
        }

        public RestSerializationBuilder<RestClientBuilder> Serialization => new RestSerializationBuilder<RestClientBuilder>(_container, this);

        public static implicit operator RestClient(RestClientBuilder builder) => builder.CreateClient();

        private sealed class DefaultCultureProvider : IDefaultCultureProvider
        {
            public CultureInfo Culture { get; set; } = CultureInfo.InvariantCulture;
        }

        private sealed class DefaultEncodingProvider : IDefaultEncodingProvider
        {
            public Encoding Encoding { get; set; } = Encoding.UTF8;
        }

        private sealed class DefaultMediaTypeProvider : IDefaultMediaTypeProvider
        {
            public MimeType MediaType { get; set; }
        }

        private sealed class RestLogger : IRestLogger
        {
            public void LogInfo(string message)
            {
                foreach (var action in LogInfoActions)
                    action(message);
            }

            public void LogError(string message, Exception exception) 
            {
                foreach (var action in LogErrorActions)
                    action(message, exception);
            }

            public List<Action<string>> LogInfoActions { get; } = new List<Action<string>>();

            public List<Action<string, Exception>> LogErrorActions { get; } = new List<Action<string, Exception>>();
        }

        private sealed class RequestValidator : IRestRequestValidator
        {
            public RequestValidator(Action<IRestRequest> action) => Action = action;

            public void ValidateRequest(IRestRequest request) => Action(request);

            private Action<IRestRequest> Action { get; }
        }

        private sealed class ResponseValidator : IRestResponseValidator
        {
            public ResponseValidator(Action<IResponse> action) => Action = action;

            public void ValidateResponse(IResponse response) => Action(response);

            private Action<IResponse> Action { get; }
        }

        private sealed class RequestEnricher : IRestRequestEnricher
        {
            public RequestEnricher(Action<IRestRequest> action) => Action = action;

            public void Enrich(IRestRequest request) => Action(request);

            private Action<IRestRequest> Action { get; }
        }
    }
}
