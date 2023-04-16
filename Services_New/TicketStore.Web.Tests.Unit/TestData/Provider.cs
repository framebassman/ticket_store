using TicketStore.Data.Model;

namespace TicketStore.Web.Tests.Unit.TestData
{
    public class Provider
    {
        private MerchantsProvider _merchants;

        public Provider()
        {
            _merchants = new MerchantsProvider();
        }

        public MerchantsProvider Merchants() => _merchants;
        public EventsProvider Events(Merchant merchant) => new EventsProvider(merchant);
    }
}
