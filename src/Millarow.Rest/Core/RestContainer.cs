using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Millarow.Rest.Core
{
    [DebuggerStepThrough]
    internal class RestContainer : IRestContainer
    {
        private readonly IEnumerable _registrations;

        public RestContainer(IEnumerable registrations)
        {
            registrations.AssertNotNull(nameof(registrations));

            _registrations = registrations;
        }

        public IEnumerable<T> Resolve<T>()
        {
            foreach (var item in _registrations)
            {
                if (item is IRestContainer child)
                {
                    foreach (var childComponent in child.Resolve<T>())
                        yield return childComponent;
                }
                else if (item is T component)
                    yield return component;
            }
        }
    }
}
