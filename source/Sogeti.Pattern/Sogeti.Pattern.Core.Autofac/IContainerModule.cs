using Autofac;

namespace Sogeti.Pattern.InversionOfControl.Autofac
{
    /// <summary>
    /// Bridge pattern. 
    /// </summary>
    /// <remarks>Allows us to create a nuget packages which </remarks>
    public interface IContainerModule
    {
        /// <summary>
        /// Add registrations to the container builder.
        /// </summary>
        /// <param name="builder">Builder to add registrations to.</param>
        void BuildContainer(ContainerBuilder builder);
    }
}
