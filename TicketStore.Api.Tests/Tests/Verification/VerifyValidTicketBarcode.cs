using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore;
using NHamcrest;
using TicketStore.Api.Tests.Model;
using TicketStore.Api.Tests.Model.Services.Verify.Answers;
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
            var email = "test4@test.test";
            Fixture.Api.SendPayment(email, 300.00m, 300.00m);
            var ticket = Fixture.Db.Tickets.First(t => t.Payment.Email == email);

            // Act
            var response = Fixture.Api.VerifyBarcode(ticket.Number);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(new OkAnswer().ToString(), response.Content);

            AssertWithTimeout.That("Ticket should be expired",
                () =>
                {
                    Fixture.Db.Entry(ticket).State = EntityState.Detached;
                    return Fixture.Db.Find<Ticket>(ticket.Id).Expired;
                }, Is.False());
        }

        [Fact]
        public void SendNotExistBarcode_ReturnsNotFound()
        {
            // Act
            var response = Fixture.Api.VerifyBarcode("-1");

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(new NotFoundAnswer().ToString(), response.Content);
        }
    }
}