using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TicketStore.Data.Model;
using TicketStore.Web.Model;
using TicketStore.Web.Controllers;
using TicketStore.Web.Tests.Unit.Model;
using Xunit;
using Moq;

namespace TicketStore.Web.Tests.Unit.ControllersTests.Events
{
    public class EventTimeInUtc : EventsControllerBaseTest
    {
        private readonly EventsController _controller;
        private Merchant _merchant;
        private Event _concert;
        private DateTime _closer;
        private DateTime _farther;
        private String _dateTimeInString;
        
        public EventTimeInUtc() : base("EventTimeInUtc")
        {
            var now = new DateTime(2018, 9, 3, 16, 00, 00, DateTimeKind.Utc);
            _closer = now + TimeSpan.FromDays(1);
            _farther = now + TimeSpan.FromDays(2);
            var dbTime = new DateTime(2018, 9, 5, 16, 00, 00, DateTimeKind.Utc);
            _dateTimeInString = "2018-09-05T16:00:00Z";
            var dateTimeMock = new Mock<IDateTimeProvider>();
            dateTimeMock.Setup(mock => mock.Now).Returns(now);
            _controller = new EventsController(Logger, Db, dateTimeMock.Object);
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
            var result = _controller.Get(merchantId);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var json = Serializer.ToJson((result as OkObjectResult).Value);
            Assert.Contains($"\"Time\":\"{_dateTimeInString}\"", json);
        }
        
        [Fact]
        public void GetEvents_ReturnInOrderDescByTime()
        {
            // Arrange
            var merchant = new Merchant{ YandexMoneyAccount = "123456789", Place = "Test merchant" };
            var concertsToInsert = ArrangeEventsOrderingTest(merchant, _closer, _farther);

            var merchantId = Db.Merchants
                .First(m => m.YandexMoneyAccount == merchant.YandexMoneyAccount)
                .Id;
            
            // Act
            var result = _controller.Get(merchantId);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var value = (result as OkObjectResult).Value;
            Assert.IsType<List<Event>>(value);
            var events = value as List<Event>;
            Assert.Equal(concertsToInsert.Count, events.Count);
            var closerConcert = events[0];
            var fartherConcert = events[1];
            Assert.Equal(_closer, closerConcert.Time);
            Assert.Equal(_farther, fartherConcert.Time);
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
