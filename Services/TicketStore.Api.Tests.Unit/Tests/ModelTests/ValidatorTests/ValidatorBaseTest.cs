using System;
using TicketStore.Api.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.Tests.ModelTests.ValidatorTests
{
    public class ValidatorBaseTest
    {
        [Fact]
        public void FromYandexExample_ShouldBeFromYandex()
        {
            var validator = new Validator();
            Assert.True(validator.FromYandex("p2p-incoming", "1234567", new Decimal(300.00), "643",
                DateTime.Parse("2011-07-01T09:00:00.000+04:00"), "41001XXXXXXXX",  false, "01234567890ABCDEF01234567890", "YM.label.12345",
                "a2ee4a9195f4a90e893cff4f62eeba0b662321f9"));
        }
    }
}
