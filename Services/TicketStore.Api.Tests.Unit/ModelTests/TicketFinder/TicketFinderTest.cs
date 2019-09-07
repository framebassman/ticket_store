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
            var barcode = new UnknownBarcode("123");

            var ex = Assert.Throws<Exception>(() => Finder.Find(barcode));

            Assert.Equal("Verification method doesn't exist: Unknown", ex.Message);
        }

        [Fact]
        public void ManualVerificationMethod_TicketNotFound()
        {
            var barcode = new ManualBarcode("123");

            var ex = Assert.Throws<Exception>(() => Finder.Find(barcode));

            Assert.Equal("Method: Manual. Ticket not found in Database", ex.Message);
        }

        [Fact]
        public void ManualVerificationMethod_TicketExist()
        {
            var barcode = new ManualBarcode("1111122222");

            var ticket = Finder.Find(barcode);

            Assert.Equal("1111122222", ticket.Number);
        }

        [Fact]
        public void BarcodeVerificationMethod_TicketExist()
        {
            var barcode = new BarcodeBarcode("11111");

            var ticket = Finder.Find(barcode);

            Assert.Equal("1111122222", ticket.Number);
        }

        [Fact]
        public void BarcodeVerificationMethod_ConcertNotFound()
        {
            var barcode = new BarcodeBarcode("55555");

            var ex = Assert.Throws<Exception>(() => Finder.Find(barcode));

            Assert.Equal("Method: Barcode. Concert is not found for ticket", ex.Message);
        }

        [Fact]
        public void BarcodeVerificationMethod_TicketNotFound()
        {
            var barcode = new BarcodeBarcode("123");

            var ex = Assert.Throws<Exception>(() => Finder.Find(barcode));

            Assert.Equal("Method: Barcode. Ticket not found in Database", ex.Message);
        }

        [Fact]
        public void BarcodeVerificationMethod_TooLateForConcert()
        {
            var now = _dbTime.AddHours(15);
            SetupFinder(now);
            var barcode = new BarcodeBarcode("11111");

            var ex = Assert.Throws<Exception>(() => Finder.Find(barcode));

            Assert.Equal("Method: Barcode. Too late for concert, it's happend 15 hours ago", ex.Message);
        }

        [Fact]
        public void BarcodeVerificationMethod__TooEarlyForConcert()
        {
            var now = _dbTime.AddHours(-15);
            SetupFinder(now);
            var barcode = new BarcodeBarcode("11111");;

            var ex = Assert.Throws<Exception>(() => Finder.Find(barcode));

            Assert.Equal("Method: Barcode. Too early for concert, it will happen in 15 hours", ex.Message);
        }

        protected void SetupFinder(DateTime date)
        {
            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(mock => mock.Now).Returns(date);

            Finder = new TicketFinder(Db, Logger, dateTimeProviderMock.Object);
        }
    }
}
