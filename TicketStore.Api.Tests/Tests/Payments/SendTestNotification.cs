using System;
using System.Net;
using RestSharp;
using Xunit;
using TicketStore.Api.Tests.Tests.Fixtures;

namespace TicketStore.Api.Tests.Tests.Payments
{
    public class SendTestNotification : AbstractFixtureTest
    {
        public SendTestNotification(ApiFixture fixture) : base(fixture) {}

        [Fact]
        public void YandexSendTestRequest_ReturnTestMessage()
        {            
            // Act
            var response = Fixture.Api.SendTestPayment();
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("\"It's OK for yandex testing\"", response.Content);
        }
    }
}
