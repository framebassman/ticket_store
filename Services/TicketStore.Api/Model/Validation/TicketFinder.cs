using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model.Validation
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }

    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now => DateTime.Now;
    }


    public interface ITicketFinder
    {
        Ticket Find(Barcode barcode);
    }

    public class TicketFinder : ITicketFinder
    {
        private readonly ApplicationContext _db;
        private readonly ILogger<ITicketFinder> _log;
        private readonly IDateTimeProvider _dateTimeProvider;

        public TicketFinder(ApplicationContext context, ILogger<ITicketFinder> log, IDateTimeProvider dateTimeProvider)
        {
            _db = context;
            _log = log;
            _dateTimeProvider = dateTimeProvider;
        }
        public Ticket Find(Barcode barcode)
        {
            _log.LogInformation("Find ticket using verification method: {0}", barcode.method);
            if (barcode.method == VerificationMethod.Barcode) {
                var ticket = _db.Tickets.FirstOrDefault(t => t.Number.StartsWith(barcode.code));
                if (ticket == null)
                {
                    throw new Exception($"Verification Method: Barcode. Ticket not found in Database");
                };

                var concert = _db.Events.FirstOrDefault(e => e.Id == ticket.EventId);
                if (concert == null)
                {
                    throw new Exception($"Verification Method: Barcode. Concert is not found for ticket");
                };

                TimeSpan dateDiff = concert.Time - _dateTimeProvider.Now;
                if (dateDiff.TotalDays >= 2) {
                    throw new Exception($"Verification Method: Barcode. Ticket is not valid for todays concert, difference in days: {dateDiff.TotalDays}");
                }

                return ticket;
            }

            if (barcode.method == VerificationMethod.Manual) {
                var ticket = _db.Tickets.FirstOrDefault(t => t.Number == barcode.code);
                if (ticket == null)
                {
                    throw new Exception($"Verification Method: Manual. Ticket not found in Database");
                };
                return ticket;
            }

            throw new Exception($"Verification method doesn't exist: {barcode.method}");
        }
    }
}
