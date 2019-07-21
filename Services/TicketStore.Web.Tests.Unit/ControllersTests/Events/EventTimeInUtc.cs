using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicketStore.Data.Model;
using Xunit;

namespace TicketStore.Web.Tests.Unit.ControllersTests.Events
{
    public class EventTimeInUtc : EventsControllerBaseTest
    {
        private Merchant _merchant;
        private Event _concert;
        private String _dateTimeInString;
        
        public EventTimeInUtc() : base("EventTimeInUtc")
        {
            var dbTime = new DateTime(2019, 10, 4, 16, 00, 00, DateTimeKind.Utc);
            _dateTimeInString = "2019-10-04T16:00:00Z";
            SeedTestData(dbTime);
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

        [Fact]
        public void ThereAreEvents_ControllerReturnsTimeInUtcAndIso()
        {
            // Act
            var result = Controller.Get(1);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var json = JsonConvert.SerializeObject((result as OkObjectResult).Value);
            Assert.Contains($"\"Time\":\"{_dateTimeInString}\"", json);
        }
    }
}
