using System;
using System.Collections.Generic;
using TicketStore.Api.Model.Pdf.Model;
using TicketStore.Api.Tests.Unit.ModelTests.Preview.Model;
using Xunit;

namespace TicketStore.Api.Tests.Unit.ModelTests.Preview
{
    public class TemplatesInTicket : TemplatesInBarcode
    {
        protected readonly Ticket Ticket;
        
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
                DateTime.Parse("8/18/2010 12:00:00 AM"),
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