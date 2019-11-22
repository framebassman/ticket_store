using System;
using System.Collections.Generic;
using System.Net.Http;
using TicketStore.Api.Model.Pdf.Model;
using TicketStore.Api.Model.Pdf.Model.BarcodeConverters;
using TicketStore.Data.Model;
using Ticket = TicketStore.Data.Model.Ticket;

namespace TicketStore.Api.Model.Pdf
{
    public class Preview
    {
        private readonly HttpClient _client;
        private readonly List<Ticket> _tickets;
        private readonly Event _concert;
        private readonly Converter _barcodeConverter;

        public Preview(HttpClient client, Event concert)
        {
            _client = client;
            _concert = concert;
            _tickets = concert.Tickets;
            _barcodeConverter = new FakeConverter();
        }

        public String Layout()
        {
            var barcodes = new List<Barcode>();
            foreach (var ticket in _tickets)
            {
                barcodes.Add(new Barcode(ticket.Number, _barcodeConverter));
            }
            return new Layout(
                new TicketStore.Api.Model.Pdf.Model.Ticket(
                    barcodes,
                    _concert.Artist,
                    _concert.Time,
                    _concert.Roubles
                )
            ).ToHtml();
        }
    }
}