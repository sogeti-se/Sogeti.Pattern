using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Sogeti.Pattern.DomainEvents;
using Sogeti.Pattern.InversionOfControl;

namespace Sogeti.Pattern.Core.Tests.DomainEvents
{
    [TestClass]
    public class ServiceResolverDispatcherTests
    {
        readonly ServiceResolverDispatcher _dispatcher = new ServiceResolverDispatcher();

        [TestMethod]
        public void Test()
        {
            // assign
            var domainEvent = new UncaughtException(new Exception());
            var subscriber = new Mock<IAutoSubscriberOf<UncaughtException>>();
            subscriber.Setup(r => r.Handle(It.Is<UncaughtException>(k => k == domainEvent))).Verifiable();
            var resolver = new Mock<IServiceResolver>();
            resolver.Setup(r => r.ResolveAll(It.IsAny<Type>())).Returns(
                new List<IAutoSubscriberOf<UncaughtException>>() {subscriber.Object}).Verifiable();

            // act
            ServiceResolver.Assign(resolver.Object);
            _dispatcher.Dispatch(domainEvent);


            // assert
            subscriber.VerifyAll();
        }
    }

   
}
