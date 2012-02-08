using System;
using System.Linq;
using Microsoft.Practices.Unity;
using Sogeti.Pattern.InversionOfControl;

namespace Sogeti.Pattern.InversionOfControl.Unity
{
    /// <summary>
    ///   Used to register [Component] decorated classes.
    /// </summary>
    public class ComponentRegistrar : IComponentRegistrar
    {
        [ThreadStatic] private static IUnityContainer _container;
        private static IComponentRegistrar _instance = new ComponentRegistrar();

        /// <summary>
        ///   Gets current implementation
        /// </summary>
        public static IComponentRegistrar Current
        {
            get { return _instance; }
        }

        /// <summary>
        ///   Assign a new implementation which will take care of the registration.
        /// </summary>
        /// <param name="registrar"> </param>
        public static void Assign(IComponentRegistrar registrar)
        {
            if (registrar == null) throw new ArgumentNullException("registrar");

            _instance = registrar;
        }


        #region IComponentRegistrar Members

        /// <summary>
        ///   Gets default life time if none has been specified with the [Component] attribute.
        /// </summary>
        /// <remarks>
        ///   Should be Lifetime.Scoped per default.
        /// </remarks>
        public Lifetime DefaultLifetime { get; set; }

        /// <summary>
        ///   Register a new component.
        /// </summary>
        /// <param name="container"> Container to register the type in </param>
        /// <param name="type"> Type to register. </param>
        public void RegisterComponent(IUnityContainer container, Type type)
        {
            _container = container;
            var attribute = type.GetAttributes<ComponentAttribute>(false).Single();
            var lifetime = attribute.Lifetime == Lifetime.Default ? DefaultLifetime : attribute.Lifetime;

            // singletons requries that we register the instance first.
            if (lifetime == Lifetime.Singleton)
            {
                RegisterType(type, type, new ContainerControlledLifetimeManager());
            }

            foreach (var @interface in type.GetInterfaces())
            {
                var lifetimeManager = GetLifetimeManager(type, lifetime);
                RegisterType(@interface, type, lifetimeManager);
            }
        }

        #endregion

        /// <summary>
        ///   Convert the lifetime enum to a lifetime manager in Unity.
        /// </summary>
        /// <param name="type"> Type to register </param>
        /// <param name="lifetime"> Lifetime to convert </param>
        /// <returns> A lifetime manager </returns>
        protected virtual LifetimeManager GetLifetimeManager(Type type, Lifetime lifetime)
        {
            if (type == null) throw new ArgumentNullException("type");

            LifetimeManager lifetimeManager;
            switch (lifetime)
            {
                case Lifetime.Transient:
                    lifetimeManager = new TransientLifetimeManager();
                    break;
                case Lifetime.Singleton:
                    lifetimeManager = new ContainerControlledLifetimeManager();
                    break;
                case Lifetime.Scoped:
                    lifetimeManager = ScopedLifetimeFactory.Current.Create();
                    break;
                default:
                    throw new InvalidOperationException(
                        string.Format(
                            "Either the [Component] attribute on {0} or the ComponentRegistrar.DefaultLifetime must have been specified.",
                            type.FullName));
            }

            return lifetimeManager;
        }

        /// <summary>
        /// Register a type in the container.
        /// </summary>
        /// <param name="service">Service (interface)</param>
        /// <param name="concrete">Class to return</param>
        /// <param name="lifetimeManager">Service lifetime</param>
        /// <remarks>Have to be able to handle collection services</remarks>
        /// <seealso cref="CollectionAttribute"/>
        protected virtual void RegisterType(Type service, Type concrete, LifetimeManager lifetimeManager)
        {
            if (service == null) throw new ArgumentNullException("service");
            if (concrete == null) throw new ArgumentNullException("concrete");
            if (lifetimeManager == null) throw new ArgumentNullException("lifetimeManager");


            // registering as an interface which is supposed to be resolve a collection of concretes.
            // need to name the registration to get it to work in unity.
            var collectionAttribute = service.GetAttributes<CollectionAttribute>(true).FirstOrDefault();
            if (collectionAttribute != null)
            {
                var name = Guid.NewGuid().ToString().Replace("-", "");
                Func<IUnityContainer, object> resolver = (unity) => unity.Resolve(concrete);
                _container.RegisterType(service, concrete, name, new InjectionFactory(resolver));
                return;
            }

            _container.RegisterType(service, concrete, lifetimeManager);
        }
    }
}