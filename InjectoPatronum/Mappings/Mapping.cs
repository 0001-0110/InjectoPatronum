namespace InjectoPatronum.Mappings
{
    public class Mapping : IMapping
    {
        private readonly Type _interface;
        private readonly Type _implementation;

        public Mapping(Type @interface, Type implementation)
        {
            _interface = @interface;
            _implementation = implementation;
        }

        public bool IsOfType(Type @interface)
        {
            return @interface == _interface;
        }

        public object? GetInstance(IDependencyInjector injector, Type @interface, object[] arguments)
        {
            return injector.Instantiate(_implementation, arguments);
        }
    }
}
