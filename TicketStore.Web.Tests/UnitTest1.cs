using System;
using Xunit;

namespace TicketStore.Web.Tests
{
    public class UnitTest1 : IClassFixture<UiTestFixture>
    {
        private UiTestFixture _fixture;
        
        public UnitTest1()
        {
            _fixture = new UiTestFixture();
        }
        
        [Fact]
        public void Test1()
        {
            _fixture.Browser().Navigate().GoToUrl("http://localhost:5000");
            int a = 1;
        }
    }
}
