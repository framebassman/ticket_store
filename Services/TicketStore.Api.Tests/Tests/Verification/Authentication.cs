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
    [Collection("Api collection")]
    public class Authentication
    {
        private readonly ApiFixture _fixture;
        public Authentication(ApiFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void SendBarcode_WithoutBearerToken_ReturnsUnauthorized()
        {
            // Arrange
            var sender = _fixture.Merchant.YandexMoneyAccount;
            var testEvent = _fixture.Events[0];
            var email = Generator.Email();
            _fixture.Api.SendPayment(sender, new YandexPaymentLabel(testEvent), email, testEvent.Roubles, testEvent.Roubles);
            var ticket = _fixture.Db.Tickets.First(t => t.Payment.Email == email);

            // Act
            var response = _fixture.Api.VerifyBarcodeWithoutAuth(ticket.Number);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal(new UnauthorizedAnswer().ToString(), response.Content);
            AssertWithTimeout.That(() => _fixture.Db.Tickets.First(t => t.Payment.Email == email).Expired, Is.False());
        }
    }
}
