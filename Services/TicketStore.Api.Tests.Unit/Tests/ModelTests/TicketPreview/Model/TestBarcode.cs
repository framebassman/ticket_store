using System;
using TicketStore.Api.Model.PdfDocument.Model;
using TicketStore.Api.Model.PdfDocument.Model.BarcodeConverters;

namespace TicketStore.Api.Tests.Unit.Tests.ModelTests.TicketPreview.Model
{
    public class TestBarcode : Barcode
    {
        private String _template;
        
        public TestBarcode(String template, String ticketNumber, Converter converter) : base(ticketNumber, converter)
        {
            _template = template;
        }

        protected override String ReadTemplate()
        {
            return _template;
        }
    }
}