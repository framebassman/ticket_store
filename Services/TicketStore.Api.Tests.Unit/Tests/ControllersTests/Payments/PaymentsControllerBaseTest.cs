using TicketStore.Api.Controllers;
using TicketStore.Api.Tests.Unit.Stubs;
using TicketStore.Api.Tests.Unit.Stubs.Http;

namespace TicketStore.Api.Tests.Unit.Tests.ControllersTests.Payments
{
    public abstract class PaymentsControllerBaseTest : ControllersBaseTest<PaymentsController>
    {
        protected PaymentsController Controller;
        protected DummyEmailService EmailService;
        protected PaymentsControllerBaseTest(string databaseName) : base(databaseName)
        {
            EmailService = new DummyEmailService();
            Controller = new PaymentsController(
                Db,
                Logger,
                new DummyPdfConverter(),
                new DummyBarcodeConverter(),
                EmailService, new DummyHttpClientFactory()
            );
        }
    }
}
