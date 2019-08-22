using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TicketStore.Data.Model;
using Xunit;

namespace TicketStore.Web.Tests.Unit.ControllersTests.Events.Ordering
{
    public class EventsIsOrderedDescByDate : EventsControllerBaseTest
    {
        private readonly DateTime _closer;
        private readonly DateTime _farther;
        private readonly List<Event> _concertsToInsert;

        public EventsIsOrderedDescByDate() : base("Ordered_By_Date")
        {
            var merchant = Provider.Merchants().First();
            _closer = new DateTime(2018, 9, 4, 16, 00, 00, DateTimeKind.Utc);
            _farther = new DateTime(2019, 10, 4, 16, 00, 00, DateTimeKind.Utc);
            var oldConcert = Provider.Events(merchant).WithDate(_farther);
            var newConcert = Provider.Events(merchant).WithDate(_closer);
            _concertsToInsert = new List<Event> { newConcert, oldConcert };
            
            merchant = Db.Merchants.Add(merchant).Entity;
            oldConcert.Merchant = merchant;
            Db.Events.Add(oldConcert);
            newConcert.Merchant = merchant;
            Db.Events.Add(newConcert);
            Db.SaveChanges();
        }

        [Fact]
        public void GetEvents_ReturnInOrderDescByTime()
        {
            // Act
            var result = Controller.Get(1);
            
            // Assert
            Assert.IsType<OkObjectResult>(result);
            var value = (result as OkObjectResult).Value;
            Assert.IsType<List<Event>>(value);
            var events = (value as List<Event>);
            Assert.Equal(_concertsToInsert.Count, events.Count);
            var closerConcert = events[0];
            var fartherConcert = events[1];
            Assert.Equal(_closer, closerConcert.Time);
            Assert.Equal(_farther, fartherConcert.Time);
        }
    }
}