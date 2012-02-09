namespace Sogeti.Pattern.DomainEvents
{
    /// <summary>
    /// Indicates that the service want's to get all events of the specified type
    /// </summary>
    /// <typeparam name="T">Type of domain event</typeparam>
    /// <remarks>See <see cref="IDomainEvent"/> for an example.</remarks>
    public interface IAutoSubscriberOf<in T> where T : IDomainEvent
    {
        /// <summary>
        /// Handle the domain event
        /// </summary>
        /// <param name="e">Domain to process</param>
        void Handle(T e);
    }
}