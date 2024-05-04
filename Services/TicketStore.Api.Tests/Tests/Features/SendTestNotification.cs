using System.Net;
using TicketStore.Api.Tests.Tests.Fixtures;
using Xunit;

namespace TicketStore.Api.Tests.Tests.Features
{
    [Collection("Api collection")]
    public class SendTestNotification
    {
        private ApiFixture _fixture;

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
