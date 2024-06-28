﻿namespace InjectoPatronum
{
	public interface IDependencyInjector
	{
		IDependencyInjector Map<TInterface, TImplementation>() where TInterface : class where TImplementation : TInterface;

		IDependencyInjector Map<TInterface, TImplementation>(params object[] arguments) where TInterface : class where TImplementation : TInterface;

		IDependencyInjector MapSingleton<TInterface, TImplementation>() where TInterface : class where TImplementation : TInterface;

		IDependencyInjector MapSingleton<TInterface, TImplementation>(TImplementation singleton) where TInterface : class where TImplementation : TInterface;

		object? Instantiate(Type type, params object[] arguments);

		
		T? Instantiate<T>(params object[] arguments);

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
	}
}
