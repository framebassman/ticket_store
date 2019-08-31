using System;
using TicketStore.Api.Controllers;
using TicketStore.Data;

namespace TicketStore.Api.Tests.Unit.ControllersTests.Verify
{
    public abstract class VerifyControllerBaseTest : ControllersBaseTest<VerifyController>, IDisposable
    {
        protected readonly ApplicationContext Db;
        protected readonly VerifyController Controller;
        protected VerifyControllerBaseTest(string databaseName) : base(databaseName)
        {
            Db = new ApplicationContext(Options);
            Controller = new VerifyController(Db, Logger);
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}
