using System;

namespace Sogeti.Pattern.InversionOfControl
{
    /// <summary>
    /// Service resolver singleton
    /// </summary>
    public class ServiceResolver
    {
        private static IServiceResolver _serviceResolver = new NoServiceResolver();

        /// <summary>
        /// Gets current service resolver implementation
        /// </summary>
        public static IServiceResolver Current
        {
            get { return _serviceResolver; }
        }


        /// <summary>
        /// Assign a new service resolver implementation.
        /// </summary>
        /// <param name="serviceResolver">Resolver to use.</param>
        public static void Assign(IServiceResolver serviceResolver)
        {
            if (serviceResolver == null) throw new ArgumentNullException("serviceResolver");

            _serviceResolver = serviceResolver;
        }
    }
}