using TicketStore.Web.Tests.Unit.BaseTest;

namespace TicketStore.Web.Tests.Unit.ControllersTests
{
    public abstract class ControllersBaseTest<T> : DbBaseTest<T>
    {
        public ControllersBaseTest(String databaseName) : base(databaseName)
        {
        }
    }
}
