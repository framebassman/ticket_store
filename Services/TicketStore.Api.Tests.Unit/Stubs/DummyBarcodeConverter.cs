using System;
using TicketStore.Api.Model.PdfDocument.Model.BarcodeConverters;
using TicketStore.Api.Tests.Unit.Stubs.Http;

namespace TicketStore.Api.Tests.Unit.Stubs
{
    public class DummyBarcodeConverter : Converter
    {
        public DummyBarcodeConverter() : base(new DummyHttpClientFactory())
        {
        }
        
        public override string ToBase64(String origin)
        {
            return "base64";
        }
    }
}
