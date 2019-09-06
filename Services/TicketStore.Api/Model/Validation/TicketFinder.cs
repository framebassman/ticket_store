using System;
using System.Linq;
using Microsoft.Extensions.Logging;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model.Validation
{
    public interface ITicketFinder
    {
        Ticket Find(Barcode barcode);
    }

    public class TicketFinder : ITicketFinder
    {
        private readonly ApplicationContext _db;
        private readonly ILogger<TicketFinder> _log;

        public TicketFinder(ApplicationContext context, ILogger<TicketFinder> log)
        {
            _db = context;
            _log = log;
        }
        public Ticket Find(Barcode barcode)
        {
            _log.LogInformation("Find ticket using verification method: {0}", barcode.method);
            if (barcode.method == VerificationMethod.Barcode) {
                var ticket = _db.Tickets.FirstOrDefault(t => t.Number.StartsWith(barcode.code));
                if (ticket == null)
                {
                    return null;
                };

                var concert = _db.Events.FirstOrDefault(e => e.Id == ticket.EventId);
                if (concert == null)
                {
                    return null;
                };

                TimeSpan dateDiff = concert.Time - DateTime.Now;
                if (dateDiff.TotalDays >= 2) {
                    _log.LogInformation("Ticket is not valid for todays concert, difference in days: {0}", dateDiff.TotalDays);
                    return null;
                }

                return ticket;
            }

            if (barcode.method == VerificationMethod.Manual) {
                return _db.Tickets.FirstOrDefault(t => t.Number == barcode.code);
            }

            _log.LogInformation("Verification method doesn't exist: {0}", barcode.method);
            return null;
        }
    }
}
