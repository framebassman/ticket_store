using System;
using System.Collections.Generic;
using TicketStore.Api.Model.PdfDocument.Model;
using TicketStore.Api.Tests.Unit.ModelTests.TicketPreview.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ModelTests.TicketPreview
{
    public class TemplatesInTicket : TemplatesInBarcode
    {
        protected Ticket Ticket;
        
        public TemplatesInTicket()
        {
            var template = "Barcodes: \n\t%BARCODES%\n, Artist: %ARTIST%, Time: %TIME%, Price: %PRICE%";
            var barcodes = new List<Barcode>
            {
                Barcode
            };
            Ticket = new TestTicket(
                template,
                barcodes, 
                "Test artist", 
                "Среда, 18 августа 2010 г. 04:00",
                new Decimal(10)
            );
        }
        
        [Fact]
        public override void RenderHtml_ChangePlaceholdersInTemplate()
        {
            // Act
            var html = Ticket.ToHtml();

            // Assert
            Assert.Equal(
                $"Barcodes: \n\t{Barcode.ToHtml()}\n, " +
                $"Artist: Test artist, " +
                $"Time: Среда, 18 августа 2010 г. 04:00, " +
                $"Price: 10",
                html
            );
        }
    }
}