using System.Reflection;

namespace InjectoPatronum.Extensions
{
	internal static class TypeExtensions
	{
		public static IEnumerable<MethodInfo> GetOverloads(this Type type, string methodName, BindingFlags bindingFlags = BindingFlags.Public)
		{
			return type.GetMethods(bindingFlags).Where(method => method.Name == methodName);
		}
	}
}
