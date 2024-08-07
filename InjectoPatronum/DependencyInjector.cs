using InjectoPatronum.Extensions;
using InjectoPatronum.Mappings;
using System.Reflection;

namespace InjectoPatronum
{
    public class DependencyInjector : IDependencyInjector
    {
        private readonly MappingCollection _mappings;

        public DependencyInjector()
        {
            _mappings = new MappingCollection();

            // So that every class that needs it can access it
            MapSingleton<IDependencyInjector, DependencyInjector>(this);
        }

        // TODO This is functionnal but kind of ugly, might need some work to make this code prettier
        private bool IsSuitableMethod(MethodBase method, Type[] argumentTypes)
        {
            // If the contructor doesn't have enough argument even without any dependencies added, it cannot be suitable
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length < argumentTypes.Length)
                return false;

            // Go through each dependency argument to check if this dependency has been mapped
            int dependencyIndex;
            for (dependencyIndex = 0; dependencyIndex < parameters.Length - argumentTypes.Length; dependencyIndex++)
                if (!_mappings.ContainsMappingFor(parameters[dependencyIndex].ParameterType))
                    return false;

            // Go through each remaining argument to check that they have the correct type
            int argumentIndex;
            for (argumentIndex = 0; dependencyIndex + argumentIndex < parameters.Length && argumentIndex < argumentTypes.Length; argumentIndex++)
                if (!argumentTypes[argumentIndex].IsAssignableTo(parameters[dependencyIndex + argumentIndex].ParameterType))
                    return false;

            // Check that we used all given arguments
            if (argumentIndex < argumentTypes.Length)
                return false;

            // This constructor can be instantiated by this injector with the given arguments
            return true;
        }

        private MethodBase? GetBestMethod(IEnumerable<MethodBase> methods, Type[] argumentTypes)
        {
            MethodBase? bestMethod = null;
            // We start at -1 so that constructor without arguments are not considered ambiguous
            int bestConstructorParameterCount = -1;

            foreach (MethodBase method in methods)
            {
                int parameterCount = method.GetParameters().Length;
                // If the current best constructor has more parameters than this one, we can skip it
                if (parameterCount < bestConstructorParameterCount)
                    continue;

                if (IsSuitableMethod(method, argumentTypes))
                {
                    if (bestConstructorParameterCount == parameterCount)
                        throw new AmbiguousMatchException("Multiple suitable constructor found with the same number of dependencies");

                    // Better constructor than previous one
                    bestMethod = method;
                    bestConstructorParameterCount = parameterCount;
                }
            }

            return bestMethod;
        }

        private IEnumerable<object?> GetDependencies(IEnumerable<ParameterInfo> requiredDependencies)
        {
            return requiredDependencies.Select(parameter => _mappings.GetInstanceFor(this, parameter.ParameterType));
        }

        private object[] AddDependencies(MethodBase method, object[] arguments)
        {
            var requiredArguments = method.GetParameters();
            return GetDependencies(requiredArguments.Take(requiredArguments.Length - arguments.Length)).Cast<object>().Concat(arguments).ToArray();
        }

        private ConstructorInfo? GetBestConstructor(Type type, Type[] argumentTypes)
        {
            return GetBestMethod(type.GetConstructors(BindingFlags.Instance | BindingFlags.Public), argumentTypes) as ConstructorInfo;
        }

        private MethodBase? GetBestMethod(Type type, MethodInfo methodInfo, BindingFlags bindingFlags, Type[] argumentTypes)
        {
            return GetBestMethod(type.GetOverloads(methodInfo.Name, bindingFlags), argumentTypes);
        }

        #region Map

        private DependencyInjector Map(Type implementation, params Type[] interfaces)
        {
            foreach (Type @interface in interfaces)
                _mappings.Add(new Mapping(@interface, implementation));
            return this;
        }

        public IDependencyInjector Map<TImplementation>() where TImplementation : class
        {
            // Map a class to itself
            return Map<TImplementation, TImplementation>();
        }

        public IDependencyInjector Map<TInterface, TImplementation>() where TInterface : class where TImplementation : TInterface
        {
            return Map(typeof(TImplementation), typeof(TInterface));
        }

        public IDependencyInjector Map<TInterface1, TInterface2, TImplementation>()
            where TInterface1 : class
            where TInterface2 : class
            where TImplementation : TInterface1, TInterface2
        {
            return Map(typeof(TImplementation), typeof(TInterface1), typeof(TInterface2));
        }

        public IDependencyInjector Map<TInterface1, TInterface2, TInterface3, TImplementation>()
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TImplementation : TInterface1, TInterface2, TInterface3
        {
            return Map(typeof(TImplementation), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3));
        }

        public IDependencyInjector Map<TInterface1, TInterface2, TInterface3, TInterface4, TImplementation>()
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4
        {
            return Map(typeof(TImplementation), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4));
        }

        public IDependencyInjector Map<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TImplementation>()
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5
        {
            return Map(typeof(TImplementation), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5));
        }

        public IDependencyInjector Map<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TImplementation>()
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6
        {
            return Map(typeof(TImplementation), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5), typeof(TInterface6));
        }

        public IDependencyInjector Map<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TImplementation>()
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TInterface7 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7
        {
            return Map(typeof(TImplementation), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5), typeof(TInterface6), typeof(TInterface7));
        }

        public IDependencyInjector Map<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TInterface8, TImplementation>()
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TInterface7 : class
            where TInterface8 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TInterface8
        {
            return Map(typeof(TImplementation), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5), typeof(TInterface6), typeof(TInterface7), typeof(TInterface8));
        }

        #endregion

        #region MapSingleton

        private DependencyInjector MapSingleton(object singleton, params Type[] interfaces)
        {
            foreach (Type @interface in interfaces)
                _mappings.Add(new SingletonMapping(@interface, singleton));
            return this;
        }

        public IDependencyInjector MapSingleton<TImplementation>(params object[] arguments)
            where TImplementation : class
        {
            // Map a class to itself
            return MapSingleton<TImplementation, TImplementation>(arguments);
        }

        public IDependencyInjector MapSingleton<TInterface, TImplementation>(params object[] arguments)
            where TInterface : class
            where TImplementation : TInterface
        {
            return MapSingleton<TInterface, TImplementation>(Instantiate<TImplementation>(arguments));
        }

        public IDependencyInjector MapSingleton<TInterface1, TInterface2, TImplementation>(params object[] arguments)
            where TInterface1 : class
            where TInterface2 : class
            where TImplementation : TInterface1, TInterface2
        {
            return MapSingleton(Instantiate<TImplementation>(arguments), typeof(TInterface1), typeof(TInterface2));
        }

        public IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TImplementation>(params object[] arguments)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TImplementation : TInterface1, TInterface2, TInterface3
        {
            return MapSingleton(Instantiate<TImplementation>(arguments), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3));
        }

        public IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TImplementation>(params object[] arguments)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4
        {
            return MapSingleton(Instantiate<TImplementation>(arguments), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4));
        }

        public IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TImplementation>(params object[] arguments)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5
        {
            return MapSingleton(Instantiate<TImplementation>(arguments), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5));
        }

        public IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TImplementation>(params object[] arguments)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6
        {
            return MapSingleton(Instantiate<TImplementation>(arguments), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5), typeof(TInterface6));
        }

        public IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TImplementation>(params object[] arguments)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TInterface7 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7
        {
            return MapSingleton(Instantiate<TImplementation>(arguments), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5), typeof(TInterface6), typeof(TInterface7));
        }

        public IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TInterface8, TImplementation>(params object[] arguments)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TInterface7 : class
            where TInterface8 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TInterface8
        {
            return MapSingleton(Instantiate<TImplementation>(arguments), typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5), typeof(TInterface6), typeof(TInterface7), typeof(TInterface8));
        }

        // These overload allows for the instance to be created manually

        public IDependencyInjector MapSingleton<TImplementation>(TImplementation singleton)
            where TImplementation : class
        {
            // Map a class to itself
            return MapSingleton<TImplementation, TImplementation>(singleton);
        }

        public IDependencyInjector MapSingleton<TInterface, TImplementation>(TImplementation singleton)
            where TInterface : class
            where TImplementation : TInterface
        {
            return MapSingleton(singleton, typeof(TInterface));
        }

        public IDependencyInjector MapSingleton<TInterface1, TInterface2, TImplementation>(TImplementation singleton)
            where TInterface1 : class
            where TInterface2 : class
            where TImplementation : TInterface1, TInterface2
        {
            return MapSingleton(singleton, typeof(TInterface1), typeof(TInterface2));
        }

        public IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TImplementation>(TImplementation singleton)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TImplementation : TInterface1, TInterface2, TInterface3
        {
            return MapSingleton(singleton, typeof(TInterface1), typeof(TInterface2), typeof(TInterface3));
        }

        public IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TImplementation>(TImplementation singleton)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4
        {
            return MapSingleton(singleton, typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4));
        }

        public IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TImplementation>(TImplementation singleton)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5
        {
            return MapSingleton(singleton, typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5));
        }

        public IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TImplementation>(TImplementation singleton)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6
        {
            return MapSingleton(singleton, typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5), typeof(TInterface6));
        }

        public IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TImplementation>(TImplementation singleton)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TInterface7 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7
        {
            return MapSingleton(singleton, typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5), typeof(TInterface6), typeof(TInterface7));
        }

        public IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TInterface8, TImplementation>(TImplementation singleton)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TInterface7 : class
            where TInterface8 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TInterface8
        {
            return MapSingleton(singleton, typeof(TInterface1), typeof(TInterface2), typeof(TInterface3), typeof(TInterface4), typeof(TInterface5), typeof(TInterface6), typeof(TInterface7), typeof(TInterface8));
        }

        #endregion

        #region MapGeneric

        public IDependencyInjector MapGeneric(Type @interface, Type implementation)
        {
            _mappings.Add(new GenericMapping(@interface, implementation));
            return this;
        }

        public IDependencyInjector MapGeneric(Type type, params Type[] interfaces)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Instantiate

        public object Instantiate(Type type, params object[] arguments)
        {
            ConstructorInfo? constructor = GetBestConstructor(type, arguments.Select(argument => argument.GetType()).ToArray());
            // Warning disabled for readability
#pragma warning disable IDE0270 // Use coalesce expression
            if (constructor == null)
                // HOW TO FIX :
                // 1. Check that the object you are trying to instantiate has a public constructor
                // 2. Check that each dependency has been mapped
                // 3. Check that all dependencies are first in the constructor
                // 4. Check that you are calling it with the right argument types
                // 5. Call for help (22#0130)
                throw new Exception($"Couldn't find any suitable constructor to instantiate {type}");
#pragma warning restore IDE0270 // Use coalesce 

            // Invoke the constructor with all the dependencies followed by all the given arguments
            return constructor.Invoke(AddDependencies(constructor, arguments));
        }

        public T Instantiate<T>(params object[] arguments)
        {
            return (T)Instantiate(typeof(T), arguments);
        }

        #endregion

        #region Execute

        private object? Execute(Type type, MethodInfo methodInfo, object[] arguments)
        {
            MethodBase? bestMethod = GetBestMethod(type, methodInfo, BindingFlags.Static | BindingFlags.Public, arguments.Select(arg => arg.GetType()).ToArray());
            return bestMethod?.Invoke(null, AddDependencies(bestMethod, arguments));
        }

        private object? Execute(object instance, MethodInfo methodInfo, object[] arguments)
        {
            MethodBase? bestMethod = GetBestMethod(instance.GetType(), methodInfo, BindingFlags.Instance | BindingFlags.Public, arguments.Select(arg => arg.GetType()).ToArray());
            return bestMethod?.Invoke(instance, AddDependencies(bestMethod, arguments));
        }

        public void Execute(Type type, Delegate method, params object[] arguments)
        {
            Execute(type, method.Method, arguments);
        }

        public T? Execute<T>(Type type, Delegate method, params object[] arguments)
        {
            return (T?)Execute(type, method.Method, arguments);
        }

        public void Execute(object instance, Delegate method, params object[] arguments)
        {
            Execute(instance, method.Method, arguments);
        }

        public T? Execute<T>(object instance, Delegate method, params object[] arguments)
        {
            return (T?)Execute(instance, method.Method, arguments);
        }

        #endregion
    }
}
