using System;
using System.Linq;
using System.Net;
using NHamcrest;
using TicketStore.Api.Tests.Data;
using TicketStore.Api.Tests.Model;
using TicketStore.Api.Tests.Model.Services.Verify.Answers;
using TicketStore.Api.Tests.Model.Services.Verify.Requests;
using TicketStore.Api.Tests.Tests.Fixtures;
using TicketStore.Api.Tests.Tests.Matchers;
using Xunit;
using Xunit.Abstractions;

namespace TicketStore.Api.Tests.Tests.Verification
{
    [Collection("Api collection")]
    public class Authentication
    {
        private ApiFixture _fixture;
        private ITestOutputHelper _logger;
        public Authentication(ApiFixture fixture, ITestOutputHelper logger)
        {
            _fixture = fixture;
            _logger = logger;
        }

        [Fact]
        public void SendBarcode_WithoutBearerToken_ReturnsUnauthorized()
        {
            // Arrange
            var sender = _fixture.Merchant.YandexMoneyAccount;
            var testEvent = _fixture.Events[0];
            var email = Generator.Email();
            _logger.WriteLine("Send a Payment Request with the following email: " + email);
            var response = _fixture.Api.SendPayment(sender, new YandexPaymentLabel(testEvent), email, testEvent.Roubles, testEvent.Roubles);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var ticket = _fixture.Db.Tickets.First(t => t.Payment.Email == email);
            _logger.WriteLine("The first ticket in database with " + email + " email was: " + ticket.Number);
            _logger.WriteLine("Now is: " + DateTime.UtcNow.ToString());
            var scan = new ManualScan(ticket.Number);

            // Act
            response = _fixture.Api.VerifyBarcodeWithoutAuth(scan);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.Equal(new UnauthorizedAnswer().ToString(), response.Content);
            AssertWithTimeout.That(() => _fixture.Db.Tickets.First(t => t.Payment.Email == email).Expired, Is.False());
        }
    }
}
