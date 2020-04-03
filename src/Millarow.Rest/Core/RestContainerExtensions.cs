using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Millarow.Rest.Core
{
    [DebuggerStepThrough]
    public static class RestContainerExtensions
    {
        public static void ExecuteFor<T>(this IRestContainer container, Action<T> action)
        {
            foreach (var c in container.Resolve<T>())
                action(c);
        }

        public static bool IsRegistered<T>(this IRestContainer container)
        {
            return container.Resolve<T>().Any();
        }

        public static T Optional<T>(this IRestContainer container)
        {
            return container.Resolve<T>().FirstOrDefault();
        }

        public static T Required<T>(this IRestContainer container)
        {
            var component = container.Resolve<T>().FirstOrDefault();
            if (component != null)
                return component;

            throw new InvalidOperationException($"Component '{typeof(T)}' is not registered"); //TODO ex type, msg
        }

        public static IRestContainer Union(this IRestContainer container, IRestContainer with)
        {
            container.AssertNotNull(nameof(container));
            with.AssertNotNull(nameof(with));

            return new CompositeProvider(new[] { with, container });
        }

        private class CompositeProvider : IRestContainer
        {
            private readonly IEnumerable<IRestContainer> _sources;

            public CompositeProvider(IEnumerable<IRestContainer> sources)
            {
                _sources = sources;
            }

            public IEnumerable<T> Resolve<T>()
            {
                return _sources.SelectMany(x => x.Resolve<T>());
            }
        }
    }
}
