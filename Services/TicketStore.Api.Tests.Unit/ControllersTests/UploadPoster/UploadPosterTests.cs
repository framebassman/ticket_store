using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
        public async void CanUploadPoster()
        {
            // Arrange
            var poster = new Poster
            {
                eventId = 1,
                imageUrl = "https://sun9-32.userapi.com/c852236/v852236322/17cdae/uHreFWeE3Sw.jpg"
            };

            // Act
            var result = await Controller.Post(poster);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var value = (result as OkObjectResult).Value;
            Assert.Equal("https://storage.yandexcloud.net/igor-test/g-u-i-d.jpg", value);

            var concert = Db.Events.FirstOrDefault(venue => venue.Id == poster.eventId);
            Assert.Equal("https://storage.yandexcloud.net/igor-test/g-u-i-d.jpg", concert.PosterUrl);
        }
    }
}
