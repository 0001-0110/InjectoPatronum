namespace InjectoPatronum.Mappings
{
    internal class SingletonMapping : IMapping
    {
        private readonly Type _interface;
        private readonly object _instance;

        public SingletonMapping(Type @interface, object instance)
        {
            _interface = @interface;
            _instance = instance;
        }

        public bool IsOfType(Type @interface)
        {
            return @interface == _interface;
        }

        public object? GetInstance(IDependencyInjector injector, Type @interface, params object[] arguments)
        {
            if (arguments.Length > 0)
                throw new ArgumentException("Arguments cannot be passed when calling a singleton");

            return _instance;
        }
    }
}
