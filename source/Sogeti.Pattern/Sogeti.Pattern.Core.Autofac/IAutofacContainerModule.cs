using Autofac;

namespace Sogeti.Pattern.InversionOfControl.Autofac
{
    /// <summary>
    /// Used to register components in the container when the [Component] attribute is not enough.
    /// </summary>
    /// <remarks>
    /// See base interface for details.
    /// </remarks>
    /// <seealso cref="IContainerModule{T}"/>
    public interface IAutofacContainerModule : IContainerModule<ContainerBuilder>
    {
    }
}
