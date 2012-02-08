using System;
using Autofac;
using Sogeti.Pattern.InversionOfControl;

namespace Sogeti.Pattern.InversionOfControl.Autofac
{
    /// <summary>
    ///   Use to register [Component] tagged classes in the container
    /// </summary>
    public interface IComponentRegistrar
    {
        /// <summary>
        ///   Gets default life time if none has been specified with the [Component] attribute.
        /// </summary>
        /// <remarks>
        ///   Should be Lifetime.Scoped per default.
        /// </remarks>
        Lifetime DefaultLifetime { get; set; }

        /// <summary>
        ///   Register a new component.
        /// </summary>
        /// <param name="builder"> Autofac builder </param>
        /// <param name="type"> Type to register. </param>
        void RegisterComponent(ContainerBuilder builder, Type type);
    }
}