using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Sogeti.Pattern.Core.Unity
{
    /// <summary>
    /// Used to let each assembly provide it's own custom registrations
    /// </summary>
    public interface IContainerModule
    {
        /// <summary>
        /// Add registrations to the container
        /// </summary>
        /// <param name="container">Container to add registrations to.</param>
        void BuildContainer(IUnityContainer container);
    }
}
