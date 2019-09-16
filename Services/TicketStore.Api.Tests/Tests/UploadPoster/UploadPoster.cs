using System.Net;
using TicketStore.Api.Tests.Model.Services.UploadPoster;
using TicketStore.Api.Tests.Model.Services.Verify.Answers;
using TicketStore.Api.Tests.Tests.Fixtures;
using Xunit;

namespace TicketStore.Api.Tests.Tests.Verification
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
        public void FailWhenWrongImageProvided()
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
        public void FailWhenNoEventExist()
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
        public void Sucess()
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
            // Assert.Equal(new FailedUploadAnswer().ToString(), response.Content);
        }
    }
}
