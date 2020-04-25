using System;
using System.Collections.Generic;
using TicketStore.Api.Model.PdfDocument;
using TicketStore.Api.Tests.Unit.ModelTests.TicketPreview.Model;
using TicketStore.Api.Tests.Unit.Stubs;
using TicketStore.Api.Tests.Unit.Stubs.Http;
using TicketStore.Data.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ModelTests
{
    public class PdfTimeTest
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
    }
}
