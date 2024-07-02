namespace InjectoPatronum.Mappings
{
    internal class GenericMapping : IMapping
    {
        private readonly Type _interface;
        private readonly Type _implementation;

        public GenericMapping(Type @interface, Type implementation)
        {
            if (!@interface.IsGenericTypeDefinition || !implementation.IsGenericTypeDefinition)
                throw new ArgumentException("Expected generic type definition");

            _interface = @interface;
            _implementation = implementation;
        }

        public bool IsOfType(Type @interface)
        {
            return @interface == _interface;
        }

        public object? GetInstance(IDependencyInjector injector, Type @interface, object[] arguments)
        {
            return injector.Instantiate(_implementation.MakeGenericType(@interface.GetGenericArguments()), arguments);
        }
    }
}
