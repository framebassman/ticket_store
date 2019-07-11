using System.Linq;
using System.Net;
using NHamcrest;
using TicketStore.Api.Tests.Data;
using TicketStore.Api.Tests.Model;
using TicketStore.Api.Tests.Model.Services.Verify.Answers;
using TicketStore.Api.Tests.Tests.Fixtures;
using TicketStore.Api.Tests.Tests.Matchers;
using Xunit;

namespace TicketStore.Api.Tests.Tests.Verification
{
    public class Authentication : AbstractFixtureTest
    {
        public Authentication(ApiFixture fixture) : base(fixture) { }

        [Fact]
        public void SendBarcode_WithoutBearerToken_ReturnsUnauthorized()
        {
            // Arrange
            var sender = Merchant.YandexMoneyAccount;
            var testEvent = Events[0];
            var email = Generator.Email();
            Fixture.Api.SendPayment(sender, new YandexPaymentLabel(testEvent), email, testEvent.Roubles, testEvent.Roubles);
            var ticket = Fixture.Db.Tickets.First(t => t.Payment.Email == email);

            // Act
            var response = Fixture.Api.VerifyBarcodeWithoutAuth(ticket.Number);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal(new UnauthorizedAnswer().ToString(), response.Content);
            AssertWithTimeout.That(() => Fixture.Db.Tickets.First(t => t.Payment.Email == email).Expired, Is.False());
        }
    }
}
