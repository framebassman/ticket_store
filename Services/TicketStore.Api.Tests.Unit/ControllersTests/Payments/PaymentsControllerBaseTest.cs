using System;
using System.Net.Http;
using TicketStore.Api.Controllers;
using TicketStore.Api.Tests.Unit.ModelTests.TicketPreview.Model;
using TicketStore.Api.Tests.Unit.Stubs;
using TicketStore.Api.Tests.Unit.Stubs.Http;

namespace TicketStore.Api.Tests.Unit.ControllersTests.Payments
{
    public abstract class PaymentsControllerBaseTest : ControllersBaseTest<PaymentsController>, IDisposable
    {
        protected readonly FakePaymentsController Controller;
        protected readonly DummyEmailService EmailService;
        protected PaymentsControllerBaseTest(string databaseName) : base(databaseName)
        {
            EmailService = new DummyEmailService();
            Controller = new FakePaymentsController(Db, Logger, new DummyConverter(), EmailService, new DummyHttpClientFactory());
        }
    }
}
