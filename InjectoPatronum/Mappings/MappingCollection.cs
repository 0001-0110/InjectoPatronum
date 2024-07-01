namespace InjectoPatronum.Mappings
{
    internal class MappingCollection
    {
        private readonly List<IMapping> _mappings;

        public MappingCollection()
        {
            _mappings = new List<IMapping>();
        }

        public void Add(IMapping mapping)
        {
            _mappings.Add(mapping);
        }

        public bool ContainsMappingFor(Type @interface)
        {
            return _mappings.Any(mapping => mapping.IsOfType(@interface));
        }

        public object? GetInstanceFor(IDependencyInjector injector, Type @interface)
        {
            return _mappings.SingleOrDefault(mapping => mapping.IsOfType(@interface))?.GetInstance(injector, @interface) ??
                throw new KeyNotFoundException("This type has not been mapped to any implementation");
        }
    }
}
