using System;
using TicketStore.Api.Model.Validation;
using TicketStore.Api.Tests.Unit.BaseTest;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ModelTests
{
    public class TicketFinderTest : DbBaseTest<ITicketFinder>
    {
        public TicketFinderTest() : base("ticket_finder") {
            // UTC should be stored in Database
            var dbTime = new DateTime(2019, 10, 4, 16, 00, 00, DateTimeKind.Utc);
            SeedTestData(dbTime);
        }

        [Fact]
        public void TicketDoesntExistInDatabase_ReturnsNull()
        {
            // Arrange
            var finder = new TicketFinder(Db, Logger);
            var barcode = new Barcode
            {
                code = "123",
                method = "Manual"
            };

            // Act
            var ticket = finder.Find(barcode);

            // Assert
            Assert.Null(ticket);
        }
    }
}
