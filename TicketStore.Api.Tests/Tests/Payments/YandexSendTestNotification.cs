using System;
using System.Net;
using RestSharp;
using Xunit;

namespace TicketStore.Api.Tests.Tests.Payments
{
    public class YandexSendTestNotification
    {
        private readonly RestClient _client;
        
        public YandexSendTestNotification()
        {
            var dockerHost = DockerHost();
            _client = new RestClient($"http://{dockerHost}:3000");
        }

        private String DockerHost()
        {
            var variable = Environment.GetEnvironmentVariable("DOCKER_HOST");
            if (String.IsNullOrEmpty(variable))
            {
                return "localhost";
            }
            else
            {
                return new UriBuilder(variable).Host;
            }
        }
        
        [Fact]
        public void SendTestRequest_ReturnTestMessage()
        {
            // Arrange
            var request = new RestRequest("api/payments", Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("test_notification", true);
            
            // Act
            var response = _client.Execute(request);
            
            // Assert
            Assert.Equal(HttpStatusCode.OK,response.StatusCode);
            Assert.Equal("\"It's OK for yandex testing\"", response.Content);
        }
    }
}
