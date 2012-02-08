using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sogeti.Pattern.DomainEvents;

namespace Sogeti.Pattern
{
    /// <summary>
    /// An uncaught exception have been handled somewhere in the system
    /// </summary>
    /// <remarks>The purpose of this event is to let the main application be able to either handle or log
    /// all uncaught exceptions. The event should mainly be used in worker threads to prevent the application
    /// from crashing</remarks>
    public class UncaughtException : IDomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UncaughtException"/> class.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public UncaughtException(Exception exception)
        {
            if (exception == null) throw new ArgumentNullException("exception");
            Exception = exception;
        }

        /// <summary>
        /// Gets caught exception
        /// </summary>
        public Exception Exception { get; private set; }
    }
}
