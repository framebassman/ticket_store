using System;
using TicketStore.Api.Tests.Unit.Tests.BaseTest;

namespace TicketStore.Api.Tests.Unit.Tests.ControllersTests
{
    public abstract class ControllersBaseTest<T> : DbBaseTest<T>
    {
        public ControllersBaseTest(String databaseName) : base(databaseName) {}
    }
}
