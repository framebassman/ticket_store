using System;
using System.Linq;
using TicketStore.Api.Model.Validation.Exceptions;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model.Validation
{
    public class InaccurateFinder : ITicketFinder
    {
        private readonly ApplicationContext _db;
        private readonly String _verificationMethod;

        public InaccurateFinder(ApplicationContext context, String method)
        {
            _db = context;
            _verificationMethod = method;
        }

        public Ticket Find(TurnstileScan scan)
        {
            var code = scan.code.Substring(0, scan.code.Length - 2);
            var minCodeLength = 4;
            if (code.Length < minCodeLength)
            {
                throw new CodeToShort(_verificationMethod, minCodeLength);
            };

            var tickets = _db.Tickets.Where(t => t.Number.StartsWith(code));
            if (tickets.Count() > 1)
            {
                throw new MultipleTicketsFound(_verificationMethod, tickets.Count());
            };

            var ticket = tickets.FirstOrDefault();
            if (ticket == null)
            {
                throw new TicketNotFound(_verificationMethod);
            };

            return ticket;
        }
    }
}
