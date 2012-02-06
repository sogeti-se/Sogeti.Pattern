using System;

namespace Sogeti.Pattern.InversionOfControl
{
    /// <summary>
    /// Mark a class as a component.
    /// </summary>
    /// <remarks>Put this attribute on your classes to let your container register all services for you.</remarks>
    [AttributeUsage(AttributeTargets.Class)]
    public class ComponentAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentAttribute"/> class.
        /// </summary>
        /// <param name="lifetime">Specify that the component has a specific lifetime.</param>
        public ComponentAttribute(Lifetime lifetime)
        {
            Lifetime = lifetime;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComponentAttribute"/> class.
        /// </summary>
        public ComponentAttribute()
        {
            Lifetime = Lifetime.Default;
        }


        /// <summary>
        /// Gets specified lifetime (if other than default lifetime should be used)
        /// </summary>
        public Lifetime Lifetime { get; set; }
    }
}