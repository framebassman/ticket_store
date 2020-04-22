using System;
using System.Collections.Generic;
using TicketStore.Api.Model.Pdf.Model;

namespace TicketStore.Api.Tests.Unit.ModelTests.TicketPreview.Model
{
    public class TestTicket : Ticket
    {
        private String _template;
        
        public TestTicket(
            String template,
            List<Barcode> barcodes,
            String artist,
            String time,
            Decimal price
        ) : base(barcodes, artist, time, price)
        {
            _template = template;
        }
        
        protected override String ReadTemplate()
        {
            return _template;
        }
    }
}
