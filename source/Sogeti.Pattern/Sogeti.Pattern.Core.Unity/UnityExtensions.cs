using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Hosting;
using Microsoft.Practices.Unity;
using Sogeti.Pattern.InversionOfControl;

namespace Sogeti.Pattern.InversionOfControl.Unity
{
    /// <summary>
    ///   Extension methods for unity
    /// </summary>
    public static class UnityExtensions
    {
        /// <summary>
        ///   Registers all components that are found in the specified assembly
        /// </summary>
        /// <param name="container"> The container. </param>
        /// <param name="assembly"> The assembly to scan. </param>
        /// <remarks>
        ///   <para>Uses
        ///     <see cref="ComponentAttribute" />
        ///     to find all components</para> <para>
        ///                                     <see cref="Lifetime.Scoped" />
        ///                                     is used as the default lifetime.</para>
        /// </remarks>
        public static void RegisterAllComponents(this IUnityContainer container, Assembly assembly)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (assembly == null) throw new ArgumentNullException("assembly");

            foreach (var type in assembly.GetTypes())
            {
                var attribute = type.GetAttributes<ComponentAttribute>(true).FirstOrDefault();
                if (attribute == null)
                    continue;

                ComponentRegistrar.Current.RegisterComponent(container, type);
            }
        }

        /// <summary>
        ///   Scans the current folder for assemblies that has Autofac IModule classes.
        /// </summary>
        /// <param name="container"> Builder to use </param>
        /// <param name="filePattern"> Filename pattern (can use <see cref="Directory.GetFiles(string)" /> patterns) </param>
        public static void RegisterModules(this IUnityContainer container, string filePattern)
        {
            var path = HostingEnvironment.ApplicationPhysicalPath;
            path = path != null ? Path.Combine(path, "bin") : AppDomain.CurrentDomain.BaseDirectory;

            if (container == null) throw new ArgumentNullException("container");
            if (filePattern == null) throw new ArgumentNullException("filePattern");
            foreach (var fullPath in Directory.GetFiles(path, filePattern))
            {
                var filename = Path.GetFileName(fullPath).ToLower();
                var assembly =
                    AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(asm => MatchFile(asm, filename)) ??
                    Assembly.LoadFrom(fullPath);

                RegisterModules(container, assembly);
            }
        }

        private static bool MatchFile(Assembly asm, string filename)
        {
            if (asm == null) throw new ArgumentNullException("asm");
            if (filename == null) throw new ArgumentNullException("filename");
            return filename.Equals(Path.GetFileName(asm.Location), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///   Register all found <see cref="IContainerModule" /> classes in the specified assembly.
        /// </summary>
        /// <param name="container"> Builder to register the modules in </param>
        /// <param name="assembly"> Assembly to search in </param>
        public static void RegisterModules(this IUnityContainer container, Assembly assembly)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (assembly == null) throw new ArgumentNullException("assembly");
            foreach (
                var moduleType in
                    assembly.GetTypes().Where(t => typeof (IContainerModule).IsAssignableFrom(t) && !t.IsAbstract))
            {
                var module = (IContainerModule) Activator.CreateInstance(moduleType);
                module.BuildContainer(container);
            }
        }
    }
}