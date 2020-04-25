using System.Collections.Generic;
using System.Net.Http;
using DinkToPdf.Contracts;
using TicketStore.Api.Model.PdfDocument;
using TicketStore.Api.Model.PdfDocument.Model.BarcodeConverters;
using TicketStore.Data.Model;

namespace TicketStore.Api.Tests.Unit.Stubs
{
    public class DummyPdf : Pdf
    {
        public DummyPdf(Event concert, List<Ticket> tickets, IConverter pdfConverter, Converter barcodeConverter, HttpClient client)
            : base(concert, tickets, pdfConverter, barcodeConverter, client)
        {
        }

        public Preview GetPreview()
        {
            return Preview;
        }
    }
}