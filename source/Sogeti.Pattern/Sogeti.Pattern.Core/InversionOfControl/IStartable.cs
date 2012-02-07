namespace Sogeti.Pattern.InversionOfControl
{
    /// <summary>
    ///   Mark that a component can be started.
    /// </summary>
    /// <remarks>
    ///   All startable components will be started when registration have been completed. Startable components should be single instances.
    /// </remarks>
    public interface IStartable
    {
        /// <summary>
        ///   Called during application startup
        /// </summary>
        void StartComponent();
    }
}