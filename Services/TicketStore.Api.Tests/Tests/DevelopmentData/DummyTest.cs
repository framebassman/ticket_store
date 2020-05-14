using System;
using TicketStore.Api.Tests.Data;
using Xunit;
using Xunit.Abstractions;

namespace TicketStore.Api.Tests.Tests.DevelopmentData
{
    public class DummyTest
    {
        [Trait("Category", "Dummy")]
        [Fact]
        public void HelloWorld()
        {
            Assert.Equal(1, 1);
        }
    }
}
