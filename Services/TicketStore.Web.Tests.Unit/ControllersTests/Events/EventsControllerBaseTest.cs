using TicketStore.Data;
using TicketStore.Web.Controllers;
using TicketStore.Web.Tests.Unit.TestData;

namespace TicketStore.Web.Tests.Unit.ControllersTests.Events
{
    public abstract class EventsControllerBaseTest : ControllersBaseTest<EventsController>
    {
        protected readonly ApplicationContext Db;
        protected readonly Provider Provider;

        protected EventsControllerBaseTest(string databaseName) : base(databaseName)
        {
            Db = new ApplicationContext(Options);
            Provider = new Provider();
        }
        
        public override void Dispose()
        {
            Db.Dispose();
        }
    }
}