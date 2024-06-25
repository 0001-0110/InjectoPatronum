namespace InjectoPatronum
{
	public interface IDependencyInjector
	{
		public IDependencyInjector Map<TInterface, TImplementation>() where TInterface : class where TImplementation : TInterface;

		public IDependencyInjector Map<TInterface, TImplementation>(params object[] arguments) where TInterface : class where TImplementation : TInterface;

		public IDependencyInjector MapSingleton<TInterface, TImplementation>() where TInterface : class where TImplementation : TInterface;

		public IDependencyInjector MapSingleton<TInterface, TImplementation>(TImplementation singleton) where TInterface : class where TImplementation : TInterface;

		public object? Instantiate(Type type, params object[] arguments);

		public T? Instantiate<T>(params object[] arguments);
	}
}
