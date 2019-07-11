using System.Net;
using Xunit;
using TicketStore.Api.Tests.Tests.Fixtures;

namespace TicketStore.Api.Tests.Tests.Payments
{
    [Collection("Api collection")]
    public class SendTestNotification
    {
        private readonly ApiFixture _fixture;

        public SendTestNotification(ApiFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void YandexSendTestRequest_ReturnTestMessage()
        {            
            // Act
            var response = _fixture.Api.SendTestPayment();
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("\"It's OK for yandex testing\"", response.Content);
        }
    }
}
