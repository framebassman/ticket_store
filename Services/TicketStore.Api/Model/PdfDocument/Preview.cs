using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using TicketStore.Api.Model.PdfDocument.Model;
using TicketStore.Api.Model.PdfDocument.Model.BarcodeConverters;
using TicketStore.Data.Model;
using Ticket = TicketStore.Data.Model.Ticket;

namespace TicketStore.Api.Model.PdfDocument
{
    public class Preview
    {
        private readonly CultureInfo _culture;
        private readonly List<Ticket> _tickets;
        private readonly Event _concert;
        private readonly Converter _barcodeConverter;

        public Preview(HttpClient client, Event concert, List<Ticket> tickets, Converter barcodeConverter)
        {
            _concert = concert;
            _tickets = tickets;
            _culture = CultureInfo.CreateSpecificCulture("ru-RU");
            _barcodeConverter = barcodeConverter;
        }

        public String Layout()
        {
            var barcodes = new List<Barcode>();
            foreach (var ticket in _tickets)
            {
                barcodes.Add(new Barcode(ticket.Number, _barcodeConverter));
            }
            return new Layout(
                new Model.Ticket(
                    barcodes,
                    _concert.Artist,
                    ConcertTime(),
                    _concert.Roubles
                )
            ).ToHtml();
        }

        public String ConcertTime()
        {
            return FormatTime(_concert.Time);
        }
        
        private String FormatTime(DateTime origin)
        {
            var mskTimezone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Moscow");
            var originTime = DateTime.SpecifyKind(origin, DateTimeKind.Utc);
            var withTimezone = TimeZoneInfo.ConvertTimeFromUtc(originTime, mskTimezone);
            var allLowerCase = withTimezone.ToString("f", _culture);
            var firstLetterIsCapital = new StringBuilder()
                .Append(allLowerCase[0].ToString().ToUpper())
                .Append(allLowerCase.Substring(1, allLowerCase.Length - 1))
                .ToString();
            return firstLetterIsCapital;
        }
    }
}