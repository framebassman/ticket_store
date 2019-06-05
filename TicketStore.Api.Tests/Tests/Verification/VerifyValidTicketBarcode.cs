using System.Linq;
using System.Net;
using NHamcrest;
using TicketStore.Api.Tests.Tests.Fixtures;
using TicketStore.Api.Tests.Tests.Matchers;
using Xunit;

namespace TicketStore.Api.Tests.Tests.Verification
{
    public class VerifyValidTicketBarcode : AbstractFixtureTest
    {
        public VerifyValidTicketBarcode(ApiFixture fixture) : base (fixture) {}
        
        [Fact]
        public void SendExistBarcode_ReturnsOk()
        {
            // Arrange
            var email = "test2@test.test";
            Fixture.Api.SendPayment(email, 300.00m, 300.00m);
            var ticket = Fixture.Db.Tickets.First(t => t.Payment.Email == email);

            // Act
            var response = Fixture.Api.VerifyBarcode(ticket.Number);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("OK", response.Content);
            AssertWithTimeout.That(() => Fixture.Db.Tickets.First(t => t.Payment.Email == email).Expired, Is.True());
        }

        [Fact]
        public void SendNotExistBarcode_ReturnsNotFound()
        {
            // Act
            var response = Fixture.Api.VerifyBarcode("-1");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal("cannot find code in database", response.Content);
        }
    }
}