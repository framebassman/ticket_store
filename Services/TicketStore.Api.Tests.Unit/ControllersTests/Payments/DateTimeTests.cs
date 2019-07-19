using System;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketStore.Api.Controllers;
using TicketStore.Api.Model.Email;
using TicketStore.Api.Tests.Unit.Stubs;
using TicketStore.Data;
using TicketStore.Data.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ControllersTests.Payments
{
    public class DateTimeTests : PaymentsControllerBaseTest
    {
        private Merchant _merchant;
        private Event _concert;
        private Char _longDash;
        private String _dateInString;
        public DateTimeTests() : base("date_time")
        {
            _longDash = '—';
            // UTC should be stored in Database
            var dbTime = new DateTime(2019, 10, 4, 16, 00, 00);
            _dateInString = "4 октября 2019";
            SeedTestData(dbTime);
        }

        [Fact]
        public void Some()
        {
            Assert.Equal(1, 1);
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
            var pdf = EmailService.Pdf("test@test.test");
            Assert.Equal("Пятница, 4 октября 2019 г. 19:00", pdf.Time());
        }

        private void SeedTestData(DateTime date)
        {
            var merchant = Provider.Merchants().First();
            var concert = Provider.Events(merchant).WithDate(date);
            _merchant = Db.Merchants.Add(merchant).Entity;
            concert.Merchant = _merchant;
            _concert = Db.Events.Add(concert).Entity;
            Db.SaveChanges();
        }
    }
}