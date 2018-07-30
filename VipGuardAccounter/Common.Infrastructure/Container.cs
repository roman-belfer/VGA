using System;
using System.Collections.Generic;

namespace Common.Infrastructure
{
    public class Container
    {
        static readonly IDictionary<Type, Type> types = new Dictionary<Type, Type>();
        static readonly IDictionary<Type, object> instances = new Dictionary<Type, object>();

        public static void Register<TContract, TImplementation>(bool isSingletonInstance = true)
        {
            types[typeof(TContract)] = typeof(TImplementation);

            if (!isSingletonInstance) return;

            var result = Resolve(typeof(TContract));

            instances[typeof(TContract)] = result;
        }

        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public static object Resolve(Type contract)
        {
            if (instances.Count != 0 &&
                instances.ContainsKey(contract))
            {
                var instance = instances[contract];

                if (instance != null) return instance;
            }

            var implementation = types[contract];
            var constructor = implementation.GetConstructors()[0];
            var constructorParameters = constructor.GetParameters();

            if (constructorParameters.Length == 0)
            {
                return Activator.CreateInstance(implementation);
            }

            var parameters = new List<object>(constructorParameters.Length);

            foreach (var parameterInfo in constructorParameters)
            {
                parameters.Add(Resolve(parameterInfo.ParameterType));
            }

            return constructor.Invoke(parameters.ToArray());
        }
    }
}
