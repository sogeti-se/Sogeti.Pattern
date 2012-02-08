using System;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sogeti.Pattern.InversionOfControl.Autofac.Tests.TestSubjects;

namespace Sogeti.Pattern.InversionOfControl.Autofac.Tests
{
    [TestClass]
    public class ContainerBuilderTests
    {

        [TestMethod]
        public void RegisterModules()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModules(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            Assert.IsNotNull(container.Resolve<IContainerModule>());
        }

        [TestMethod]
        public void RegisterComponent()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAllComponents(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            Assert.IsNotNull(container.Resolve<ICoolers>());
        }
    }

}
