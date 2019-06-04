using System.Linq;
using System.Net;
using Xunit;
using TicketStore.Api.Tests.Tests.Fixtures;

namespace TicketStore.Api.Tests.Tests.Payments
{
    public class SendPayment : AbstractFixtureTest
    {
        public SendPayment(ApiFixture fixture) : base (fixture) {}

        [Fact]
        public void YandexSendPayment_ValidPayment_ReturnsOk()
        {
            // Arrange
            var before = Fixture.Db.Tickets.Count();
            
            // Act
            var response = Fixture.Api.SendPayment(299.00m, 300.00m);
            var after = Fixture.Db.Tickets.Count();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(before + 1, after);
        }

        [Fact]
        public void YandexSendPayment_InvalidPayment_ReturnsOk()
        {
            // Arrange
            var before = Fixture.Db.Tickets.Count();
            
            // Act
            var response = Fixture.Api.SendPayment(300.00m, 299.00m);
            var after = Fixture.Db.Tickets.Count();

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(before, after);
        }
    }
}
