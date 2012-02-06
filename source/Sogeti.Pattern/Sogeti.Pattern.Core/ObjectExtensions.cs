using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sogeti.Pattern
{
	/// <summary>
	/// Extensions used for object.
	/// </summary>
	public static class ObjectExtensions
	{
		/// <summary>
		/// A regular cast but can be changed with other methods.
		/// </summary>
		/// <typeparam name="T">Type to cast to</typeparam>
		/// <param name="instance">Instance being casted. No null check is made.</param>
		/// <returns>object casted.</returns>
		public static T As<T>(this object instance)
		{
			return (T) instance;
		}
	}
}
