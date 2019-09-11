using System.Linq;
using TicketStore.Api.Model.Validation.Exceptions;
using TicketStore.Data;
using TicketStore.Data.Model;

namespace TicketStore.Api.Model.Validation
{
    public class ManualTicketFinder : ITicketFinder
    {
        private readonly StrictFinder _strictFinder;

        public ManualTicketFinder(ApplicationContext context)
        {
            _strictFinder = new StrictFinder(context, VerificationMethod.Manual);
        }
        public Ticket Find(TurnstileScan scan)
        {
            return _strictFinder.Find(scan);
        }
    }
}
