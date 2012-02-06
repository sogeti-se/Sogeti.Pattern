using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sogeti.Pattern
{
	/// <summary>
	/// extension methods for <see cref="Type"/>
	/// </summary>
	public static class TypeExtensions
	{
		/// <summary>
		/// Get all custom attributes for a type.
		/// </summary>
		/// <typeparam name="T">Type of attribute</typeparam>
		/// <param name="type">Type to get the attributes for</param>
		/// <param name="inherit">Check classes that the type inherits.</param>
		/// <returns>All found attributes or an empty collection.</returns>
		/// <example>
		/// <code>
		/// <![CDATA[
		/// [SomeAttribute];
		/// public class MyClass
		/// {
		/// }
		/// 
		/// var obj = new MyClass();
		/// var attributes = obj.GetType().GetAttributes<SomeAttribute>(true);
		/// if (!attributes.Any())
		///		return false;
		/// 
		/// //do something
		/// ]]>
		/// </code>
		/// </example>
		public static IEnumerable<T> GetAttributes<T>(this Type type, bool inherit) where T : Attribute
		{
			return type.GetCustomAttributes(typeof (T), inherit).Cast<T>();
		}
	}
}
