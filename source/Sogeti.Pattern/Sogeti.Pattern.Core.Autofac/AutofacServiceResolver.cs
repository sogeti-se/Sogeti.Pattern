using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace Sogeti.Pattern.InversionOfControl.Autofac
{
    /// <summary>
    /// Autofac implementation of the service locator.
    /// </summary>
    public  class AutofacServiceResolver : IServiceResolver
    {
        private readonly IContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacServiceResolver"/> class.
        /// </summary>
        /// <param name="container">Autofac container.</param>
        public AutofacServiceResolver(IContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");

            _container = container;
        }

        /// <summary>
        /// Resolve a service
        /// </summary>
        /// <typeparam name="T">Type to resolve</typeparam>
        /// <returns>Service if found; otherwise <c>null</c>.</returns>
        public T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        /// <summary>
        /// Resolve a service
        /// </summary>
        /// <param name="type">Type to resolve</param>
        /// <returns>Service if found; otherwise <c>null</c>.</returns>
        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        /// <summary>
        /// Resolve all services
        /// </summary>
        /// <typeparam name="T">Type that the services must implement</typeparam>
        /// <returns>A collection of services (or an empty collection)</returns>
        public IEnumerable<T> ResolveAll<T>() where T : class
        {
            return _container.Resolve<IEnumerable<T>>();
        }

        /// <summary>
        /// Resolve all services
        /// </summary>
        /// <param name="type">Type that the services must implement</param>
        /// <returns>A collection of services (or an empty collection)</returns>
        public IEnumerable ResolveAll(Type type)
        {
            var collectionType = typeof (IEnumerable<>).MakeGenericType(type);
            return (IEnumerable)_container.Resolve(collectionType);
        }
    }
}
