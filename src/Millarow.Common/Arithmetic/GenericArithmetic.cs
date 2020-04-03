using Millarow.Arithmetic.Internal;
using Millarow.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Millarow.Arithmetic
{
    public static class GenericArithmetic
    {
        private readonly static Dictionary<Type, object> _implementations = new Dictionary<Type, object>();

        static GenericArithmetic()
        {
            var implType = typeof(Arithmetic<>);
            var implementations =
                from x in implType.Assembly.GetTypes()
                where x.Namespace == implType.Namespace
                where x.IsClass && !x.IsAbstract
                where x.BaseType.GetGenericTypeDefinition() == implType
                where x.HasParameterlessConstructor()
                select Activator.CreateInstance(x);

            foreach (var math in implementations)
            {
                var type = math.GetType().BaseType.GetGenericArguments().Single();

                _implementations.Add(type, math);
            }
        }

        public static void RegisterInstance<T>(IGenericArithmetic<T> math)
        {
            math.AssertNotNull(nameof(math));

            if (_implementations.ContainsKey(typeof(T)))
                throw new InvalidOperationException($"Implementation of {typeof(IGenericArithmetic<T>)} already registered.");

            _implementations.Add(typeof(T), math);
        }

        public static IGenericArithmetic<T> Get<T>()
        {
            if (_implementations.TryGetValue(typeof(T), out var math))
                return (IGenericArithmetic<T>)math;

            throw new NotSupportedException($"Implementation of {typeof(IGenericArithmetic<T>)} is not registered.");
        }
    }
}