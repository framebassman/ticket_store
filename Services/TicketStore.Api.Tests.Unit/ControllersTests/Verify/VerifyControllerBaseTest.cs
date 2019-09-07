using Microsoft.Extensions.Logging;
using Moq;
using TicketStore.Api.Controllers;
using TicketStore.Api.Model.Validation;

namespace TicketStore.Api.Tests.Unit.ControllersTests.Verify
{
    public abstract class VerifyControllerBaseTest : ControllersBaseTest<VerifyController>
    {
        protected readonly VerifyController Controller;
        protected VerifyControllerBaseTest(string databaseName) : base(databaseName)
        {
            var finder = new TicketFinder(Db, new Mock<ILogger<TicketFinder>>().Object, new Mock<IDateTimeProvider>().Object);
            Controller = new VerifyController(Db, Logger, finder);
        }
    }
}
