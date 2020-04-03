using System.Collections;
using System.Diagnostics;
using System.Linq;

namespace Millarow.Rest.Core
{
    [DebuggerStepThrough]
    internal class RestContainerBuilder : IRestContainerBuilder
    {
        private readonly ArrayList _registrations;

        public RestContainerBuilder()
        {
            _registrations = new ArrayList();
        }

        public RestContainerBuilder(IRestContainer parent)
            : this()
        {
            parent.AssertNotNull(nameof(parent));

            _registrations.Add(parent);
        }

        public void Register(object component)
        {
            component.AssertNotNull(nameof(component));

            if (_registrations.Contains(component))
                _registrations.Remove(component);

            _registrations.Insert(0, component);
        }

        public void Register<T>() where T : new()
        {
            Register(new T());
        }

        public IRestContainer CreateContainer()
        {
            var container = new RestContainer(_registrations);

            foreach (var component in _registrations.OfType<ICompositeComponent>())
                component.ResolveDependencies(container);

            return container;
        }
    }
}
