using System;
using System.Collections;
using System.Collections.Generic;

namespace Sogeti.Pattern.InversionOfControl
{
    /// <summary>
    /// Our implementation of the Service Locator pattern.
    /// </summary>
    /// <remarks>Use it carefully since abuse of the service locator pattern makes it a nightmare
    /// when trying to find which dependencies a class have.</remarks>
    public interface IServiceResolver
    {
        /// <summary>
        /// Resolve a service
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>Service if found; otherwise <c>null</c>.</returns>
        T Resolve<T>() where T : class;

        /// <summary>
        /// Resolve a service
        /// </summary>
        /// <param name="type">Type to resolve</param>
        /// <returns>Service if found; otherwise <c>null</c>.</returns>
        object Resolve(Type type);

        /// <summary>
        /// Resolve all services
        /// </summary>
        /// <typeparam name="T">Type that the services must implement</typeparam>
        /// <returns>A collection of services (or an empty collection)</returns>
        IEnumerable<T> ResolveAll<T>() where T : class;


        /// <summary>
        /// Resolve all services
        /// </summary>
        /// <param name="type">Type that the services must implement</param>
        /// <returns>A collection of services (or an empty collection)</returns>
        IEnumerable ResolveAll(Type type);
    }
}