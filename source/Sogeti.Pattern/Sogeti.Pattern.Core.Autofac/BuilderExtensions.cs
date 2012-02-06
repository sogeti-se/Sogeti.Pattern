using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Sogeti.Pattern.InversionOfControl;

namespace Sogeti.Pattern.Core.Autofac
{
    /// <summary>
    /// Extension methods for the container builder
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Registers all components that are found in the specified assembly
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="assembly">The assembly to scan.</param>
        /// <remarks>
        /// <para>
        /// Uses <see cref="ComponentAttribute"/> to find all components
        /// </para>
        /// <para><see cref="Lifetime.Scoped"/> is used as the default lifetime.</para>
        /// </remarks>
        /// 
        public static void RegisterAllComponents(this ContainerBuilder builder, Assembly assembly)
        {
            if (builder == null) throw new ArgumentNullException("builder");
            if (assembly == null) throw new ArgumentNullException("assembly");

            foreach (var type in assembly.GetTypes())
            {
                var attribute = type.GetAttributes<ComponentAttribute>(true).FirstOrDefault();
                if (attribute == null)
                    continue;

                ComponentRegistrar.Instance.RegisterComponent(builder, type);
            }
        }

        /// <summary>
        /// Scans the current folder for assemblies that has Autofac IModule classes.
        /// </summary>
        /// <param name="builder">Builder to use</param>
        /// <param name="filePattern">Filename pattern (can use <see cref="Directory.GetFiles(string)"/> patterns)</param>
        public static void RegisterModules(this ContainerBuilder builder, string filePattern)
        {
            var path = HostingEnvironment.ApplicationPhysicalPath;
            path = path != null ? Path.Combine(path, "bin") : AppDomain.CurrentDomain.BaseDirectory;

            if (builder == null) throw new ArgumentNullException("builder");
            if (filePattern == null) throw new ArgumentNullException("filePattern");
            foreach (var fullPath in Directory.GetFiles(path, filePattern))
            {
                var filename = Path.GetFileName(fullPath).ToLower();
                var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(asm => MatchFile(asm, filename)) ??
                               Assembly.LoadFrom(fullPath);

                RegisterModules(builder, assembly);
            }
        }

        private static bool MatchFile(Assembly asm, string filename)
        {
            if (asm == null) throw new ArgumentNullException("asm");
            if (filename == null) throw new ArgumentNullException("filename");
            return filename.Equals(Path.GetFileName(asm.Location), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Register all found <see cref="IModule"/> classes in the specified assembly.
        /// </summary>
        /// <param name="builder">Builder to register the modules in</param>
        /// <param name="assembly">Assembly to search in</param>
        public static void RegisterModules(this ContainerBuilder builder, Assembly assembly)
        {
            if (builder == null) throw new ArgumentNullException("builder");
            if (assembly == null) throw new ArgumentNullException("assembly");
            foreach (var moduleType in assembly.GetTypes().Where(t => typeof(IModule).IsAssignableFrom(t) && !t.IsAbstract))
            {
                var module = (IModule)Activator.CreateInstance(moduleType);
                builder.RegisterModule(module);
            }
        }
    }

}
