using System;
using Moq;
using TicketStore.Api.Model;
using TicketStore.Api.Model.Validation;
using TicketStore.Api.Tests.Unit.BaseTest;
using TicketStore.Api.Tests.Unit.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ModelTests
{
    public class TicketFinderTest : DbBaseTest<ITicketFinder>
    {
        protected TicketFinder Finder;
        protected DateTime _dbTime;
        public TicketFinderTest() : base("ticket_finder") {
            // UTC should be stored in Database
            _dbTime = new DateTime(2019, 10, 7, 16, 00, 00, DateTimeKind.Utc);
            SeedTestData(_dbTime);
            SetupFinder(_dbTime);
        }

        [Fact]
        public void InvalidVeificationMethod()
        {
            var turnstileScan = new UnknownTurnstileScan("123");

            var ex = Assert.Throws<Exception>(() => Finder.Find(turnstileScan));

            Assert.Equal("Verification method doesn't exist: Unknown", ex.Message);
        }

        [Fact]
        public void ManualVerificationMethod_TicketExist()
        {
            var turnstileScan = new ManualTurnstileScan("1111122222");

            var ticket = Finder.Find(turnstileScan);

            Assert.Equal("1111122222", ticket.Number);
        }

        [Fact]
        public void BarcodeVerificationMethod_TicketExist()
        {
            var turnstileScan = new BarcodeTurnstileScan("1111122222");

            var ticket = Finder.Find(turnstileScan);

            Assert.Equal("1111122222", ticket.Number);
        }

        protected void SetupFinder(DateTime date)
        {
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(mock => mock.Now).Returns(date);

            Finder = new TicketFinder(Db, Logger, dateTimeProviderMock.Object);
        }
    }
}
