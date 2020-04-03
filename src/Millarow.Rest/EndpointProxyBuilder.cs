using Millarow.Rest.Core;
using Millarow.Rest.Proxy;
using System;

namespace Millarow.Rest
{
    public class EndpointProxyBuilder
    {
        private readonly IRestContainerBuilder _container;

        internal EndpointProxyBuilder(IRestContainerBuilder container)
        {
            container.AssertNotNull(nameof(container));

            _container = container;
        }

        //TODO RoutePrefix(string prefix)

        public EndpointProxyBuilder With<T>(T component)
        {
            _container.Register(component);

            return this;
        }

        public EndpointProxyBuilder WithResultFilter<TResult>(Action<TResult> action)
        {
            action.AssertNotNull(nameof(action));

            return With(new InvocationPostFilter<TResult>(action));
        }

        //public RestSerializationBuilder<EndpointProxyBuilder> Serialization => new RestSerializationBuilder<EndpointProxyBuilder>(_container, this);

        private sealed class InvocationPostFilter<T> : IInvocatioтFilter
        {
            private readonly Action<T> _action;

            public InvocationPostFilter(Action<T> action) => _action = action;

            public void OnExecuted(InvocationResult result)
            {
                if (result.ReturnValue is T value)
                    _action(value);
            }
        }
    }
}
