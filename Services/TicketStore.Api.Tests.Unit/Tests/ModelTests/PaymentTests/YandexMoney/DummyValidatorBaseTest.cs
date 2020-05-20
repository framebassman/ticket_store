using System;
using Xunit;
using Xunit.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TicketStore.Api.Model.Payment.YandexMoney;

namespace TicketStore.Api.Tests.Unit.Tests.ModelTests.PaymentTests.YandexMoney
{
    public class DummyValidatorBaseTest
    {
        private DummyValidator _validator;

        public DummyValidatorBaseTest(ITestOutputHelper output)
        {
            var services = new ServiceCollection()
                .AddLogging((builder) => builder.AddXUnit(output))
                .AddSingleton<DummyValidator>();

            _validator = services
                .BuildServiceProvider()
                .GetRequiredService<DummyValidator>();
        }

        [Fact]
        public void AnyParameters_ShouldBeFromYandex()
        {
            Assert.True(_validator.FromYandex("1", "2", new Decimal(3), "4",
                DateTime.Parse("2011-07-01T05:00:00Z"), "5",  false, "6", "7",
                "8"));
        }
    }
}
