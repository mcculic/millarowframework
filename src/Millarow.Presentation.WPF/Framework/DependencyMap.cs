using System;
using System.Collections.Generic;
using System.Linq;

namespace Millarow.Presentation.WPF.Framework
{
    public class DependencyMap
    {
        private readonly Dictionary<string, List<string>> _map;

        public DependencyMap()
        {
            _map = new Dictionary<string, List<string>>();
        }

        public void RegisterPropertyDependency(string propertyName, string dependencyPropertyName)
        {
            propertyName.AssertNotNull(nameof(propertyName));
            dependencyPropertyName.AssertNotNull(nameof(dependencyPropertyName));

            if (GetDependentProperties(propertyName).Contains(dependencyPropertyName))
                throw new InvalidOperationException("Circular dependency detected");

            List<string> dependencyList;
            if (!_map.TryGetValue(dependencyPropertyName, out dependencyList))
            {
                dependencyList = new List<string>();
                _map[dependencyPropertyName] = dependencyList;
            }

            if (!dependencyList.Contains(propertyName))
                dependencyList.Add(propertyName);
        }

        public IEnumerable<string> GetDependentProperties(string propertyName)
        {
            propertyName.AssertNotNull(nameof(propertyName));

            var returned = new HashSet<string>();
            var queue = new Queue<string>();

            queue.Enqueue(propertyName);

            while (queue.Count > 0)
            {
                propertyName = queue.Dequeue();

                var dependencyList = default(List<string>);
                if (_map.TryGetValue(propertyName, out dependencyList))
                {
                    foreach (var pn in dependencyList)
                    {
                        if (!returned.Contains(pn))
                        {
                            queue.Enqueue(pn);
                            returned.Add(pn);

                            yield return pn;
                        }
                    }
                }
            }
        }
    }
}