using System;

namespace Sogeti.Pattern.DomainEvents
{
    /// <summary>
    /// Singleton proxy for the domain event dispatcher
    /// </summary>
    /// <remarks>Default implementation is <see cref="ServiceResolverDispatcher"/>.</remarks>
    public class DomainEventDispatcher
    {
        private static IDomainEventDispatcher _dispatcher = new ServiceResolverDispatcher();

        /// <summary>
        /// Gets current service resolver implementation
        /// </summary>
        public static IDomainEventDispatcher Current
        {
            get { return _dispatcher; }
        }


        /// <summary>
        /// Assign a new service resolver implementation.
        /// </summary>
        /// <param name="dispatcher">Resolver to use.</param>
        public static void Assign(IDomainEventDispatcher dispatcher)
        {
            if (dispatcher == null) throw new ArgumentNullException("dispatcher");

            _dispatcher = dispatcher;
        }
    }
}