using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Millarow.Rest.Core
{
    public class DynamicProxyFactory
    {
        private readonly AssemblyName _assemblyName;
        private readonly AssemblyBuilder _assembly;
        private readonly ModuleBuilder _module;
        private readonly Dictionary<Type, Type> _proxyTypeCache;

        public DynamicProxyFactory(AssemblyName assemblyName)
        {
            assemblyName.AssertNotNull(nameof(assemblyName));

            _assemblyName = assemblyName;
            _assembly = AssemblyBuilder.DefineDynamicAssembly(_assemblyName, AssemblyBuilderAccess.RunAndCollect);
            _module = _assembly.DefineDynamicModule("DynamicProxyLibrary");
            _proxyTypeCache = new Dictionary<Type, Type>();
        }

        public DynamicProxyFactory(string assemblyName)
            : this(new AssemblyName(assemblyName)
            {
                VersionCompatibility = AssemblyVersionCompatibility.SameProcess,
                Version = typeof(DynamicProxyFactory).Assembly.GetName().Version
            })
        {
        }

        public Type GetProxyType<THandler>(Type contractType)
            where THandler : class, IDynamicProxy
        {
            var handlerType = typeof(THandler);
            if (!handlerType.IsPublic && !IsInternallyVisibleTo(handlerType, _assemblyName.Name))
                throw new ArgumentException("Handler type shoud be public, otherwise apply InternalsVisibleTo attribute to the handler type assembly", nameof(THandler));

            contractType.AssertNotNull(nameof(contractType));

            lock (_proxyTypeCache)
            {
                if (!_proxyTypeCache.TryGetValue(contractType, out var proxyType))
                {
                    proxyType = CreateProxyType<THandler>(contractType);
                    _proxyTypeCache.Add(contractType, proxyType);
                }

                return proxyType;
            }
        }

        public Type GetProxyType<THandler, TContract>()
            where THandler : class, IDynamicProxy
        {
            return GetProxyType<THandler>(typeof(TContract));
        }

        private Type CreateProxyType<THandler>(Type contractType)
            where THandler : class, IDynamicProxy
        {
            if (!contractType.IsInterface)
                throw new ArgumentException($"Contract type should be an interface", nameof(contractType));

            var typeName = $"{AssemblyName}.{contractType.Name}_DynamicProxy";

            var typeBuilder = _module.DefineType(typeName,
                attr: typeof(THandler).Attributes,
                parent: typeof(THandler),
                interfaces: new[] { contractType });

            var members = contractType.GetMembers(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance).ToList();

            foreach (var constructorInfo in GetHandlerConstructors<THandler>())
            {
                CreateConstructor<THandler>(typeBuilder, constructorInfo);
                members.Remove(constructorInfo);
            }

            foreach (var methodInfo in GetContractMethods(contractType))
            {
                CreateMethod<THandler>(typeBuilder, methodInfo);
                members.Remove(methodInfo);
            }

            if (members.Any())
                throw new NotSupportedException(); //TODO msg

            return typeBuilder.CreateTypeInfo();
        }

        private IEnumerable<ConstructorInfo> GetHandlerConstructors<THandler>()
            where THandler : class, IDynamicProxy
        {
            return typeof(THandler).GetConstructors();
        }

        private void CreateConstructor<THandler>(TypeBuilder typeBuilder, ConstructorInfo constructorInfo)
            where THandler : class, IDynamicProxy
        {
            var parameters = constructorInfo.GetParameters();
            var constructorBuilder = typeBuilder.DefineConstructor(constructorInfo.Attributes | MethodAttributes.Public,
                callingConvention: constructorInfo.CallingConvention,
                parameterTypes: parameters.Select(x => x.ParameterType).ToArray(),
                requiredCustomModifiers: parameters.Select(p => p.GetRequiredCustomModifiers()).ToArray(),
                optionalCustomModifiers: parameters.Select(p => p.GetOptionalCustomModifiers()).ToArray());

            EmitConstructor(constructorBuilder, constructorInfo);
        }

        private IEnumerable<MethodInfo> GetContractMethods(Type contractType)
        {
            return contractType.GetMethods(BindingFlags.Public | BindingFlags.FlattenHierarchy | BindingFlags.Instance);
        }

        private void CreateMethod<THandler>(TypeBuilder typeBuilder, MethodInfo methodInfo)
            where THandler : class, IDynamicProxy
        {
            var parameters = methodInfo.GetParameters();
            var methodBuilder = typeBuilder.DefineMethod(methodInfo.Name,
                attributes: MethodAttributes.Public | MethodAttributes.Virtual,
                callingConvention: CallingConventions.Standard,
                returnType: methodInfo.ReturnType,
                returnTypeRequiredCustomModifiers: methodInfo.ReturnParameter.GetRequiredCustomModifiers(),
                returnTypeOptionalCustomModifiers: methodInfo.ReturnParameter.GetOptionalCustomModifiers(),
                parameterTypes: parameters.Select(p => p.ParameterType).ToArray(),
                parameterTypeRequiredCustomModifiers: parameters.Select(p => p.GetRequiredCustomModifiers()).ToArray(),
                parameterTypeOptionalCustomModifiers: parameters.Select(pi => pi.GetOptionalCustomModifiers()).ToArray());

            EmitMethod<THandler>(methodBuilder, methodInfo);

            typeBuilder.DefineMethodOverride(methodBuilder, methodInfo);
        }

        private void EmitConstructor(ConstructorBuilder constructorBuilder, ConstructorInfo constructorInfo)
        {
            var parameters = constructorInfo.GetParameters();
            var il = constructorBuilder.GetILGenerator();

            //load this
            il.Emit(OpCodes.Ldarg_0);

            foreach (var p in parameters)
                //load parameters[p.Position]
                il.Emit(OpCodes.Ldarg, p.Position + 1);

            //call
            il.Emit(OpCodes.Call, constructorInfo);

            //ret
            il.Emit(OpCodes.Ret);
        }

        private void EmitMethod<THandler>(MethodBuilder methodBuilder, MethodInfo methodInfo)
            where THandler : class, IDynamicProxy
        {
            var invokeMethod = typeof(THandler).GetMethod(nameof(IDynamicProxy.Invoke));
            var parameters = methodInfo.GetParameters();
            var il = methodBuilder.GetILGenerator();

            // args
            il.DeclareLocal(typeof(object[]));

            //args len
            il.Emit(OpCodes.Ldc_I4, parameters.Length);

            //args = new object[len];
            {
                il.Emit(OpCodes.Newarr, typeof(object));
                il.Emit(OpCodes.Stloc_0);
            }

            //args[x] = params[x]
            foreach (var p in parameters)
            {
                //load args
                il.Emit(OpCodes.Ldloc_0);

                //load args[i]
                il.Emit(OpCodes.Ldc_I4, p.Position);
                il.Emit(OpCodes.Ldarg, p.Position + 1);

                if (p.ParameterType.IsValueType)
                    il.Emit(OpCodes.Box, p.ParameterType);

                //set args[i]
                il.Emit(OpCodes.Stelem_Ref);
            }

            //load this
            il.Emit(OpCodes.Ldarg_0);

            //load args
            il.Emit(OpCodes.Ldstr, methodInfo.ToString());
            il.Emit(OpCodes.Ldloc_0);

            //call
            if (invokeMethod.IsVirtual)
                il.EmitCall(OpCodes.Callvirt, invokeMethod, null);
            else
                il.EmitCall(OpCodes.Call, invokeMethod, null);

            if (methodInfo.ReturnType == typeof(void))
                il.Emit(OpCodes.Pop);

            il.Emit(OpCodes.Ret);
        }

        private static bool IsInternallyVisibleTo(Type type, string assemblyName)
        {
            return type.Assembly.GetCustomAttributes<InternalsVisibleToAttribute>()
                .Any(x => x.AssemblyName == assemblyName);
        }

        public string AssemblyName => _assemblyName.Name;
    }
}
