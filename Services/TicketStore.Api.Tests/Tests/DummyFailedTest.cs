using System.Reflection.Metadata;
using Xunit;

namespace TicketStore.Api.Tests.Tests
{
    public class DummyFailedTest
    {
        [Fact]
        public void ItFails()
        {
            Assert.False(true);
        }
    }
}