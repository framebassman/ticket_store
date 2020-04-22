using TicketStore.Api.Tests.Unit.ModelTests.TicketPreview.Model;
using TicketStore.Api.Tests.Unit.Stubs;
using TicketStore.Api.Tests.Unit.Stubs.Http;

namespace TicketStore.Api.Tests.Unit.ControllersTests.Payments
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