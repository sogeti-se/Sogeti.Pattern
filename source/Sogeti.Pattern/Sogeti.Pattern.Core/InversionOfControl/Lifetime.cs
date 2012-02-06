using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sogeti.Pattern.InversionOfControl
{
	/// <summary>
	/// Used by <see cref="ComponentAttribute"/> to specify lifetime
	/// </summary>
	public enum Lifetime
	{
        /// <summary>
        /// Use default lifetime (as specified for the component loader)
        /// </summary>
        Default,

		/// <summary>
		/// Return a new object each time
		/// </summary>
		Transient,

		/// <summary>
		/// Scoped as in per HTTP request.
		/// </summary>
		/// <remarks>The scope depends on the type of application</remarks>
		Scoped,

		/// <summary>
		/// One per application lifetime.
		/// </summary>
		/// <remarks>Make sure that really want a singleton, since it can be tricky to combine lifetimes.</remarks>
		Singleton,
	}
}
