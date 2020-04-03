using Millarow.Rest.Core;
using Millarow.Rest.Metadata;
using Millarow.Rest.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Millarow.Rest.Proxy
{
    internal class EndpointProxy : IDynamicProxy
    {
        private readonly IRestEndpoint _endpoint;
        private readonly EndpointMetadata _metadata;
        private readonly RestModelMapper _modelMapper;
        private readonly List<IInvocatioтFilter> _resultValidators;

        public EndpointProxy(IRestEndpoint endpoint, EndpointMetadata metadata, IRestContainer components)
        {
            endpoint.AssertNotNull(nameof(endpoint));
            metadata.AssertNotNull(nameof(metadata));
            components.AssertNotNull(nameof(components));

            _endpoint = endpoint;
            _metadata = metadata;
            _modelMapper = components.Required<RestModelMapper>();
            _resultValidators = components.Resolve<IInvocatioтFilter>().ToList();
        }

        public object Invoke(string methodSignature, object[] args)
        {
            var metadata = _metadata.Methods.First(x => x.MethodInfo.ToString() == methodSignature);
            if (metadata == null)
                throw new ArgumentException(nameof(methodSignature));
            
            var cancellationToken = GetCancellationToken(metadata.MethodInfo, args);
            var request = GetRequest(metadata, args);
            var response = Task.Run(() => _endpoint.ExecuteAsync(request, cancellationToken)).Result;
            
            if (metadata.Response.IsAsync)
            {
                if (metadata.Response.ResultType.IsAssignableFrom(typeof(IResponse)))
                {
                    _resultValidators.ForEach(x => x.OnExecuted(new InvocationResult(args, response, metadata)));

                    return ContinueAs(metadata.Response.ResultType, () => Task.FromResult<object>(response));
                }
                else if (metadata.Response.Parameters.Any())
                {
                    var result = GetResult(metadata, response);
                    _resultValidators.ForEach(x => x.OnExecuted(new InvocationResult(args, result, metadata)));

                    return ContinueAs(metadata.Response.ResultType, () => Task.FromResult(result));
                }
                else
                {
                    return Task.CompletedTask;
                }
            }
            else
            {
                if (metadata.Response.Parameters.Any())
                {
                    var result = GetResult(metadata, response);
                    _resultValidators.ForEach(x => x.OnExecuted(new InvocationResult(args, result, metadata)));

                    return result;
                }
                else
                {
                    return null;
                }
            }
        }

        private IRestRequest GetRequest(MethodMetadata methodMetadata, object[] args)
        {
            var bindingSource = new ModelBindingSource();

            foreach (var parameter in methodMetadata.MethodInfo.GetParameters())
            {
                var parameterValue = new RestValue(args[parameter.Position], parameter.ParameterType);

                bindingSource.AddValue(parameter.Name, parameterValue);
            }

            return _modelMapper.MapRequest(methodMetadata.Request, bindingSource);
        }

        private object GetResult(MethodMetadata methodMetadata, IResponse response)
        {
            var responseMetadata = methodMetadata.Response;
            var responseType = response.GetType();
            if (responseMetadata.ResultType.IsAssignableFrom(responseType))
                return response;

            return _modelMapper.MapResponse(methodMetadata.Response, response);
        }

        private static CancellationToken GetCancellationToken(MethodInfo methodInfo, object[] args)
        {
            var tokenParameters = methodInfo.GetParameters()
                .Where(x => x.ParameterType == typeof(CancellationToken))
                .ToArray();

            if (tokenParameters.Length == 0)
                return CancellationToken.None;
            else if (tokenParameters.Length == 1)
                return (CancellationToken)args[tokenParameters[0].Position];
            else
                throw new InvalidOperationException("Multiple CancellationToken parameters TODO msg");
        }

        private static object ContinueAs(Type taskResultType, Func<Task<object>> taskFactory)
        {
            var method = typeof(EndpointProxy).GetMethod(nameof(CastTask), BindingFlags.NonPublic | BindingFlags.Static);
            var genericMethod = method.MakeGenericMethod(taskResultType);

            return genericMethod.Invoke(null, new object[] { taskFactory });
        }

        private static Task<T> CastTask<T>(Func<Task<object>> taskFactory)
        {
            return taskFactory().ContinueWith(x => (T)x.Result);
        }
    }
}