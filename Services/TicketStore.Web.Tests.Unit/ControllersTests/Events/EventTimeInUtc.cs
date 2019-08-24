using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
            // Arrange
            var merchantId = Db.Merchants
                .First(m => m.YandexMoneyAccount == _merchant.YandexMoneyAccount)
                .Id;
            
            // Act
            var result = Controller.Get(merchantId);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var json = JsonConvert.SerializeObject((result as OkObjectResult).Value);
            Assert.Contains($"\"Time\":\"{_dateTimeInString}\"", json);
        }
        
        [Fact]
        public void GetEvents_ReturnInOrderDescByTime()
        {
            // Arrange
            var merchant = new Merchant{ YandexMoneyAccount = "123456789", Place = "Test merchant" };
            var closer = new DateTime(2018, 9, 4, 16, 00, 00, DateTimeKind.Utc);
            var farther = new DateTime(2019, 10, 4, 16, 00, 00, DateTimeKind.Utc);
            var concertsToInsert = ArrangeEventsOrderingTest(merchant, closer, farther);

            var merchantId = Db.Merchants
                .First(m => m.YandexMoneyAccount == merchant.YandexMoneyAccount)
                .Id;
            
            // Act
            var result = Controller.Get(merchantId);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var value = (result as OkObjectResult).Value;
            Assert.IsType<List<Event>>(value);
            var events = value as List<Event>;
            Assert.Equal(concertsToInsert.Count, events.Count);
            var closerConcert = events[0];
            var fartherConcert = events[1];
            Assert.Equal(closer, closerConcert.Time);
            Assert.Equal(farther, fartherConcert.Time);
        }

        private List<Event> ArrangeEventsOrderingTest(Merchant merchant, DateTime closer, DateTime farther)
        {
            var oldConcert = Provider.Events(merchant).WithDate(farther);
            var newConcert = Provider.Events(merchant).WithDate(closer);
            var concertsToInsert = new List<Event> { newConcert, oldConcert };
            
            merchant = Db.Merchants.Add(merchant).Entity;
            oldConcert.Merchant = merchant;
            Db.Events.Add(oldConcert);
            newConcert.Merchant = merchant;
            Db.Events.Add(newConcert);
            Db.SaveChanges();
            return concertsToInsert;
        }
    }
}
