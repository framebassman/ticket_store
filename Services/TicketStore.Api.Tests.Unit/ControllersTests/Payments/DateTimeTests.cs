using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ControllersTests.Payments
{
    public class DateTimeTests : PaymentsControllerBaseTest
    {
        private Char _longDash;
        private String _dateInString;
        public DateTimeTests() : base("date_time")
        {
            _longDash = '—';
            // UTC should be stored in Database
            var dbTime = new DateTime(2019, 10, 4, 16, 00, 00, DateTimeKind.Utc);
            _dateInString = "4 октября 2019";
            SeedTestData(dbTime);
        }

        [Fact]
        public void EventInDatabaseWithoutTimezone_ControllerReturnsDateInUtc()
        {
            // Arrange
            var concertLabel = $"{_concert.Artist} {_longDash} {_dateInString}";
            
            // Act
            var result = Controller.Post(
                false,
                "",
                "",
                0.0m,
                800.0m,
                "",
                DateTime.UtcNow,
                false,
                "test@test.test",
                "",
                _merchant.YandexMoneyAccount,
                false,
                concertLabel
            );
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.True(EmailService.IsExist("test@test.test"));
            var pdfs = EmailService.PdfList("test@test.test");
            Assert.Equal("Пятница, 4 октября 2019 г. 19:00", pdfs.First().ConcertTime());
        }
    }
}
