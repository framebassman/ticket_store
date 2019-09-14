using TicketStore.Api.Controllers;

namespace TicketStore.Api.Tests.Unit.ControllersTests.UploadPoster
{
    public abstract class UploadPosterControllerBaseTest : ControllersBaseTest<VerifyController>
    {
        protected readonly UploadPosterController Controller;
        protected UploadPosterControllerBaseTest(string databaseName) : base(databaseName)
        {
            Controller = new UploadPosterController(Db, Logger);
        }
    }
}
