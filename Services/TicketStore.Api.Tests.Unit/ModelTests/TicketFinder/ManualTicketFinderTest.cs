using System;
using TicketStore.Api.Model.Validation;
using TicketStore.Api.Tests.Unit.BaseTest;
using TicketStore.Api.Tests.Unit.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ModelTests
{
    public class ManualTicketFinderTest : DbBaseTest<ITicketFinder>
    {
        protected ITicketFinder Finder;
        public ManualTicketFinderTest() : base("manual_ticket_finder") {
            // UTC should be stored in Database
            var dbTime = new DateTime(2019, 10, 7, 16, 00, 00, DateTimeKind.Utc);
            SeedTestData(dbTime);
            SetupFinder();
        }

        [Fact]
        public void ManualVerificationMethod_TicketNotFound()
        {
            var turnstileScan = new ManualTurnstileScan("123");

            var ex = Assert.Throws<Exception>(() => Finder.Find(turnstileScan));

            Assert.Equal("Method: Manual. Ticket not found in Database", ex.Message);
        }

        [Fact]
        public void ManualVerificationMethod_TicketExist()
        {
            var turnstileScan = new ManualTurnstileScan("1111122222");

            var ticket = Finder.Find(turnstileScan);

            Assert.Equal("1111122222", ticket.Number);
        }

        protected void SetupFinder()
        {
            Finder = new ManualTicketFinder(Db);
        }
    }
}
