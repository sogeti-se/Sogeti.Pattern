using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Sogeti.Pattern.InversionOfControl;

namespace Sogeti.Pattern.Core.Autofac
{
    /// <summary>
    /// Used to load all classes decorated with the [Component] attribute.
    /// </summary>
    public class ComponentRegistrar : IComponentRegistrar
    {
        private static ComponentRegistrar _registrar = new ComponentRegistrar();

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentRegistrar"/> class.
        /// </summary>
        protected ComponentRegistrar()
        {
            DefaultLifetime = Lifetime.Scoped;
        }

        /// <summary>
        /// Gets current implementation
        /// </summary>
        public static ComponentRegistrar Current { get { return _registrar; } }

        /// <summary>
        /// Assign a new implementation which will take care of the registration.
        /// </summary>
        /// <param name="registrar"></param>
        public static void Assign(ComponentRegistrar registrar)
        {
            _registrar = registrar;
        }

        /// <summary>
        /// Gets lifetime to use if no components have specified one.
        /// </summary>
        public Lifetime DefaultLifetime { get; set; }

        /// <summary>
        /// Register a new component.
        /// </summary>
        /// <param name="builder">autofac container builder</param>
        /// <param name="type">Type to register</param>
        public virtual void RegisterComponent(ContainerBuilder builder, Type type)
        {
            builder.RegisterType(type).AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();
        }
    }
}
