using TicketStore.Api.Tests.Unit.Stubs;
using TicketStore.Api.Tests.Unit.Stubs.Http;
using TicketStore.Api.Tests.Unit.Tests.ModelTests.TicketPreview.Model;
using Moq;
using Microsoft.Extensions.Logging;
using TicketStore.Api.Model.Payment.YandexMoney;

namespace TicketStore.Api.Tests.Unit.Tests.ControllersTests.Payments
{
    public abstract class FakePaymentsControllerBaseTest : ControllersBaseTest<FakePaymentsController>
    {
        protected FakePaymentsController Controller;
        protected DummyEmailService EmailService;
        protected FakePaymentsControllerBaseTest(string databaseName) : base(databaseName)
        {
            EmailService = new DummyEmailService();
            Controller = new FakePaymentsController(
                Db,
                Logger,
                new DummyPdfConverter(),
                new DummyBarcodeConverter(),
                EmailService,
                new DummyHttpClientFactory(),
                new Stubs.DummyValidator(new Mock<ILogger<Validator>>().Object)
            );
        }
    }
}