namespace InjectoPatronum.Mappings
{
    internal class GenericMapping : IMapping
    {
        private readonly Type _interface;
        private readonly Type _implementation;
        private readonly object[] _arguments;

        public GenericMapping(Type @interface, Type implementation, params object[] arguments)
        {
            if (!@interface.IsGenericTypeDefinition || !implementation.IsGenericTypeDefinition)
                throw new ArgumentException("Expected generic type definition");

            _interface = @interface;
            _implementation = implementation;
            _arguments = arguments;
        }

        public bool IsOfType(Type @interface)
        {
            return @interface == _interface;
        }

        public object? GetInstance(IDependencyInjector injector, Type @interface)
        {
            return injector.Instantiate(_implementation.MakeGenericType(@interface.GetGenericArguments()), _arguments);
        }
    }
}
