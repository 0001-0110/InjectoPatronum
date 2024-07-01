namespace InjectoPatronum.Mappings
{
    internal interface IMapping
    {
        bool IsOfType(Type @interface);

        object? GetInstance(IDependencyInjector injector, Type @interface);
    }
}
