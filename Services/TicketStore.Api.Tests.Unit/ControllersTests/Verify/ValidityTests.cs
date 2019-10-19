using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TicketStore.Api.Tests.Unit.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ControllersTests.Verify
{
    public class ValidityTests : VerifyControllerBaseTest
    {
        private JsonSerializerOptions serializerOptions;

        public ValidityTests() : base("validity")
        {
            serializerOptions = new JsonSerializerOptions();
            serializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            // UTC should be stored in Database
            var dbTime = new DateTime(2019, 10, 4, 16, 00, 00, DateTimeKind.Utc);
            SeedTestData(dbTime);
        }

        [Fact]
        public void TicketIsNotFoundInDatabase_ReturnsBadRequest()
        {
            // Arrange
            var turnstileScan = new ManualTurnstileScan("123");
            
            // Act
            var result = Controller.Post(turnstileScan);
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var json = JsonSerializer.Serialize((result as BadRequestObjectResult).Value, serializerOptions);
            Assert.Equal("{\"message\":\"cannot find code in database\"}", json);
        }

        [Fact]
        public void TicketDoesntMatchAnyConcert_ReturnsBadRequest()
        {
            // Arrange
            var turnstileScan = new ManualTurnstileScan("5555566666");
            
            // Act
            var result = Controller.Post(turnstileScan);
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var json = JsonSerializer.Serialize((result as BadRequestObjectResult).Value, serializerOptions);
            Assert.Equal("{\"message\":\"no concert found for this ticket\"}", json);
        }

        [Fact]
        public void TicketHasExpired_ReturnsOk()
        {
            // Arrange
            var turnstileScan = new ManualTurnstileScan("3333344444");
            
            // Act
            var result = Controller.Post(turnstileScan);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var json = JsonSerializer.Serialize((result as OkObjectResult).Value, serializerOptions);
            Assert.Equal("{\"concertLabel\":\"Test artist — 4 октября 2019\",\"used\":true,\"message\":\"OK\"}", json);
        }

        [Fact]
        public void TicketIsValid_ReturnsOk()
        {
            // Arrange
            var turnstileScan = new ManualTurnstileScan("1111122222");
            
            // Act
            var result = Controller.Post(turnstileScan);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var json = JsonSerializer.Serialize((result as OkObjectResult).Value, serializerOptions);
            Assert.Equal("{\"concertLabel\":\"Test artist — 4 октября 2019\",\"used\":false,\"message\":\"OK\"}", json);
        }
    }
}
