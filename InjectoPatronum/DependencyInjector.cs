using System.Reflection;

namespace InjectoPatronum
{
	public class DependencyInjector : IDependencyInjector
	{
		private readonly Dictionary<Type, Func<object?>> mappings;

		public DependencyInjector()
		{
			mappings = new Dictionary<Type, Func<object?>>();

			// So that every class that needs it can access it
			MapSingleton<IDependencyInjector, DependencyInjector>(this);
		}

		public IDependencyInjector Map<TInterface, TImplementation>() where TInterface : class where TImplementation : TInterface
		{
			mappings.Add(typeof(TInterface), () => Instantiate<TImplementation>());
			return this;
		}

		public IDependencyInjector Map<TInterface, TImplementation>(params object[] arguments) where TInterface : class where TImplementation : TInterface
		{
			mappings.Add(typeof(TInterface), () => Instantiate<TImplementation>(arguments));
			return this;
		}

		public IDependencyInjector MapSingleton<TInterface, TImplementation>() where TInterface : class where TImplementation : TInterface
		{
			// Create the singleton instance
			if (Instantiate(typeof(TImplementation)) is not TImplementation singleton)
				throw new Exception("This singleton instance could not be created. Check that all of its dependencies have been mapped beforehand and that there is a valid constructor.");
			return MapSingleton<TInterface, TImplementation>(singleton);
		}

		// This overload allows for the instance to be created manually
		public IDependencyInjector MapSingleton<TInterface, TImplementation>(TImplementation singleton) where TInterface : class where TImplementation : TInterface
		{
			mappings.Add(typeof(TInterface), () => singleton);
			return this;
		}

		private bool HasMapping(Type type)
		{
			return mappings.ContainsKey(type);
		}

		private object? GetInstance(Type interfaceType)
		{
			if (mappings.TryGetValue(interfaceType, out Func<object?>? factory))
				return factory.Invoke();

			throw new KeyNotFoundException("This type has not been mapped to any implementation");
		}

		// TODO This is functionnal but kind of ugly, might need some work to make this code prettier
		private bool IsSuitableConstructor(ConstructorInfo constructor, Type[] argumentTypes)
		{
			// If the contructor doesn't have enough argument even without any dependencies added, it cannot be suitable
			ParameterInfo[] parameters = constructor.GetParameters();
			if (parameters.Length < argumentTypes.Length)
				return false;

			// Go through each dependency argument to check if this dependency has been mapped
			int dependencyIndex;
			for (dependencyIndex = 0; dependencyIndex < parameters.Length - argumentTypes.Length; dependencyIndex++)
				if (!HasMapping(parameters[dependencyIndex].ParameterType))
					return false;

			// Go through each remaining argument to check that they have the correct type
			int argumentIndex;
			for (argumentIndex = 0; dependencyIndex + argumentIndex < parameters.Length && argumentIndex < argumentTypes.Length; argumentIndex++)
				if (parameters[dependencyIndex + argumentIndex].ParameterType != argumentTypes[argumentIndex])
					return false;

			// Check that the number of argument needed is the same as the number given
			if (argumentIndex < argumentTypes.Length)
				return false;

			// This constructor can be instantiated by this injector with the given arguments
			return true;
		}

		private ConstructorInfo? GetBestConstructor(Type type, Type[] argumentTypes)
		{
			ConstructorInfo? bestConstructor = null;
			// We start at -1 so that constructor without arguments are not considered ambiguous
			int bestConstructorParameterCount = -1;

			foreach (ConstructorInfo constructor in type.GetConstructors(BindingFlags.Public | BindingFlags.Instance))
			{
				int parameterCount = constructor.GetParameters().Length;
				// If the current best constructor has more parameters than this one, we can skip it
				if (parameterCount < bestConstructorParameterCount)
					continue;

				if (IsSuitableConstructor(constructor, argumentTypes))
				{
					if (bestConstructorParameterCount == parameterCount)
						throw new Exception("Multiple suitable constructor found with the same number of dependencies");

					// Better constructor than previous one
					bestConstructor = constructor;
					bestConstructorParameterCount = parameterCount;
				}
			}

			return bestConstructor;
		}

		public object? Instantiate(Type type, params object[] arguments)
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
#pragma warning restore IDE0270 // Use coalesce expression

			// Instantiate all dependencies
			int dependencyCount = constructor.GetParameters().Length - arguments.Length;
			IEnumerable<object?> dependencies = constructor.GetParameters().Take(dependencyCount).Select(parameter => GetInstance(parameter.ParameterType));

			// Invoke the constructor with all the dependencies followed by all the given arguments
			return constructor?.Invoke(dependencies.Cast<object>().Concat(arguments).ToArray());
		}

		public T? Instantiate<T>(params object[] arguments)
		{
			return (T?)Instantiate(typeof(T), arguments);
		}
	}
}
