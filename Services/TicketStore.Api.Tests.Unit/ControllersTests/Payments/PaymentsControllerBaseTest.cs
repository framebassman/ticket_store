using System;
using TicketStore.Api.Controllers;
using TicketStore.Api.Tests.Unit.Stubs;
using TicketStore.Data;

namespace TicketStore.Api.Tests.Unit.ControllersTests.Payments
{
    public abstract class PaymentsControllerBaseTest : ControllersBaseTest<PaymentsController>, IDisposable
    {
        protected readonly ApplicationContext Db;
        protected readonly PaymentsController Controller;
        protected readonly DummyEmailService EmailService;
        protected PaymentsControllerBaseTest(string databaseName) : base(databaseName)
        {
            Db = new ApplicationContext(Options);
            EmailService = new DummyEmailService();
            Controller = new PaymentsController(Db, Logger, new DummyConverter(), EmailService);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}