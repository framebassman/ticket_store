using System.Collections.Generic;
using System.Net.Http;
using DinkToPdf.Contracts;
using TicketStore.Api.Model.Pdf;
using TicketStore.Data.Model;

namespace TicketStore.Api.Tests.Unit.Stubs
{
    public class DummyPdf : Pdf
    {
        public DummyPdf(Event concert, List<Ticket> tickets, IConverter converter, HttpClient client)
            : base(concert, tickets, converter, client)
        {
        }
    }
}