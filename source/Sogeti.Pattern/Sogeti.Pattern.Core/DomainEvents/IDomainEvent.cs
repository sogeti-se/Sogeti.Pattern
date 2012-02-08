namespace Sogeti.Pattern.DomainEvents
{
    /// <summary>
    /// All implementations are domain events
    /// </summary>
    /// <remarks>
    /// <para>
    /// Are used to show intent and nothing else. 
    /// </para>
    /// <para>
    /// All domain events are synchronous to allow that everything is run in the same UnitOfWork (or transaction). 
    /// The domain events are only intended to be distributed in the same process.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// public class UserChanged : IDomainEvent
    /// {
    ///     public UserChanged(IUser user)
    ///     {}
    /// }
    /// 
    /// [Component]
    /// public class UserAuditLogger : IAutoSubcriberOf<UserChanged>
    /// {
    ///     public void Process(UserChanged e)
    ///     {
    ///         _logger.Info(Thread.CurrentPrincipal.Identity.Name + " changed usr " + e.User);
    ///     }
    /// }
    /// 
    /// [Component]
    /// public class UserService : IUserService
    /// {
    ///     public void ChangeName(UserId id, string firstName, string lastName)
    ///     {
    ///         using (var uow = UnitOfWork.Create())
    ///         {
    ///             var user = _repos.Load(id.ToString());
    ///             user.SetName(firstName, lastName);
    /// 
    ///             DomainEventDispatcher.Current.Dispatch(new UserChanged(user));
    /// 
    ///             uow.SaveChanges();
    ///         }
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// </example>
    public interface IDomainEvent
    {
    }
}