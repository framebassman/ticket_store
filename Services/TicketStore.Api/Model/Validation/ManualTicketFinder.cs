using System.Linq;
using TicketStore.Api.Model.Validation.Exceptions;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model.Validation
{
    public class ManualTicketFinder : ITicketFinder
    {
        private readonly ApplicationContext _db;

        public ManualTicketFinder(ApplicationContext context)
        {
            _db = context;
        }
        public Ticket Find(TurnstileScan barcode)
        {
            var ticket = _db.Tickets.FirstOrDefault(t => t.Number == barcode.code);
            var anotherTicket = _db.Tickets
                .AsEnumerable()
                .FirstOrDefault(t => t.Number == barcode.code);
            if (ticket == null)
            {
                throw new TicketNotFound(VerificationMethod.Manual);
            };
            return ticket;
        }
    }
}
