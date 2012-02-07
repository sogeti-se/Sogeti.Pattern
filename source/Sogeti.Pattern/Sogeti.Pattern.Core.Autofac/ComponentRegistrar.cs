using System;
using System.Linq;
using Autofac;
using Sogeti.Pattern.InversionOfControl;

namespace Sogeti.Pattern.Core.Autofac
{
    /// <summary>
    ///   Used to load all classes decorated with the [Component] attribute.
    /// </summary>
    public class ComponentRegistrar : IComponentRegistrar
    {
        private static IComponentRegistrar _registrar = new ComponentRegistrar();
        [ThreadStatic] private static ContainerBuilder _builder;

        /// <summary>
        ///   Initializes a new instance of the <see cref="ComponentRegistrar" /> class.
        /// </summary>
        protected ComponentRegistrar()
        {
            DefaultLifetime = Lifetime.Scoped;
        }

        /// <summary>
        ///   Gets current implementation
        /// </summary>
        public static IComponentRegistrar Current
        {
            get { return _registrar; }
        }

        #region IComponentRegistrar Members

        /// <summary>
        ///   Gets lifetime to use if no components have specified one.
        /// </summary>
        public Lifetime DefaultLifetime { get; set; }

        /// <summary>
        ///   Register a new component.
        /// </summary>
        /// <param name="builder"> autofac container builder </param>
        /// <param name="type"> Type to register </param>
        public virtual void RegisterComponent(ContainerBuilder builder, Type type)
        {
            if (builder == null) throw new ArgumentNullException("builder");
            if (type == null) throw new ArgumentNullException("type");

            _builder = builder;
            var attribute = type.GetAttributes<ComponentAttribute>(false).Single();

            var lifetime = attribute.Lifetime == Lifetime.Default ? DefaultLifetime : attribute.Lifetime;

            switch (lifetime)
            {
                case Lifetime.Transient:
                    RegisterTransient(type, attribute);
                    break;
                case Lifetime.Singleton:
                    RegisterSingleton(type, attribute);
                    break;
                case Lifetime.Scoped:
                    RegisterScoped(type, attribute);
                    break;
                default:
                    throw new InvalidOperationException(
                        string.Format(
                            "Either the [Component] attribute on {0} or the ComponentRegistrar.DefaultLifetime must have been specified.",
                            type.FullName));
            }
        }

        /// <summary>
        /// Register a scoped component.
        /// </summary>
        /// <param name="type">Type of component to register</param>
        /// <param name="attribute">Component attribute</param>
        protected virtual void RegisterScoped(Type type, ComponentAttribute attribute)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (attribute == null) throw new ArgumentNullException("attribute");

            _builder.RegisterType(type).AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();
        }

        /// <summary>
        /// Registers the singleton.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="attribute">The attribute.</param>
        protected virtual void RegisterSingleton(Type type, ComponentAttribute attribute)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (attribute == null) throw new ArgumentNullException("attribute");

            _builder.RegisterType(type).AsImplementedInterfaces().AsSelf().SingleInstance();
        }

        /// <summary>
        /// Register a scoped component.
        /// </summary>
        /// <param name="type">Type of component to register</param>
        /// <param name="attribute">Component attribute</param>
        protected virtual void RegisterTransient(Type type, ComponentAttribute attribute)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (attribute == null) throw new ArgumentNullException("attribute");

            _builder.RegisterType(type).AsImplementedInterfaces().AsSelf();
        }

        #endregion

        /// <summary>
        ///   Assign a new implementation which will take care of the registration.
        /// </summary>
        /// <param name="registrar"> </param>
        public static void Assign(IComponentRegistrar registrar)
        {
            if (registrar == null) throw new ArgumentNullException("registrar");
            _registrar = registrar;
        }
    }
}