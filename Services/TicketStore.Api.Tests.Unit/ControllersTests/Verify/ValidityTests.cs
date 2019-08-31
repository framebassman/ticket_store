using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicketStore.Api.Model.Validation;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ControllersTests.Verify
{
    public class ValidityTests : VerifyControllerBaseTest
    {
        private String _dateInString;
        public ValidityTests() : base("validity")
        {
            // UTC should be stored in Database
            var dbTime = new DateTime(2019, 10, 4, 16, 00, 00, DateTimeKind.Utc);
            _dateInString = "4 октября 2019";
            SeedTestData(dbTime);
        }

        [Fact]
        public void TicketIsNotFoundInDatabase_ReturnsBadRequest()
        {
            // Arrange
            var barcode = new Barcode {
                code = "123"
            };
            
            // Act
            var result = Controller.Post(barcode);
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var json = JsonConvert.SerializeObject((result as BadRequestObjectResult).Value);
            Assert.Equal("{\"message\":\"cannot find code in database\"}", json);
        }

        [Fact]
        public void TicketHasExpired_ReturnsBadRequest()
        {
            // Arrange
            var barcode = new Barcode {
                code = "3333344444"
            };
            
            // Act
            var result = Controller.Post(barcode);
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var json = JsonConvert.SerializeObject((result as BadRequestObjectResult).Value);
            Assert.Equal("{\"message\":\"ticket has already verified\"}", json);
        }

        [Fact]
        public void TicketDoesntMatchAnyConcert_ReturnsBadRequest()
        {
            // Arrange
            var barcode = new Barcode {
                code = "5555566666"
            };
            
            // Act
            var result = Controller.Post(barcode);
            
            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var json = JsonConvert.SerializeObject((result as BadRequestObjectResult).Value);
            Assert.Equal("{\"message\":\"no concert found for this ticket\"}", json);
        }

        [Fact]
        public void TicketIsValid_ReturnsOk()
        {
            // Arrange
            var barcode = new Barcode {
                code = "1111122222"
            };
            
            // Act
            var result = Controller.Post(barcode);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var json = JsonConvert.SerializeObject((result as OkObjectResult).Value);
            Assert.Equal("{\"message\":\"OK\"}", json);
        }
    }
}
