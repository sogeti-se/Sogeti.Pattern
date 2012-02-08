using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sogeti.Pattern.InversionOfControl.Autofac.Tests.TestSubjects
{
    [Component(Lifetime.Scoped)]
    class Scoped : ICoolers<Scoped>
    {
    }
}
