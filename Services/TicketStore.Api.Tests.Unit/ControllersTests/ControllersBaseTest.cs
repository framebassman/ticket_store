using System;
using TicketStore.Api.Tests.Unit.BaseTest;

namespace TicketStore.Api.Tests.Unit.ControllersTests
{
    public abstract class ControllersBaseTest<T> : DbBaseTest<T>
    {
        public ControllersBaseTest(String databaseName) : base(databaseName) {}
    }
}
