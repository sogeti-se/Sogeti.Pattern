using System;

namespace Sogeti.Pattern.InversionOfControl
{
    /// <summary>
    ///   Used to define that a service/component is intended to be fetch as a collection (container.ResolveAll)
    /// </summary>
    /// <example>
    ///   <code>[Collection]
    ///     public interface IStartable
    ///     {
    ///     }
    /// 
    ///     [Component]
    ///     public class MyService : IStartable
    ///     {
    ///     }
    /// 
    ///     <![CDATA[
    /// var startables = service.ResolveAll<IStartable>();
    /// ]]>
    ///   </code>
    /// </example>
    /// <remarks>
    ///   Some containers do not support <c>ResolveAll</c> per default. This attribute is a way to overcome that limitation.
    /// </remarks>
    public class CollectionAttribute : Attribute
    {
    }
}