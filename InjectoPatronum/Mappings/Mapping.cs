namespace InjectoPatronum.Mappings
{
    public class Mapping : IMapping
    {
        private readonly Type _interface;
        private readonly Type _implementation;
        private readonly object[] _arguments;

        public Mapping(Type @interface, Type implementation, object[] arguments)
        {
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
            return injector.Instantiate(_implementation, _arguments);
        }
    }
}
