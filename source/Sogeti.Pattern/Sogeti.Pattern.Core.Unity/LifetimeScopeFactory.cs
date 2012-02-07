using System;
using Microsoft.Practices.Unity;

namespace Sogeti.Pattern.Core.Unity
{
    /// <summary>
    /// Creates a scoped lifetime manager
    /// </summary>
    /// <remarks>Scoped components have a scope which is different for each type of application. Web applications have scope which is active during the request/reply, while
    /// WPF applications have a scope which is active during an unit of work.</remarks>
    public class ScopedLifetimeFactory : IScopedLifetimeFactory
    {
        private static IScopedLifetimeFactory _instance = new ScopedLifetimeFactory();

        /// <summary>
        ///   Gets current implementation
        /// </summary>
        public static IScopedLifetimeFactory Current
        {
            get { return _instance; }
        }

        #region IScopedLifetimeFactory Members

        /// <summary>
        ///   Create a new life time manager
        /// </summary>
        /// <returns> A new scope </returns>
        /// <remarks>
        ///   Scopes are used to control life time of objects registered in an container. Services are disposed when a scope is disposed.
        /// </remarks>
        public virtual LifetimeManager Create()
        {
            return new PerThreadLifetimeManager();
        }

        #endregion

        /// <summary>
        /// Assign a new scoped factory
        /// </summary>
        /// <param name="factory"></param>
        public static void Assign(IScopedLifetimeFactory factory)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            _instance = factory;
        }
    }
}