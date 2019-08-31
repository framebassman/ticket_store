using System;
using TicketStore.Api.Controllers;

namespace TicketStore.Api.Tests.Unit.ControllersTests.Verify
{
    public abstract class VerifyControllerBaseTest : ControllersBaseTest<VerifyController>, IDisposable
    {
        protected readonly VerifyController Controller;
        protected VerifyControllerBaseTest(string databaseName) : base(databaseName)
        {
            Controller = new VerifyController(Db, Logger);
        }
    }
}
