using System;
using System.Collections.Generic;
using TicketStore.Api.Model.PdfDocument;
using TicketStore.Api.Tests.Unit.Stubs;
using TicketStore.Api.Tests.Unit.Stubs.Http;
using TicketStore.Data.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.Tests.ModelTests.PdfDocument
{
    public class PdfTests
    {
        [Fact]
        public void GeneratePdf_TimeShouldBeInSpecifiedFormat()
        {
            // Arrange
            var testEvent = new Event
            {
                Artist = "Test",
                Time = new DateTime(2019, 09, 14, 19, 00, 00),
            };
            var ticket = new Ticket
            {
                CreatedAt = DateTime.Now,
                Number = "1"
            };

            // Act
            var pdf = new Pdf(testEvent, new List<Ticket>{ticket}, new DummyPdfConverter(), new DummyBarcodeConverter(), new DummyHttpClient());

            // Assert
            Assert.Equal("Суббота, 14 сентября 2019 г. 22:00", pdf.ConcertTime());
        }

        [Fact]
        public void GeneratePdf_ConverterShouldConvertToBytes()
        {
            // Arrange
            var concert = new Event
            {
                Artist = "Test",
                Time = DateTime.Now
            };
            var tickets = new List<Ticket>
            {
                new Ticket
                {
                    CreatedAt = DateTime.Now,
                    Number = "1"
                }
            };
            var pdfConverter = new DummyPdfConverter();
            var barcodeConverter = new DummyBarcodeConverter();
            var httpClient = new DummyHttpClient();

            // Act
            var pdf = new Pdf(concert, tickets, pdfConverter, barcodeConverter, httpClient);
            var bytes = pdf.ToBytes();
            
            // Assert
            Assert.Single(bytes);
            Assert.Equal(0, bytes[0]);
        }
    }
}
