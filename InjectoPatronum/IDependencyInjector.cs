namespace InjectoPatronum
{
    public interface IDependencyInjector
    {
        #region Map

        IDependencyInjector Map<TImplementation>() where TImplementation : class;

        IDependencyInjector Map<TInterface, TImplementation>()
            where TInterface : class where TImplementation : TInterface;

        IDependencyInjector Map<TInterface1, TInterface2, TImplementation>()
            where TInterface1 : class 
            where TInterface2 : class 
            where TImplementation : TInterface1, TInterface2;

        IDependencyInjector Map<TInterface1, TInterface2, TInterface3, TImplementation>() 
            where TInterface1 : class 
            where TInterface2 : class 
            where TInterface3 : class 
            where TImplementation : TInterface1, TInterface2, TInterface3;

        IDependencyInjector Map<TInterface1, TInterface2, TInterface3, TInterface4, TImplementation>() 
            where TInterface1 : class 
            where TInterface2 : class 
            where TInterface3 : class 
            where TInterface4 : class 
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4;

        IDependencyInjector Map<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TImplementation>() 
            where TInterface1 : class 
            where TInterface2 : class 
            where TInterface3 : class 
            where TInterface4 : class 
            where TInterface5 : class 
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5;

        IDependencyInjector Map<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TImplementation>() 
            where TInterface1 : class 
            where TInterface2 : class 
            where TInterface3 : class 
            where TInterface4 : class 
            where TInterface5 : class 
            where TInterface6 : class 
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6;

        IDependencyInjector Map<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TImplementation>()
            where TInterface1 : class 
            where TInterface2 : class 
            where TInterface3 : class 
            where TInterface4 : class 
            where TInterface5 : class 
            where TInterface6 : class 
            where TInterface7 : class 
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7;

        IDependencyInjector Map<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TInterface8, TImplementation>() 
            where TInterface1 : class 
            where TInterface2 : class 
            where TInterface3 : class 
            where TInterface4 : class 
            where TInterface5 : class 
            where TInterface6 : class 
            where TInterface7 : class 
            where TInterface8 : class 
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TInterface8;

        #endregion

        #region MapSingleton

        // Arguments have to be passed during the mapping since the instance will be created as soon as the mapping is done

        IDependencyInjector MapSingleton<TImplementation>(params object[] arguments)
            where TImplementation : class;

        IDependencyInjector MapSingleton<TInterface, TImplementation>(params object[] arguments) 
            where TInterface : class 
            where TImplementation : TInterface;

        IDependencyInjector MapSingleton<TInterface1, TInterface2, TImplementation>(params object[] arguments) 
            where TInterface1 : class
            where TInterface2 : class
            where TImplementation : TInterface1, TInterface2;

        IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TImplementation>(params object[] arguments) 
            where TInterface1 : class 
            where TInterface2 : class
            where TInterface3 : class
            where TImplementation : TInterface1, TInterface2, TInterface3;

        IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TImplementation>(params object[] arguments) 
            where TInterface1 : class 
            where TInterface2 : class 
            where TInterface3 : class
            where TInterface4 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4;

        IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TImplementation>(params object[] arguments)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5;

        IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TImplementation>(params object[] arguments)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6;

        IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TImplementation>(params object[] arguments)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TInterface7 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7;

        IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TInterface8, TImplementation>(params object[] arguments)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TInterface7 : class
            where TInterface8 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TInterface8;

        IDependencyInjector MapSingleton<TImplementation>(TImplementation implementation)
            where TImplementation : class;

        IDependencyInjector MapSingleton<TInterface, TImplementation>(TImplementation implementation)
            where TInterface : class
            where TImplementation : TInterface;

        IDependencyInjector MapSingleton<TInterface1, TInterface2, TImplementation>(TImplementation implementation)
            where TInterface1 : class
            where TInterface2 : class
            where TImplementation : TInterface1, TInterface2;

        IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TImplementation>(TImplementation implementation)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TImplementation : TInterface1, TInterface2, TInterface3;

        IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TImplementation>(TImplementation implementation)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4;

        IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TImplementation>(TImplementation implementation)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5;

        IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TImplementation>(TImplementation implementation)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6;

        IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TImplementation>(TImplementation implementation)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TInterface7 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7;

        IDependencyInjector MapSingleton<TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TInterface8, TImplementation>(TImplementation implementation)
            where TInterface1 : class
            where TInterface2 : class
            where TInterface3 : class
            where TInterface4 : class
            where TInterface5 : class
            where TInterface6 : class
            where TInterface7 : class
            where TInterface8 : class
            where TImplementation : TInterface1, TInterface2, TInterface3, TInterface4, TInterface5, TInterface6, TInterface7, TInterface8;

        #endregion

        #region MapGeneric

        IDependencyInjector MapGeneric(Type @interface, Type type);

        IDependencyInjector MapGeneric(Type type, params Type[] interfaces);

        #endregion

        #region Instantiate

        object Instantiate(Type type, params object[] arguments);

        T Instantiate<T>(params object[] arguments);

        #endregion

        #region Execute

        /// <summary>
        /// Executes the static method while filling missing arguments with the mapped dependencies
        /// </summary>
        /// <remarks>
        /// This is used for void methods. If you want to get the result, you have to add the return type as type argument
        /// </remarks>
        void Execute(Type instance, Delegate @delegate, params object[] arguments);

        /// <summary>
        /// Executes the static method while filling missing arguments with the mapped dependencies
        /// </summary>
        /// <returns>
        /// The result of the function casted to the type argument
        /// </returns>
        T? Execute<T>(Type instance, Delegate @delegate, params object[] arguments);

        /// <summary>
        /// Executes the instance method while filling missing arguments with the mapped dependencies
        /// </summary>
        /// <remarks>
        /// This is used for void methods. If you want to get the result, you have to add the return type as type argument
        /// </remarks>
        void Execute(object instance, Delegate @delegate, params object[] arguments);

        /// <summary>
        /// Executes the instance method while filling missing arguments with the mapped dependencies
        /// </summary>
        /// <returns>
        /// The result of the function casted to the type argument
        /// </returns>
        T? Execute<T>(object instance, Delegate @delegate, params object[] arguments);

        #endregion
    }
}
