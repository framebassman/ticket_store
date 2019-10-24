using System;
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

        [Fact]
        public void Some()
        {
            var inUtc = DateTime.UtcNow;
            // var events = _db.Events
            //     .Where(e =>
            //         e.MerchantId == 4
            //         && e.Time - DateTime.Now >= TimeSpan.FromHours(6) 
            //     )
            //     .OrderBy(e => e.Time)
            //     .ToList();
        }
    }
}
