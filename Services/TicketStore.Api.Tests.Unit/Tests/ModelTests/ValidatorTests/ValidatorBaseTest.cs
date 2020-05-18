using System;
using System.Globalization;
using TicketStore.Api.Model;
using Xunit;
using Xunit.Abstractions;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TicketStore.Api.Model.Payment.YandexMoney;

namespace TicketStore.Api.Tests.Unit.Tests.ModelTests.ValidatorTests
{
  public class ValidatorBaseTest
    {
        private Validator _validator;

        public ValidatorBaseTest(ITestOutputHelper output)
        {
            var services = new ServiceCollection()
                .AddLogging((builder) => builder.AddXUnit(output))
                .AddSingleton<Validator>();

            _validator = services
                .BuildServiceProvider()
                .GetRequiredService<Validator>();
        }

        [Fact]
        public void FromYandexExample_ShouldBeFromYandex()
        {
            Assert.True(_validator.FromYandex("p2p-incoming", "1234567", new Decimal(300.00), "643",
                DateTime.Parse("2011-07-01T05:00:00Z"), "41001XXXXXXXX",  false, "01234567890ABCDEF01234567890", "YM.label.12345",
                "a2ee4a9195f4a90e893cff4f62eeba0b662321f9"));
        }

        [Fact]
        public void DateTimeTest()
        {
            
        }
    }
}
