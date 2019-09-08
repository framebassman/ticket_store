using System;
using System.Linq;
using TicketStore.Api.Model.Validation.Exceptions;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model.Validation
{
    public class BarcodeTicketFinder : ITicketFinder
    {
        private readonly ApplicationContext _db;
        private readonly IDateTimeProvider _dateTimeProvider;

        public BarcodeTicketFinder(ApplicationContext context, IDateTimeProvider dateTimeProvider)
        {
            _db = context;
            _dateTimeProvider = dateTimeProvider;
        }
        public Ticket Find(TurnstileScan barcode)
        {
            var ticket = _db.Tickets.FirstOrDefault(t => t.Number.StartsWith(barcode.code));
            if (ticket == null)
            {
                throw new TicketNotFound(VerificationMethod.Barcode);
            };

            var concert = _db.Events.FirstOrDefault(e => e.Id == ticket.EventId);
            if (concert == null)
            {
                throw new ConcertNotFound(VerificationMethod.Barcode);
            };

            TimeSpan dateDiff = _dateTimeProvider.Now - concert.Time;
            var hoursDiff = Math.Abs(dateDiff.TotalHours);
            if (dateDiff.TotalHours <= -12) {
                throw new TooEarly(VerificationMethod.Barcode, hoursDiff);
            }

            if (dateDiff.TotalHours >= 12) {
                throw new TooLate(VerificationMethod.Barcode, hoursDiff);
            }

            return ticket;
        }
    }
}
