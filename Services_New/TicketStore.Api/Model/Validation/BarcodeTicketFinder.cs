using System;
using System.Linq;
using TicketStore.Api.Model.Validation.Exceptions;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model.Validation
{
    public class BarcodeTicketFinder : ITicketFinder
    {
        private ApplicationContext _db;
        private IDateTimeProvider _dateTimeProvider;

        public BarcodeTicketFinder(ApplicationContext context, IDateTimeProvider dateTimeProvider)
        {
            _db = context;
            _dateTimeProvider = dateTimeProvider;
        }
        public Ticket Find(TurnstileScan barcode)
        {
            Ticket ticket;
            try
            {
                ticket = StrictEquals(barcode);
            }
            catch (TicketNotFound)
            {
                ticket = InaccurateEquals(barcode);
            }

            var concert = _db.Events.FirstOrDefault(e => e.Id == ticket.EventId);
            if (concert == null)
            {
                throw new ConcertNotFound(VerificationMethod.Barcode);
            };

            // TimeSpan dateDiff = _dateTimeProvider.Now - concert.Time;
            // var hoursDiff = Math.Abs(dateDiff.TotalHours);
            // if (dateDiff.TotalHours <= -12) {
            //     throw new TooEarly(VerificationMethod.Barcode, hoursDiff);
            // }

            // if (dateDiff.TotalHours >= 12) {
            //     throw new TooLate(VerificationMethod.Barcode, hoursDiff);
            // }

            return ticket;
        }

        private Ticket StrictEquals(TurnstileScan scan)
        {
            var tickets = _db.Tickets.Where(t => t.Number == scan.code);
            if (tickets.Count() > 1)
            {
                throw new MultipleTicketsFound(VerificationMethod.Barcode, tickets.Count());
            }

            var ticket = tickets.FirstOrDefault();
            if (ticket == null)
            {
                throw new TicketNotFound(VerificationMethod.Barcode);
            }

            return ticket;
        }

        private Ticket InaccurateEquals(TurnstileScan scan)
        {
            var code = scan.code.Substring(0, scan.code.Length - 2);
            var minCodeLength = 4;
            if (code.Length < minCodeLength)
            {
                throw new CodeToShort(VerificationMethod.Barcode, minCodeLength);
            }

            var tickets = _db.Tickets.Where(t => t.Number.StartsWith(code));
            if (tickets.Count() > 1)
            {
                throw new MultipleTicketsFound(VerificationMethod.Barcode, tickets.Count());
            }

            var ticket = tickets.FirstOrDefault();
            if (ticket == null)
            {
                throw new TicketNotFound(VerificationMethod.Barcode);
            }

            return ticket;
        }
    }
}