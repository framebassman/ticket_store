using System.Linq;
using TicketStore.Api.Model.Validation.Exceptions;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model.Validation
{
    public class BarcodeNetFinder : ITicketFinder
    {
        private readonly ApplicationContext _db;

        public BarcodeNetFinder(ApplicationContext context)
        {
            _db = context;
        }
        
        public Ticket Find(TurnstileScan barcode)
        {
            var code = barcode.code.Substring(0, barcode.code.Length - 2);
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