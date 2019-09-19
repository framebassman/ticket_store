using System.Linq;
using System.Net;
using NHamcrest;
using TicketStore.Api.Tests.Model.Services.UploadPoster;
using TicketStore.Api.Tests.Model.Services.Verify.Answers;
using TicketStore.Api.Tests.Tests.Fixtures;
using TicketStore.Api.Tests.Tests.Matchers;
using Xunit;

namespace TicketStore.Api.Tests.Tests.Uploads
{
    [Collection("Api collection")]
    public class UploadPoster
    {
        private readonly ApiFixture _fixture;
        public UploadPoster(ApiFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void WrongImageProvided_ShouldReturnsError()
        {
            // Arrange
            var scan = new Poster
            {
                eventId = 1,
                imageUrl = "qwe"
            };
            
            // Act
            var response = _fixture.Api.UploadPoster(scan);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(new FailedUploadAnswer().ToString(), response.Content);
        }

        [Fact]
        public void EventNotExist_ShouldReturnsError()
        {
            // Arrange
            var scan = new Poster
            {
                eventId = -1,
                imageUrl = "https://sun9-32.userapi.com/c852236/v852236322/17cdae/uHreFWeE3Sw.jpg"
            };
            
            // Act
            var response = _fixture.Api.UploadPoster(scan);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Equal(new FailedUploadAnswer().ToString(), response.Content);
        }

        [Fact]
        public void EventExist_ImageExist_ShouldUpdatePoster()
        {
            // Arrange
            var scan = new Poster
            {
                eventId = 1,
                imageUrl = "https://sun9-32.userapi.com/c852236/v852236322/17cdae/uHreFWeE3Sw.jpg"
            };
            
            // Act
            var response = _fixture.Api.UploadPoster(scan);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            AssertWithTimeout.That(
                "Poster should be updated to not null value",
                () =>
                {
                    var concert = _fixture.Db.Events.FirstOrDefault(e => e.Id == scan.eventId);
                    if (concert == null)
                    {
                        return null;
                    }
                    else
                    {
                        return concert.PosterUrl;                    
                    }                    
                },
                Is.NotNull());
        }
    }
}
