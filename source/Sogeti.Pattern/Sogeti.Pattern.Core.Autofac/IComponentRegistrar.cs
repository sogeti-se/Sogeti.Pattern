using System;
using Autofac;

namespace Sogeti.Pattern.Core.Autofac
{
    public interface IComponentRegistrar
    {
        /// <summary>
        /// Register a new component.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="type"></param>
        void RegisterComponent(ContainerBuilder builder, Type type);
    }
}