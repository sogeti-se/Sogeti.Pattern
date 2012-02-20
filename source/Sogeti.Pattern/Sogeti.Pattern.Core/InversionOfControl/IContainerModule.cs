using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sogeti.Pattern.InversionOfControl
{
    /// <summary>
    /// Used to do custom registrations in the container
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks><para>The [Component] attribute is not always enough. The purpose of
    /// container modules is to be able to do custom registrations for those cases.
    /// </para><para>
    /// Each container adapter for Sogeti.Pattern has an extension method called <c>RegisterModules</c> which
    /// is used to load and invoke all <c>IContainerModule</c> implementations.
    /// </para></remarks>
    public interface IContainerModule<in T>
    {
        /// <summary>
        /// Register services in the container
        /// </summary>
        /// <param name="containerBuilder">Object used to add registrations to your container.</param>
        void BuildContainer(T containerBuilder);
    }
}
