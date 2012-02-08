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
    public class ComponentRegistrarTests
    {
        [TestMethod]
        public void TestScoped()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAllComponents(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            ICoolers<Scoped> first, secondInSameScope, last;
            using (var scope  = container.BeginLifetimeScope())
            {
                first = scope.Resolve<ICoolers<Scoped>>();
                secondInSameScope = scope.Resolve<ICoolers<Scoped>>();
            }
            using (var scope = container.BeginLifetimeScope())
            {
                last = scope.Resolve<ICoolers<Scoped>>();
            }

            Assert.AreSame(first, secondInSameScope);
            Assert.AreNotSame(first, last);
        }

        [TestMethod]
        public void TestSingleton()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAllComponents(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            ICoolers<Singleton> first, last;
            using (var scope = container.BeginLifetimeScope())
                first = scope.Resolve<ICoolers<Singleton>>();
            using (var scope = container.BeginLifetimeScope())
                last = scope.Resolve<ICoolers<Singleton>>();


            Assert.AreSame(first, last);
        }

        [TestMethod]
        public void TestTransient()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAllComponents(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            var first = container.Resolve<ICoolers<Transient>>();
            var last = container.Resolve<ICoolers<Transient>>();


            Assert.AreNotSame(first, last);
        }


        [TestMethod]
        public void TestDefaultLifetimeTransient()
        {
            ComponentRegistrar.Current.DefaultLifetime = Lifetime.Transient;
            
            var builder = new ContainerBuilder();
            builder.RegisterAllComponents(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            var first = container.Resolve<ICoolers>();
            var last = container.Resolve<ICoolers>();

            Assert.AreNotSame(first, last);
        }

        [TestMethod]
        public void TestDefaultLifetimeSingleton()
        {
            ComponentRegistrar.Current.DefaultLifetime = Lifetime.Singleton;

            var builder = new ContainerBuilder();
            builder.RegisterAllComponents(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            ICoolers<Singleton> first, last;
            using (var scope = container.BeginLifetimeScope())
                first = scope.Resolve<ICoolers<Singleton>>();
            using (var scope = container.BeginLifetimeScope())
                last = scope.Resolve<ICoolers<Singleton>>();


            Assert.AreSame(first, last);
        }

        [TestMethod]
        public void TestDefaultLifetimeScoped()
        {
            ComponentRegistrar.Current.DefaultLifetime = Lifetime.Scoped;

            var builder = new ContainerBuilder();
            builder.RegisterAllComponents(Assembly.GetExecutingAssembly());
            var container = builder.Build();

            ICoolers<Scoped> first, secondInSameScope, last;
            using (var scope = container.BeginLifetimeScope())
            {
                first = scope.Resolve<ICoolers<Scoped>>();
                secondInSameScope = scope.Resolve<ICoolers<Scoped>>();
            }
            using (var scope = container.BeginLifetimeScope())
            {
                last = scope.Resolve<ICoolers<Scoped>>();
            }

            Assert.AreSame(first, secondInSameScope);
            Assert.AreNotSame(first, last);
        }


        [TestMethod]
        public void TestDefaultLifetimeScopedWithASpecifiedComponent()
        {
            ComponentRegistrar.Current.DefaultLifetime = Lifetime.Singleton;

            var builder = new ContainerBuilder();
            builder.RegisterAllComponents(Assembly.GetExecutingAssembly());
            var container = builder.Build();


            var first = container.Resolve<ICoolers<Transient>>();
            var last = container.Resolve<ICoolers<Transient>>();

            Assert.AreNotSame(first, last);
        }
    }
}
