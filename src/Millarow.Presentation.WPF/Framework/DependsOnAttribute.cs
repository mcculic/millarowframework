using System;
using System.Linq;
using System.Reflection;

namespace Millarow.Presentation.WPF.Framework
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DependsOnAttribute : Attribute
    {
        public DependsOnAttribute(params string[] dependencies)
        {
            Dependencies = dependencies;
        }

        public string[] Dependencies { get; }

        public static DependsOnAttribute GetAttribute(PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes(typeof(DependsOnAttribute), true).OfType<DependsOnAttribute>().FirstOrDefault();
        }

        public static bool IsTypeWithDependencies(Type type)
        {
            return type.GetProperties().Any(x => GetAttribute(x) != null);
        }

        public static void FillDependencyMap(Type type, DependencyMap map)
        {
            type.AssertNotNull(nameof(type));
            map.AssertNotNull(nameof(map));

            foreach (var propertyInfo in type.GetProperties())
            {
                var att = GetAttribute(propertyInfo);

                if (att != null && att.Dependencies != null)
                {
                    foreach (var dependencyName in att.Dependencies)
                        map.RegisterPropertyDependency(propertyInfo.Name, dependencyName);
                }
            }
        }
    }
}