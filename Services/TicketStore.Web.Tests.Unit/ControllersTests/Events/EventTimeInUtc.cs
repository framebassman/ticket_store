using Xunit;

namespace TicketStore.Web.Tests.Unit.ControllersTests.Events
{
    public class EventTimeInUtc : EventsControllerBaseTest
    {
        public EventTimeInUtc() : base("EventTimeInUtc")
        {
        }

        [Fact]
        public void Some()
        {
            Assert.Equal(1, 1);
        }
    }
}
