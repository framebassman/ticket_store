using System;
using TicketStore.Data;
using TicketStore.Web.Controllers;
using TicketStore.Web.Tests.Unit.TestData;

namespace TicketStore.Web.Tests.Unit.ControllersTests.Events
{
    public abstract class EventsControllerBaseTest : ControllersBaseTest<EventsController>, IDisposable
    {
        protected readonly ApplicationContext Db;
        protected readonly EventsController Controller;
        protected readonly Provider Provider;

        protected EventsControllerBaseTest(string databaseName) : base(databaseName)
        {
            Db = new ApplicationContext(Options);
            Controller = new EventsController(Logger, Db);
            Provider = new Provider();
        }
        
        public void Dispose()
        {
            Db.Dispose();
        }
    }
}