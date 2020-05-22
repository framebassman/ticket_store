using System;
using System.Linq;
using Moq;
using TicketStore.Data;
using TicketStore.Web.Model;
using TicketStore.Web.Model.Events;
using TicketStore.Web.Tests.Unit.BaseTest;
using Xunit;

namespace TicketStore.Web.Tests.Unit.ModelTests.EventsFinderTests
{
    public class FindOnlyNewEvents : DbBaseTest<EventsFinder>
    {
        private ApplicationContext _db;
        private EventsFinder _finder;
        private Mock<IDateTimeProvider> _dateTimeMock;
        private DateTime _oldDate;
        private DateTime _newDate;
        private DateTime _now;

        public FindOnlyNewEvents() : base("find_only_new_events")
        {
            _db = new ApplicationContext(Options);
            _dateTimeMock = new Mock<IDateTimeProvider>();
        }

        public override void Dispose()
        {
            _db.Dispose();
        }

        [Fact]
        public void TimeDiffMoreThan6Hours_ReturnsBothEvents()
        {
            // Arrange
            var diff = 7;
            var startHours = 16;
            SetupTestData(diff, startHours);
            
            // Act
            var result = _finder.Find(0, 100);
            
            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(_oldDate, result.First().Time);
            Assert.Equal(_newDate, result.Last().Time);
        }
        
        [Fact]
        public void TimeDiffEqualTo6Hours_ReturnsBothEvents()
        {
            // Arrange
            var diff = 6;
            var startHours = 16;
            SetupTestData(diff, startHours);
            
            // Act
            var result = _finder.Find(0, 100);
            
            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal(_oldDate, result.First().Time);
            Assert.Equal(_newDate, result.Last().Time);
        }

        [Fact]
        public void TimeDiffLessThan6Hours_ReturnsOnlyNewEvent()
        {
            // Arrange
            var diff = 5;
            var startHours = 16;
            SetupTestData(diff, startHours);
            
            // Act
            var result = _finder.Find(0, 100);
            
            // Assert
            Assert.Single(result);
            Assert.Equal(_newDate, result.First().Time);
        }

        private void SetupTestData(Int32 diff, Int32 startHours)
        {
            _oldDate = new DateTime(2018, 1, 1, startHours, 0, 0);
            _newDate = new DateTime(2018, 1, 2, startHours, 0, 0);
            _now = new DateTime(2018, 1, 1, startHours - diff, 0, 0);
            var merchant = _db.Merchants
                .Add(TestData.Merchants().First())
                .Entity;
            var oldConcert = TestData.Events(merchant).WithDate(_oldDate);
            var newConcert = TestData.Events(merchant).WithDate(_newDate);
            _db.Events.AddRange(oldConcert, newConcert);
            _db.SaveChanges();
            _dateTimeMock.Setup(mock => mock.Now).Returns(_now);
            _finder = new EventsFinder(_db, merchant.Id, _dateTimeMock.Object);
        }
    }
}
