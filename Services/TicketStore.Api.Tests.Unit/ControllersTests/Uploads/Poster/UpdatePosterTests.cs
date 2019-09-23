using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ControllersTests.Uploads.Poster
{
    public class UploadsTests : UploadsControllerBaseTest
    {
        public UploadsTests() : base("poster")
        {
            // UTC should be stored in Database
            var dbTime = new DateTime(2019, 10, 4, 16, 00, 00, DateTimeKind.Utc);
            SeedTestData(dbTime);
        }

       [Fact(Skip = "Suggest to remove this test, because it push picture to real yandex storage")]
        // [Fact]
        public async void UpdatePosterSuccessfully()
        {
            // Arrange
            var poster = new Api.Model.Poster.Poster
            {
                eventId = 1,
                imageUrl = "https://sun9-32.userapi.com/c852236/v852236322/17cdae/uHreFWeE3Sw.jpg"
            };

            // Act
            var result = await Controller.UpdatePoster(poster);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var json = JsonConvert.SerializeObject((result as OkObjectResult).Value);
            Assert.Equal("{\"imageUrl\":\"https://storage.yandexcloud.net/igor-test/g-u-i-d.jpg\",\"message\":\"OK\"}", json);

            var concert = Db.Events.FirstOrDefault(venue => venue.Id == poster.eventId);
            Assert.Equal("https://storage.yandexcloud.net/igor-test/g-u-i-d.jpg", concert.PosterUrl);
        }

        [Fact(Skip = "Suggest to remove this test, because it push picture to real yandex storage")]
        public async void FailToUpdatePoster()
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
            var json = JsonConvert.SerializeObject((result as BadRequestObjectResult).Value);
            Assert.Equal("{\"message\":\"Failed to update poster\"}", json);
        }
    }
}
