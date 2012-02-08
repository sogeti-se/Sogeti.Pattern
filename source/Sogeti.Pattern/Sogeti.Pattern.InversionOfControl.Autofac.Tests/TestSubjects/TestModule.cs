using Autofac;

namespace Sogeti.Pattern.InversionOfControl.Autofac.Tests.TestSubjects
{
    public class TestModule : IContainerModule
    {
        /// <summary>
        /// Add registrations to the container builder.
        /// </summary>
        /// <param name="builder">Builder to add registrations to.</param>
        public void BuildContainer(ContainerBuilder builder)
        {
            builder.RegisterType<TestModule>().AsImplementedInterfaces();
        }
    }
}