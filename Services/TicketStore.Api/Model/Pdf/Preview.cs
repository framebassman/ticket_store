using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using TicketStore.Api.Model.Pdf.Model;
using TicketStore.Api.Model.Pdf.Model.BarcodeConverters;

namespace TicketStore.Api.Model.Pdf
{
    public abstract class Preview
    {
        protected readonly HttpClient Client;

        protected Preview(HttpClient client)
        {
            Client = client;
        }

        public String Layout(String ticketNumber)
        {
            var converter = new FakeConverter();
            var ticketNumbers = new List<String>();
            ticketNumbers.Add(ticketNumber);
            ticketNumbers.Add(ticketNumber);
            var barcodes = new List<Barcode>();
            foreach (var number in ticketNumbers)
            {
                barcodes.Add(new Barcode(number, converter));
            }
            var artist = "Blur";
            var time = DateTime.Now;
            var price = Decimal.One;
            return new Layout(
                new Ticket(
                    barcodes,
                    artist,
                    time,
                    price
                )
            ).ToHtml();
        }
    }
}