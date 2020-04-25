using System;
using TicketStore.Api.Model.PdfDocument.Model.BarcodeConverters;

namespace TicketStore.Api.Tests.Unit.Stubs
{
    public class DummyBarcodeConverter : Converter
    {
        public DummyBarcodeConverter() : base(null)
        {
        }
        
        public override string ToBase64(String origin)
        {
            return "base64";
        }
    }
}
