using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sogeti.Pattern.InversionOfControl
{
    /// <summary>
    /// No resolver has been configured
    /// </summary>
    class NoServiceResolver : IServiceResolver
    {
        /// <summary>
        /// Resolve a service
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>Service if found; otherwise <c>null</c>.</returns>
        public T Resolve<T>() where T : class
        {
            throw new NotSupportedException("No resolver has been configured.");
        }

        /// <summary>
        /// Resolve a service
        /// </summary>
        /// <param name="type">Type to resolve</param>
        /// <returns>Service if found; otherwise <c>null</c>.</returns>
        public object Resolve(Type type)
        {
            throw new NotSupportedException("No resolver has been configured.");
        }

        /// <summary>
        /// Resolve all services
        /// </summary>
        /// <typeparam name="T">Type that the services must implement</typeparam>
        /// <returns>A collection of services (or an empty collection)</returns>
        public IEnumerable<T> ResolveAll<T>() where T : class
        {
            throw new NotSupportedException("No resolver has been configured.");
        }

        /// <summary>
        /// Resolve all services
        /// </summary>
        /// <param name="type">Type that the services must implement</param>
        /// <returns>A collection of services (or an empty collection)</returns>
        public IEnumerable ResolveAll(Type type)
        {
            throw new NotSupportedException("No resolver has been configured.");
        }
    }
}
