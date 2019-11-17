using System.Linq;
using System.Net;
using Xunit;
using NHamcrest;
using TicketStore.Api.Tests.Data;
using TicketStore.Api.Tests.Model;
using TicketStore.Api.Tests.Tests.Fixtures;
using TicketStore.Api.Tests.Tests.Matchers;

namespace TicketStore.Api.Tests.Tests.Payments
{
    [Collection("Api collection")]
    public class SendPayment
    {
        private readonly ApiFixture _fixture;
        public SendPayment(ApiFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void YandexSendPayment_InvalidPayment_ReturnsOk()
        {
            // Arrange
            var sender = _fixture.Merchant.YandexMoneyAccount;
            var testEvent = _fixture.Events[1];
            var email = Generator.Email();
            var before = _fixture.Db.Tickets.Count(t => t.Payment.Email == email);

            // Act
            var response = _fixture.Api.SendPayment(
                sender,
                new YandexPaymentLabel(testEvent), 
                email,
                testEvent.Roubles - 0.01m,
                testEvent.Roubles
            );

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            AssertWithTimeout.That(
                () => _fixture.Db.Tickets.Count(t => t.Payment.Email == email),
                Is.EqualTo(before)
            );
            AssertWithTimeout.That(() => _fixture.FakeSender.EmailsForAddress(email).Data.Count, Is.EqualTo(0));
        }

        [Fact]
        public void YandexSendPayment_ValidPayment_ReturnsOk()
        {
            // Arrange
            var sender = _fixture.Merchant.YandexMoneyAccount;
            var testEvent = _fixture.Events[0];
            var email = Generator.Email();
            var before = _fixture.Db.Tickets.Count(t => t.Payment.Email == email);

            // Act
            var response = _fixture.Api.SendPayment(
                sender,
                new YandexPaymentLabel(testEvent),
                email,
                testEvent.Roubles,
                testEvent.Roubles - 0.01m
            );

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            AssertWithTimeout.That(
                () => _fixture.Db.Tickets.Count(t => t.Payment.Email == email),
                Is.EqualTo(before + 1)
            );
            AssertWithTimeout.That(() => _fixture.FakeSender.EmailsForAddress(email).Data.Count, Is.EqualTo(1));
        }
    }
}
