using System;
using System.Globalization;
using System.Linq;
using Xunit;
using TicketStore.Api.Tests.Data;

namespace TicketStore.Api.Tests.Tests
{
    public class EventsTest
    {
        private ApplicationContext _db;

        public EventsTest()
        {
            _db = new ApplicationContext();
        }

        // [Fact]
        // public void Some()
        // {
        //     var inUtc = DateTime.UtcNow;
        //     var result = inUtc.ToString("d MMMM yyyy", CultureInfo.CreateSpecificCulture("ru"));
        //     Assert.Equal("24 октября 2019", result);
        // }
    }
}
