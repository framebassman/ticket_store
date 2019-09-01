using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore;
using NHamcrest;
using TicketStore.Api.Tests.Data;
using TicketStore.Api.Tests.Model;
using TicketStore.Api.Tests.Model.Db;
using TicketStore.Api.Tests.Model.Services.Verify.Answers;
using TicketStore.Api.Tests.Tests.Fixtures;
using TicketStore.Api.Tests.Tests.Matchers;
using TicketStore.Api.Tests.Tests.Matchers.Tickets;
using Xunit;

namespace TicketStore.Api.Tests.Tests.Verification
{
    [Collection("Api collection")]
    public class VerifyValidTicketBarcode
    {
        private readonly ApiFixture _fixture;
        public VerifyValidTicketBarcode(ApiFixture fixture)
        {
            _fixture = fixture;
        }
        
        [Fact]
        public void SendExistBarcode_ReturnsOk()
        {
            // Arrange
            var sender = _fixture.Merchant.YandexMoneyAccount;
            var testEvent = _fixture.Events[0];
            var email = Generator.Email();
            _fixture.Api.SendPayment(sender, new YandexPaymentLabel(testEvent), email, testEvent.Roubles, testEvent.Roubles);
            var ticket = _fixture.Db.Tickets.First(t => t.Payment.Email == email);

            // Act
            var response = _fixture.Api.VerifyBarcode(ticket.Number);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            NHamcrest.XUnit.Assert.That(
                response.Content, 
                new NotUsed(
                    new WithConcert("First Test Artist — 9 июля 2019")
                )
            );

            AssertWithTimeout.That("Ticket should be expired",
                () => {
                    _fixture.Db.Entry(ticket).State = EntityState.Detached;
                    return _fixture.Db.Find<Ticket>(ticket.Id).Expired;
                },
                Is.True());
        }

        [Fact]
        public void SendNotExistBarcode_ReturnsNotFound()
        {
            // Act
            var response = _fixture.Api.VerifyBarcode("-1");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(new NotFoundAnswer().ToString(), response.Content);
        }
    }
}
