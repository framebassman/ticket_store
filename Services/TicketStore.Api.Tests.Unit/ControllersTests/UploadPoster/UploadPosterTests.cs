using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicketStore.Api.Model.Poster;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ControllersTests.UploadPoster
{
    public class UploadPosterTests : UploadPosterControllerBaseTest
    {
        public UploadPosterTests() : base("poster")
        {
            // UTC should be stored in Database
            var dbTime = new DateTime(2019, 10, 4, 16, 00, 00, DateTimeKind.Utc);
            SeedTestData(dbTime);
        }

        [Fact]
        public void TicketIsNotFoundInDatabase_ReturnsBadRequest()
        {
            // Arrange
            var poster = new Poster
            {
                eventId = 1,
                imageUrl = "https://sun9-32.userapi.com/c852236/v852236322/17cdae/uHreFWeE3Sw.jpg"
            };
            
            // Act
            var result = Controller.Post(poster);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var json = JsonConvert.SerializeObject((result as OkObjectResult).Value);
            Assert.StartsWith("\"/9j/4AAQSkZJRgABAQEBLAEsAAD/2w", json);
        }
    }
}
