using TicketStore.Api.Controllers;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ControllersTests.Payments
{
    public class DateTimeTests : ControllersBaseTest<PaymentsController>
    {
        public DateTimeTests() : base("date_time") { }

        [Fact]
        public void Some()
        {
            Assert.Equal(1, 1);
        }
    }
}