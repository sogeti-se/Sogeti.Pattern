using System;
using Sogeti.Pattern.InversionOfControl;

namespace Sogeti.Pattern.DomainEvents
{
    /// <summary>
    /// Uses the <see cref="ServiceResolver"/> to find subscribers.
    /// </summary>
    public class ServiceResolverDispatcher : IDomainEventDispatcher
    {
        #region IDomainEventDispatcher Members

        /// <summary>
        /// Dispatch a new event to all listeners.
        /// </summary>
        /// <param name="e">Domain event</param>
        /// <remarks>Exceptions are not handled and will there break the processing. It's up to the caller to process any exceptions.</remarks>
        public void Dispatch(IDomainEvent e)
        {
            if (e == null) throw new ArgumentNullException("e");

            var type = typeof (IAutoSubscriberOf<>).MakeGenericType(e.GetType());
            var invokeMethod = type.GetMethod("Handle", new[] {e.GetType()});
            var parameters = new object[] {e};
            foreach (var handler in ServiceResolver.Current.ResolveAll(type))
            {
                invokeMethod.Invoke(handler, parameters);
            }
        }

        #endregion
    }
}