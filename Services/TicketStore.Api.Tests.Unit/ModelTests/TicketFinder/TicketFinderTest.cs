using System;
using TicketStore.Api.Model.Validation;
using TicketStore.Api.Tests.Unit.BaseTest;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ModelTests
{
    public class TicketFinderTest : DbBaseTest<ITicketFinder>
    {
        protected TicketFinder Finder;
        public TicketFinderTest() : base("ticket_finder") {
            // UTC should be stored in Database
            var dbTime = new DateTime(2019, 10, 4, 16, 00, 00, DateTimeKind.Utc);
            SeedTestData(dbTime);
            Finder = new TicketFinder(Db, Logger);
        }

        [Fact]
        public void InvalidVeificationMethod_ThrowsException()
        {
            var barcode = new Barcode
            {
                code = "123",
            };

            var ex = Assert.Throws<Exception>(() => Finder.Find(barcode));

            Assert.Equal("Verification method doesn't exist: ", ex.Message);
        }

        [Fact]
        public void ManualVerificationMethod_NoTicket_ThrowsException()
        {
            var barcode = new Barcode
            {
                code = "123",
                method = "Manual"
            };

            var ex = Assert.Throws<Exception>(() => Finder.Find(barcode));

            Assert.Equal("Ticket not found in Database", ex.Message);
        }

        [Fact]
        public void ManualVerificationMethod_TicketExist_ThrowsException()
        {
            var barcode = new Barcode
            {
                code = "1111122222",
                method = "Manual"
            };

            var ticket = Finder.Find(barcode);

            Assert.Equal("1111122222", ticket.Number);
        }
    }
}
