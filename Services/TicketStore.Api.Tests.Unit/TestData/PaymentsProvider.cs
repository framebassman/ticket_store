using System.Collections.Generic;
using TicketStore.Data.Model;

namespace TicketStore.Api.Tests.Unit.TestData
{
    public class PaymentsProvider
    {
        private readonly List<Ticket> _tickets;

        public PaymentsProvider(List<Ticket> tickets)
        {
            _tickets = tickets;
        }
        public Payment First()
        {
            return new Payment { Amount = 200, Email = "api@unit.test", Tickets = _tickets };
        }
    }
}