using System;
using Microsoft.AspNetCore.Mvc;
using TicketStore.Api.Tests.Unit.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.Tests.ControllersTests.Uploads.Poster
{
    public class UploadsTests : UploadsControllerBaseTest
    {
        public UploadsTests() : base("poster")
        {
            // UTC should be stored in Database
            var dbTime = new DateTime(2019, 10, 4, 16, 00, 00, DateTimeKind.Utc);
            SeedTestData(dbTime);
        }

        [Fact]
        public async void EventNotExist_ShouldReturnsError()
        {
            // Arrange
            var poster = new Api.Model.Poster.Poster
            {
                eventId = 0,
                imageUrl = "https://sun9-32.userapi.com/c852236/v852236322/17cdae/uHreFWeE3Sw.jpg"
            };

            // Act
            var result = await Controller.UpdatePoster(poster);
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var json = Serializer.ToJson((result as BadRequestObjectResult).Value);
            Assert.Equal("{\"message\":\"Failed to update poster\"}", json);
        }
    }
}
