using System;
using System.Linq;
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
                throw new Exception($"Method: Barcode. Ticket not found in Database");
            };

            var concert = _db.Events.FirstOrDefault(e => e.Id == ticket.EventId);
            if (concert == null)
            {
                throw new Exception($"Method: Barcode. Concert is not found for ticket");
            };

            TimeSpan dateDiff = _dateTimeProvider.Now - concert.Time;
            var hoursDiff = Math.Abs(dateDiff.TotalHours);
            if (dateDiff.TotalHours <= -12) {
                throw new Exception($"Method: Barcode. Too early for concert, it will happen in {hoursDiff} hours");
            }

            if (dateDiff.TotalHours >= 12) {
                throw new Exception($"Method: Barcode. Too late for concert, it's happend {hoursDiff} hours ago");
            }

            return ticket;
        }
    }
}
