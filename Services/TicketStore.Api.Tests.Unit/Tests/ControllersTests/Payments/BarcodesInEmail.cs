using System;
using System.Collections.Generic;
using System.Linq;
using TicketStore.Api.Model.PdfDocument;
using TicketStore.Api.Tests.Unit.Matchers;
using TicketStore.Api.Tests.Unit.Stubs;
using TicketStore.Data.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.Tests.ControllersTests.Payments
{
    public class BarcodesInEmail : FakePaymentsControllerBaseTest
    {
        public BarcodesInEmail() : base("count_of_barcodes")
        {
        }

        [Fact]
        public void Combine1Ticket_With1Barcode_TicketShouldContain1Barcode()
        {
            // Arrange
            DateTime concertTime = DateTime.Now;
            SeedTestData(concertTime);
            Event concert = Db.Events.First();
            String email = "test@test.test";
            
            // Act
            Controller.SendTickets(concert, concert.Tickets.GetRange(0, 1), email);

            // Assert
            List<Pdf> pdfs = EmailService.PdfList(email);
            Assert.Single(pdfs);
            Assert.IsType<DummyPdf>(pdfs.First());
            DummyPdf pdf = pdfs.First() as DummyPdf;
            NHamcrest.XUnit.Assert.That(
                pdf.GetPreview().Layout(),
                new ContainsNTimes("src=\"data:image/png;base64, base64\"", 1)
            );
        }
    }
}