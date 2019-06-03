using System;
using System.Net;
using RestSharp;
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
            // Act
            var response = Fixture.Api.SendPayment(199.00m, 200.00m);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void YandexSendPayment_InvalidPayment_ReturnsOk()
        {
            // Act
            var response = Fixture.Api.SendPayment(200.00m, 199.00m);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
