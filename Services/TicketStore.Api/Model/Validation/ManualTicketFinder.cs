using System;
using System.Linq;
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
            if (ticket == null)
            {
                throw new Exception($"Method: Manual. Ticket not found in Database");
            };
            return ticket;
        }
    }
}
