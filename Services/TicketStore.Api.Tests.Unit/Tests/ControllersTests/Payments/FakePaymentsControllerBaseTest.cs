using TicketStore.Api.Tests.Unit.Stubs;
using TicketStore.Api.Tests.Unit.Stubs.Http;
using TicketStore.Api.Tests.Unit.Tests.ModelTests.TicketPreview.Model;

namespace TicketStore.Api.Tests.Unit.Tests.ControllersTests.Payments
{
    public abstract class FakePaymentsControllerBaseTest : ControllersBaseTest<FakePaymentsController>
    {
        protected readonly FakePaymentsController Controller;
        protected readonly DummyEmailService EmailService;
        protected FakePaymentsControllerBaseTest(string databaseName) : base(databaseName)
        {
            EmailService = new DummyEmailService();
            Controller = new FakePaymentsController(
                Db,
                Logger,
                new DummyPdfConverter(),
                new DummyBarcodeConverter(),
                EmailService,
                new DummyHttpClientFactory()
            );
        }
    }
}