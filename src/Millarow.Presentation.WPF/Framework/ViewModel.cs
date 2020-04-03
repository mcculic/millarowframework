using System.Collections.Generic;

namespace Millarow.Presentation.WPF.Framework
{
    public abstract class ViewModel : BindableBase
    {
        private DependencyMap _propertyDependencyMap;

        public ViewModel()
        {
            //TODO optimize, cache and etc
            if (DependsOnAttribute.IsTypeWithDependencies(GetType()))
            {
                _propertyDependencyMap = new DependencyMap();
                DependsOnAttribute.FillDependencyMap(GetType(), _propertyDependencyMap);
            }
        }

        protected void RegisterPropertyDependency(string propertyName, string dependencyPropertyName)
        {
            if (_propertyDependencyMap == null)
                _propertyDependencyMap = new DependencyMap();

            _propertyDependencyMap.RegisterPropertyDependency(propertyName, dependencyPropertyName);
        }

        protected void RegisterPropertyDependency(string propertyName, params string[] dependencies)
        {
            foreach (var dep in dependencies)
                RegisterPropertyDependency(propertyName, dep);
        }

        protected IEnumerable<string> GetDependedProperties(string propertyName)
        {
            return _propertyDependencyMap.GetDependentProperties(propertyName);
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);

            if (_propertyDependencyMap != null && propertyName != null)
            {
                var dependentProperties = _propertyDependencyMap.GetDependentProperties(propertyName);

                foreach (var pn in dependentProperties)
                    OnPropertyChanged(pn);
            }
        }
    }
}