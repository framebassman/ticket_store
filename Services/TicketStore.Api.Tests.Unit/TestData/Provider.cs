using System.Collections.Generic;
using TicketStore.Data.Model;

namespace TicketStore.Api.Tests.Unit.TestData
{
    public class Provider
    {
        private readonly MerchantsProvider _merchants;

        public Provider()
        {
            _merchants = new MerchantsProvider();
        }

        public MerchantsProvider Merchants() => _merchants;
        public EventsProvider Events(Merchant merchant) => new EventsProvider(merchant);
        public TicketsProvider Tickets() => new TicketsProvider();
        public PaymentsProvider Payments(List<Ticket> tickets) => new PaymentsProvider(tickets);
    }
}