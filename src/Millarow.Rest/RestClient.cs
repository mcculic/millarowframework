using Millarow.Rest.Core;
using Millarow.Rest.Http;
using Millarow.Rest.Metadata;
using Millarow.Rest.ModelBinding;
using Millarow.Rest.ModelBinding.Binders;
using Millarow.Rest.Proxy;
using Millarow.Rest.Serialization.Formatters;
using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo(Millarow.Rest.RestClient.ProxiesAssemblyName)]

namespace Millarow.Rest
{
    public class RestClient : IRestEndpoint
    {
        private readonly IRestContainer _container;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpExecutionStrategy _executionStrategy;
        private readonly IHttpRequestMapper _requestMapper;
        private readonly IHttpResponseMapper _httpResponseMapper;

        public RestClient(IRestContainer container)
        {
            container.AssertNotNull(nameof(container));

            _container = container;
            _clientFactory = _container.Required<IHttpClientFactory>();
            _executionStrategy = _container.Required<IHttpExecutionStrategy>();
            _httpResponseMapper = _container.Required<IHttpResponseMapper>();
            _requestMapper = _container.Required<IHttpRequestMapper>();
        }

        public TContract CreateProxy<TContract>(Action<EndpointProxyBuilder> proxyConfigurator)
            where TContract : class
        {
            var proxyType = ProxyFactory.GetProxyType<EndpointProxy, TContract>();
            var metadataProvider = _container.Required<RestMetadataProvider>();
            var valuesProvider = new MetadataValueProvider();
            var metadata = metadataProvider.GetEndpointMetadata(typeof(TContract), valuesProvider);

            var proxyBuilder = new RestContainerBuilder(_container);
            proxyBuilder.Register<DefaultRequestBinder>();
            proxyBuilder.Register<DefaultResponseBinder>();
            proxyBuilder.Register<DefaultRequestQueryValueFormatter>();
            proxyBuilder.Register<DefaultRequestRouteValueFormatter>();
            proxyBuilder.Register<RestModelMapper>();

            proxyConfigurator?.Invoke(new EndpointProxyBuilder(proxyBuilder));

            var proxyContainer = proxyBuilder.CreateContainer();
            var proxy = Activator.CreateInstance(proxyType, new object[] { this, metadata, proxyContainer });

            return (TContract)proxy;
        }

        public TApi CreateProxy<TApi>()
            where TApi : class
        {
            return CreateProxy<TApi>(null);
        }

        public async Task<IResponse> ExecuteAsync(IRestRequest request, CancellationToken cancellationToken)
        {
            request.AssertNotNull(nameof(request));

            _container.ExecuteFor<IRestRequestEnricher>(x => x.Enrich(request));
            _container.ExecuteFor<IRestRequestValidator>(x => x.ValidateRequest(request));
            //логирование IRequest должно быть тут, а логи http как раз в execstrategy

            using (var client = _clientFactory.CreateClient())
            {
                if (request.Timeout.HasValue)
                    client.Timeout = request.Timeout.Value;

                var context = new HttpExecutionContext
                {
                    Invoker = client,
                    CancellationToken = cancellationToken
                };

                try
                {
                    await _executionStrategy.ExecuteAsync(() => _requestMapper.MapRequest(request), context);

                    if (context.IsTimeout)
                        throw new RestException(RestExceptionKind.Timeout, context.ErrorMessage);

                    if (context.IsError)
                        throw new RestException(RestExceptionKind.Http, context.ErrorMessage);

                    var response = _httpResponseMapper.MapResponse(context.ResponseMessage, request);
                    _container.ExecuteFor<IRestResponseValidator>(x => x.ValidateResponse(response));

                    return response;
                }
                finally
                {
                    context.ResponseMessage?.Dispose();
                }
            }
        }

        protected IRestContainer Container => _container;

        private static readonly DynamicProxyFactory ProxyFactory = new DynamicProxyFactory(ProxiesAssemblyName);

        internal const string ProxiesAssemblyName = "Millarow.Rest.DynamicProxies";
    }
}
