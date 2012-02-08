using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sogeti.Pattern.DomainEvents
{
    /// <summary>
    /// Used to dispatch domain events in the current application
    /// </summary>
    /// <remarks>See <see cref="IDomainEvent"/> for an example</remarks>
    public interface IDomainEventDispatcher
    {
        /// <summary>
        /// Dispatch a new event to all listeners.
        /// </summary>
        /// <param name="e">Domain event</param>
        void Dispatch(IDomainEvent e);
    }
}
