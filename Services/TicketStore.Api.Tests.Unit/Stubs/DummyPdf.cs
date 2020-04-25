using System.Collections.Generic;
using System.Net.Http;
using DinkToPdf.Contracts;
using TicketStore.Api.Model.PdfDocument;
using TicketStore.Api.Tests.Unit.ModelTests.TicketPreview.Model;
using TicketStore.Data.Model;

namespace TicketStore.Api.Tests.Unit.Stubs
{
    public class DummyPdf : Pdf
    {
        public DummyPdf(Event concert, List<Ticket> tickets, IConverter pdfConverter, HttpClient client)
            : base(concert, tickets, pdfConverter, new DummyBarcodeConverter(), client)
        {
        }

        public Preview GetPreview()
        {
            return Preview;
        }
    }
}