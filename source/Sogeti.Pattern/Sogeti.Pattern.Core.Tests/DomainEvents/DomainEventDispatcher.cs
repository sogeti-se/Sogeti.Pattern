using System;
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
    public class DomainEventDispatcherTests
    {
        [TestMethod]
        public void Assign()
        {
            var dispatcher = new Mock<IDomainEventDispatcher>();
            DomainEventDispatcher.Assign(dispatcher.Object);

            Assert.AreSame(dispatcher.Object, DomainEventDispatcher.Current);
        }
    }
}
