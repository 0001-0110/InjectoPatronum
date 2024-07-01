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

        public object? GetInstance(IDependencyInjector injector, Type @interface)
        {
            return _instance;
        }
    }
}
