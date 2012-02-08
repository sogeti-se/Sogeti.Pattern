using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Sogeti.Pattern.InversionOfControl.Unity
{
    /// <summary>
    /// Used to be able to provide scoped components (where scope are controlled by the application type such as a HTTP Request/reply scope)
    /// </summary>
    public interface IScopedLifetimeFactory
    {
        /// <summary>
        /// Create a new life time manager
        /// </summary>
        /// <returns>A new scope</returns>
        /// <remarks>
        /// Scopes are used to control life time of objects registered in an container. Services are
        /// disposed when a scope is disposed.
        /// </remarks>
        LifetimeManager Create();
    }
}
