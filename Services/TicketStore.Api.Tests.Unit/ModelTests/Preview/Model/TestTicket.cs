using System;
using System.Collections.Generic;
using TicketStore.Api.Model.Pdf.Model;
using TicketStore.Api.Model.Pdf.Model.BarcodeConverters;

namespace TicketStore.Api.Tests.Unit.ModelTests.Preview.Model
{
    public class TestTicket : Ticket
    {
        private String _template;
        
        public TestTicket(
            String template,
            List<Barcode> barcodes,
            String artist,
            DateTime time,
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
